using CsvHelper;
using CsvHelper.Configuration;
using ICSharpCode.SharpZipLib.BZip2;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.OpenApi.Models;
using Passports.Models;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Passports.Repositories {
    public class LocalRepository  {
        private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
        private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
        private List<Passport> _records = new();
        private const int TASK_LIMIT = 15;
        private Task<(string[], int)>[] Tasks { get; set; }
        private Dictionary<int, string[]> ConflictsDictionary { get; set; }
        private Dictionary<int, int> TasksOrderMap { get; set; }
        public List<Passport> ReadAll() {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                Delimiter = ",",
                MissingFieldFound = null,
                ReadingExceptionOccurred = args => {
                    if(args.Exception is CsvHelper.FieldValidationException)
                        return false;
                    return true;
                }
            };
            using(var reader = new StreamReader(_csvFileInfo.FullName, Encoding.UTF8))
            using(var csv = new CsvReader(reader, configuration)) {
                csv.Context.RegisterClassMap<PassportMap>();
                var records = csv.GetRecords<Passport>();
                _records = records.ToList();
            }
            return _records;
        }
        public int FindInChunks(int series, int number) {
            int chunkSize = 12000;
            int headersOffset = 26;
            int match = 0;
            string remainedRow = String.Empty;
            using(FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 524288)) {
                byte[] buffer = new byte[headersOffset];
                int bytesRead;
                bool addRow = false;
                bytesRead = fs.Read(buffer, 0, headersOffset);
                buffer = new byte[chunkSize];
                while((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
                    string chunk;
                    chunk = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);
                    string[] rows = GetRowsFromChunk(chunk);
                    var lastRowSplitted = rows[^1].Split(",");
                    if(addRow) {
                        AddRemainedRow(rows, remainedRow);
                        addRow = false;
                    }
                    if(lastRowSplitted.Length != 2 || lastRowSplitted[0].Length != 4 || lastRowSplitted[1].Length != 6) {
                        remainedRow = rows[rows.Length - 1];
                        Array.Resize(ref rows, rows.Length - 1);
                        addRow = true;
                    }
                    match += MatchesInRowsSplit(rows, series, number);
                }
            }
            return match;
        }
        public async Task<int> CountFileAsync() {
            int count = 0;
            int rowsToRead = 118319374;
            int taskLimit = 10;
            Task<int>[] tasks = new Task<int>[taskLimit];
            using(FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 32768)) {
                int rowsPerTask = rowsToRead / taskLimit;
                for(int i = 0; i < taskLimit; i++) {
                    int start = i * rowsPerTask;
                    int finish = i + 1 * rowsPerTask;
                    if(i == taskLimit - 1) {
                        finish = rowsToRead;
                    }
                    tasks[i] = ReadChunkAsync(fs, start, finish);
                }
                Task.WaitAll(tasks);
                foreach(var task in tasks) {
                    count += task.Result;
                }
            }
            return count;
        }
        private async Task<int> ReadChunkAsync(FileStream fs, int start, int finish) {
            using var reader = new StreamReader(fs);
            int count = 0;
            for(int i = 0; i < start; i++) {
                string? line = reader.ReadLine();
            }
            for(int i = start; i < finish; i++) {
                string? line = reader.ReadLine();
                count++;
            }
            return count;
        }
        public async Task<int> FindInChunksAsync(int series, int number) {
            int chunkSize = 120000;
            int headersOffset = 26;
            int match = 0;
            Tasks = new Task<(string[], int)>[TASK_LIMIT];
            ConflictsDictionary = new();
            TasksOrderMap = new();
            using(FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 262144)) {
                byte[] buffer = new byte[headersOffset];
                int bytesRead;
                bytesRead = fs.Read(buffer, 0, headersOffset);
                buffer = new byte[chunkSize];

                int i = 0;
                int index = 0;
                while((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0) { 
                    byte[] bytesBuffer = new byte[bytesRead];
                    Array.Copy(buffer, 0, bytesBuffer, 0, bytesRead);
                    if (i < TASK_LIMIT) {
                        var findTask = Task.Run(() => ProcessChunk(bytesBuffer, bytesRead, series, number));
                        Tasks[i] = findTask;
                        TasksOrderMap.Add(i, i);
                        i++;
                    } else {
                        index = Task.WaitAny(Tasks);
                        match += MatchTaskRoutine(ref i, index);
                        Tasks[index] = Task.Run(() => ProcessChunk(bytesBuffer, bytesRead, series, number));
                    }
                    buffer = new byte[chunkSize];
                }

                Task.WaitAll(Tasks);
                for(int l = 0; l < Tasks.Length; l++) {
                    if(l == index) {
                        continue;
                    }
                    match += MatchTaskRoutine(ref i, l);
                }
                var mergedConflicts = Task.Run(() => GetConflictsRowsList(ConflictsDictionary));
                mergedConflicts.Wait();
                var list = mergedConflicts.Result;
                match += MatchesEnumerableStringCompare(list, series, number);
            }
            return match;
        }
        private int MatchTaskRoutine(ref int i, int taskIndex) {
            (string[] problemRows, int matches) = Tasks[taskIndex].Result;
            ConflictsDictionary.Add(TasksOrderMap[taskIndex], problemRows);
            TasksOrderMap[taskIndex] = i;
            i++;
            return matches;
        }

        
        private List<string> GetConflictsRowsList(Dictionary<int, string[]> conflicts) {
            List<string> rows = new List<string>();
            for(int i = 0; i < conflicts.Count; i++) {
                string firstRow = conflicts[i][0];
                string secondRow = conflicts[i][1];
                if(firstRow.Length<11) {
                    firstRow = conflicts[i - 1][1] + firstRow;
                }
                rows.Add(firstRow);
                if(secondRow!=null) {
                    if(secondRow.Length == 11) {
                        rows.Add(secondRow);
                    }
                }
            }
            return rows;
        }
        private (string[], int) ProcessChunk(byte[] buffer, int bytesRead, int series, int number) {
            var rows = GetRowsFromBytes(buffer);
            var matches = MatchesEnumerableStringCompare(rows, series, number);
            var problemRows = ProblemRows(rows);
            return (problemRows, matches);
        }
        private string[] GetRowsFromBytes(byte[] chunkData) {
            string chunk = System.Text.Encoding.Default.GetString(chunkData, 0, chunkData.Length); //////
            string[] rows = GetRowsFromChunk(chunk); /////
            return rows;
        }
        private string[] GetRowsFromChunk(string chunk) {
            return chunk.Split("\n");
        }
        private int MatchesEnumerableStringCompare(IEnumerable<string> rows, int series, int number) {
            int match = 0;
            string matchString = $"{series},{number}";
            foreach (string row in rows) {
                if (row == matchString) {
                    match++;
                }
            }
            return match;
        }
        private string[] ProblemRows(string[] rows) {
            var replaceRows = new string[2];
            replaceRows[0] = rows[0];
            replaceRows[1] = rows[^1];
            return replaceRows;
        }
        private void AddRemainedRow(string[] rows, string remainedRow) {
            rows[0] = remainedRow + rows[0];
        }
        public int MatchesInRowsSplit(string[] rows, int series, int number) {
            int match = 0;
            int seriesData, numberData;
            foreach(var row in rows) {
                var passportData = row.Split(",");
                if(!int.TryParse(passportData[0], out seriesData)
                    || !int.TryParse(passportData[1], out numberData)) {
                    continue;
                }
                if(series == seriesData && number == numberData) {
                    match++;
                }
            }
            return match;
        }

        public void DecompressFile() {
            using(FileStream fileToDecompressAsStream = _bzFileInfo.OpenRead()) {
                string decompressedFileName = _csvFileInfo.FullName;
                using(FileStream decompressedStream = File.Create(decompressedFileName)) {
                    try {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    } catch(Exception ex) {
                        throw;
                    }
                }
            }
        }
        public async Task<int> GetLinesCountAsync() {
            if(string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            using var reader = File.OpenText(_csvFileInfo.FullName);
            int count = 0;
            while(true) {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if(line == null)
                    break;
                count++;
            }
            return count;
        }
        public async Task<int> GetLinesCount2Async() {
            if(string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            using var fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 32768, true);
            using var reader = new StreamReader(fs);
            int count = 0;
            while(true) {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if(line == null)
                    break;
                count++;
            }
            return count;
        }
        public Task<int> GetLinesCount3Async() {
            if(string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            return Task.Run(() => {
                using var fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 32768);
                using var reader = new StreamReader(fs);
                int count = 0;
                while(true) {
                    string? line = reader.ReadLine();
                    if(line == null) {
                        break;
                    }
                    count++;
                }
                return count;
            });
        }
        public int GetLinesCount4() {
            int chunkSize = 12000;
            int headersOffset = 26;
            int count = 0;
            string remainedRow = String.Empty;
            using(FileStream fs = File.OpenRead(_csvFileInfo.FullName)) {
                byte[] buffer = new byte[headersOffset];
                int bytesRead;
                bool addRow = false;
                bytesRead = fs.Read(buffer, 0, headersOffset);
                buffer = new byte[chunkSize];
                while((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
                    string chunk;
                    chunk = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);
                    string[] rows = GetRowsFromChunk(chunk);
                    var lastRowSplitted = rows[^1].Split(",");
                    if(addRow) {
                        AddRemainedRow(rows, remainedRow);
                        addRow = false;
                    }
                    if(lastRowSplitted.Length != 2 || lastRowSplitted[0].Length != 4 || lastRowSplitted[1].Length != 6) {
                        remainedRow = rows[rows.Length - 1];
                        Array.Resize(ref rows, rows.Length - 1);
                        addRow = true;
                    }
                    count += rows.Length;
                }
            }
            return count;
        }

    }
}

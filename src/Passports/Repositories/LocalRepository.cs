using CsvHelper;
using CsvHelper.Configuration;
using ICSharpCode.SharpZipLib.BZip2;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.OpenApi.Models;
using Passports.Models;
using System.Globalization;
using System.Text;

namespace Passports.Repositories {
    public class LocalRepository : IRepository {
        private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
        private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
        private List<Passport> _records = new();
        public string readedStr;
        public List<Passport> ReadAll() {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                Delimiter = ",",
                MissingFieldFound = null,
                ReadingExceptionOccurred = args => {
                    if (args.Exception is CsvHelper.FieldValidationException)
                        return false;
                    return true;
                }
            };
            using (var reader = new StreamReader(_csvFileInfo.FullName, Encoding.UTF8))
            using (var csv = new CsvReader(reader, configuration)) {
                csv.Context.RegisterClassMap<PassportMap>();
                var records = csv.GetRecords<Passport>();
                _records = records.ToList();
            }
            return _records;
        }
        public int FindInChunks(int series, int number) {
            int chunkSize = 120;
            int headersOffset = 26;
            int match = 0;
            string remainedRow = String.Empty;
            using (FileStream fs = File.OpenRead(_csvFileInfo.FullName)) {
                byte[] buffer = new byte[headersOffset];
                int bytesRead;
                bool addRow = false;
                bytesRead = fs.Read(buffer, 0, headersOffset);
                buffer = new byte[chunkSize];
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
                    string chunk;
                    chunk = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);
                    string[] rows = GetRowsFromChunk(chunk);
                    var lastRowSplitted = rows[^1].Split(",");
                    if (addRow) {
                        AddRemainedRow(rows, remainedRow);
                        addRow = false;
                    }
                    if (lastRowSplitted.Length != 2 || lastRowSplitted[0].Length != 4 || lastRowSplitted[1].Length != 6) {
                        remainedRow = rows[rows.Length - 1];
                        Array.Resize(ref rows, rows.Length - 1);
                        addRow = true;
                    }
                    match += FindInChunk(rows, series, number);
                }
            }
            return match;
        }
        public int FindInChunk(string[] rows, int series, int number) {
            int match = 0;
            int seriesData, numberData;
            foreach (var row in rows) {
                var passportData = row.Split(",");
                if (!int.TryParse(passportData[0], out seriesData) 
                    || !int.TryParse(passportData[1], out numberData)) {
                    continue;
                }
                if(series== seriesData && number == numberData) {
                    match++;
                }
            }
            return match;
        }

        public void DecompressFile() {
            using (FileStream fileToDecompressAsStream = _bzFileInfo.OpenRead()) {
                string decompressedFileName = _csvFileInfo.FullName;
                using (FileStream decompressedStream = File.Create(decompressedFileName)) {
                    try {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    } catch (Exception ex) {
                        throw;
                    }
                }
            }
        }
        public async Task<int> GetLinesCountAsync() {
            if (string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            using var reader = File.OpenText(_csvFileInfo.FullName);
            int count = 0;
            while (true) {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null)
                    break;
                count++;
            }
            return count;
        }
        public async Task<int> GetLinesCount2Async() {
            if (string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            using var fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 32768, true);
            using var reader = new StreamReader(fs);
            int count = 0;
            while (true) {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null)
                    break;
                count++;
            }
            return count;
        }
        public Task<int> GetLinesCount3Async() {
            if (string.IsNullOrEmpty(_csvFileInfo.FullName))
                throw new ArgumentException("Parameter should not be null or empty!", nameof(_csvFileInfo.FullName));
            return Task.Run(() => {
                using var fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 32768);
                using var reader = new StreamReader(fs);
                int count = 0;
                while (true) {
                    string? line = reader.ReadLine();
                    if (line == null) {
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
            using (FileStream fs = File.OpenRead(_csvFileInfo.FullName)) {
                byte[] buffer = new byte[headersOffset];
                int bytesRead;
                bool addRow = false;
                bytesRead = fs.Read(buffer, 0, headersOffset);
                buffer = new byte[chunkSize];
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
                    string chunk;
                    chunk = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);
                    string[] rows = GetRowsFromChunk(chunk);
                    var lastRowSplitted = rows[^1].Split(",");
                    if (addRow) {
                        AddRemainedRow(rows, remainedRow);
                        addRow = false;
                    }
                    if (lastRowSplitted.Length != 2 || lastRowSplitted[0].Length != 4 || lastRowSplitted[1].Length != 6) {
                        remainedRow = rows[rows.Length - 1];
                        Array.Resize(ref rows, rows.Length - 1);
                        addRow = true;
                    }

                    count += rows.Length;
                }
            }
            return count;
        }
        private string[] GetRowsFromChunk(string chunk) {
            return chunk.Trim('\n').Split("\n");
        }
        private void AddRemainedRow(string[] rows, string remainedRow) {
            rows[0] = remainedRow + rows[0];
        }
    }
}

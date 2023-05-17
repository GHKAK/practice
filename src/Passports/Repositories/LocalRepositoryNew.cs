using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Text;
using System.Threading.Tasks;

namespace Passports.Repositories;
public class LocalRepositoryNew {
    private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
    private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
    private Task<(int, ConflictBytes)>[] Tasks { get; set; }
    private Dictionary<int, ConflictBytes> ConflictsDictionary { get; set; }
    private Dictionary<int, int> TasksOrderMap { get; set; }
    private const int taskLimit = 10;
    private const int conflictEdgeLimit = 8;
    private int chunkSize = 1200000;
    private int headersOffset = 26;
    private int bytesRead;
    private int lastTaskIndex = 0;
    private int bufferIndex = 0;
    private int readIndex = 0;

    private struct ConflictBytes {
        public byte[] startBytes;
        public byte[] endBytes;
        public ConflictBytes(byte[] start, byte[] end) {
            startBytes = start;
            endBytes = end;
        }
    }
    private void FindInit() {
        chunkSize = 1200000;
        headersOffset = 26;
        bytesRead = 0;
        lastTaskIndex = 0;
        bufferIndex = 0;
        readIndex = 0;
        Tasks = new Task<(int, ConflictBytes)>[taskLimit];
        ConflictsDictionary = new();
        TasksOrderMap = new();
    }
    public async Task<int> FindInChunksAsync(int series, int number) {
        int matches = 0;
        int bytesRead;
        FindInit();
        byte[] passportSearchBytes = Encoding.UTF8.GetBytes(series.ToString() + "," + number.ToString() + "\n");
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, chunkSize, useAsync: true)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            bytesRead = await fs.ReadAsync(buffer);
            var buffers = new Memory<byte>[taskLimit + 1];
            for (int k = 0; k < buffers.Length; k++) {
                buffers[k] = new Memory<byte>(new byte[chunkSize]);
            }
            while ((bytesRead = await fs.ReadAsync(buffers[bufferIndex])) > 0) {
                if (readIndex < taskLimit) {
                    var findTask = Task.Run(() => GetMatchesFromBuffer2(buffers[bufferIndex].Span, passportSearchBytes));
                    Tasks[readIndex] = findTask;
                    TasksOrderMap.Add(readIndex, readIndex);
                    readIndex++;
                    bufferIndex = readIndex;

                } else {
                    lastTaskIndex = Task.WaitAny(Tasks);
                    matches += MatchTaskRoutine(ref readIndex, lastTaskIndex);
                    bufferIndex = lastTaskIndex;
                    Tasks[lastTaskIndex] = Task.Run(() => GetMatchesFromBuffer2(buffers[bufferIndex].Span, passportSearchBytes));
                }
            }
            ProcessLastTasks(ref matches);
            matches += MatchesInConflicts(passportSearchBytes);
        }
        return matches;
    }
    private int MatchesInConflicts(byte[] searchArray) {
        int matches = 0;
        var prevConflict = ConflictsDictionary.GetValueOrDefault(0);
        for (int i = 1; i < ConflictsDictionary.Count; i++) {
            var nextConflict = ConflictsDictionary.GetValueOrDefault(i);
            if (HasMatch(prevConflict.endBytes, nextConflict.endBytes, searchArray)) { 
            matches++; }
            prevConflict = nextConflict; 
        }
        return matches;
    }
    private bool HasMatch(byte[] first, byte[] second, byte[] searchArray) {
        var merged = new byte[first.Length + second.Length];
        first.CopyTo(merged, 0);
        second.CopyTo(merged, first.Length);
        if(GetMatchesFromBuffer(merged, searchArray)==1) {
            return true;
        }
        return false;
    }
    private void ProcessLastTasks(ref int matches) {
        Task.WaitAll(Tasks);
        for (int l = 0; l < Tasks.Length; l++) {
            if (l == lastTaskIndex) {
                continue;
            }
            matches += MatchTaskRoutine(ref readIndex, l);
        }
    }
    private int MatchTaskRoutine(ref int i, int taskIndex) {
        (int matches, ConflictBytes conflict) = Tasks[taskIndex].Result;
        ConflictsDictionary.Add(TasksOrderMap[taskIndex], conflict);
        TasksOrderMap[taskIndex] = i;
        i++;
        return matches;
    }
    private int GetMatchesFromBuffer(byte[] buffer, byte[] searchPassport) {
        int matches = 0;
        int bufferIndex = 0;
        while (bufferIndex < buffer.Length) {
            if (bufferIndex + searchPassport.Length > buffer.Length) {
                bufferIndex += buffer.Length;
                break;
            }
            for (int overlapCount = 0; overlapCount < searchPassport.Length; overlapCount++) {
                if (buffer[bufferIndex + overlapCount] == searchPassport[overlapCount]) {
                    if (overlapCount == searchPassport.Length - 1) {
                        matches++;
                        bufferIndex += overlapCount;
                        break;
                    }
                    continue;
                } else {
                    bufferIndex += overlapCount + 1;
                    break;
                }
            }
        }
        return matches;
    }
    private (int, ConflictBytes) GetMatchesFromBuffer2(Span<byte> buffer, byte[] searchPassport) {
        int matches = 0;
        int bufferIndex = 0;
        while (bufferIndex < buffer.Length) {
            if (bufferIndex + searchPassport.Length > buffer.Length) {
                bufferIndex += buffer.Length;
                break;
            }
            for (int overlapCount = 0; overlapCount < searchPassport.Length; overlapCount++) {
                if (buffer[bufferIndex + overlapCount] == searchPassport[overlapCount]) {
                    if (overlapCount == searchPassport.Length - 1) {
                        matches++;
                        bufferIndex += overlapCount;
                        break;
                    }
                    continue;
                } else {
                    bufferIndex += overlapCount + 1;
                    break;
                }
            }
        }
        var startBytes = buffer.Slice(0, conflictEdgeLimit).ToArray();
        int endStart = startBytes.Length;
        int endLength = startBytes.Length;
        if (buffer.Length < conflictEdgeLimit * 2) {
            endLength = buffer.Length;
        }
        var endBytes = buffer.Slice(endStart, endLength).ToArray();
        return (matches, new ConflictBytes(startBytes, endBytes));

    }
}
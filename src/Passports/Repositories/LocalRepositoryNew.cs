using System.Text;
using System.Threading.Tasks;

namespace Passports.Repositories;
public class LocalRepositoryNew {
    private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
    private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
    private Task<int>[] Tasks { get; set; }
    private Dictionary<int, string[]> ConflictsDictionary { get; set; }
    private Dictionary<int, int> TasksOrderMap { get; set; }
    private const int taskLimit = 5;
    public async Task<int> FindInChunksAsync(int series, int number) {
        int chunkSize = 120000;
        int headersOffset = 26;
        int bytesRead;
        int match = 0;
        int readIndex = 0;
        int lastTaskIndex = 0;
        int bufferIndex = 0;
        Tasks = new Task<int>[taskLimit];
        byte[] passportSearchBytes = Encoding.UTF8.GetBytes(series.ToString() + "," + number.ToString() + "\n");
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, chunkSize)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            bytesRead = await fs.ReadAsync(buffer);
            var buffers = new Memory<byte>[taskLimit+1];
            for (int k = 0; k < buffers.Length; k++) {
                buffers[k] = new Memory<byte>(new byte[chunkSize]);
            } 

            while ((bytesRead = await fs.ReadAsync(buffers[bufferIndex])) > 0) {
                if (readIndex < taskLimit) {
                    var findTask = Task.Run(() => GetMatchesFromBuffer(buffers[bufferIndex].Span, passportSearchBytes));
                    Tasks[readIndex] = findTask;
                    readIndex++;
                    bufferIndex = readIndex;
                } else {
                    lastTaskIndex = Task.WaitAny(Tasks);
                    match += MatchTaskRoutine(ref readIndex, lastTaskIndex);
                    if (lastTaskIndex >= 5) { 
                    }
                    bufferIndex = lastTaskIndex;
                    Tasks[lastTaskIndex] = Task.Run(() => GetMatchesFromBuffer(buffers[bufferIndex].Span, passportSearchBytes));
                }
            }
        }
        return match;
    }
    private int MatchTaskRoutine(ref int i, int taskIndex) {
        int matches = Tasks[taskIndex].Result;
        i++;
        return matches;
    }
    private int GetMatchesFromBuffer(Span<byte> buffer, byte[] searchPassport) {
        int matches = 0;
        int bufferIndex = 0;
        while (bufferIndex < buffer.Length) {
            if (bufferIndex + searchPassport.Length> buffer.Length) {
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
}

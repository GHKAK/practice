using System.Text;
using System.Threading.Tasks;

namespace Passports.Repositories;
public class LocalRepositoryNew {
    private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
    private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
    private Task<int>[] Tasks { get; set; }
    private Dictionary<int, string[]> ConflictsDictionary { get; set; }
    private Dictionary<int, int> TasksOrderMap { get; set; }
    private const int taskLimit = 15;
    public async Task<int> FindInChunksAsync(int series, int number) {
        int chunkSize = 120000;
        int headersOffset = 26;
        int match = 0;
        Tasks = new Task<int>[taskLimit];
        ConflictsDictionary = new();
        TasksOrderMap = new();
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, 262144)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            int bytesRead;
            bytesRead = await fs.ReadAsync(buffer);
            buffer = new Memory<byte>(new byte[chunkSize]);
            int i = 0;
            int index = 0;
            //byte[] passportSearchBytes = Encoding.UTF8.GetBytes(series.ToString() + "," + number.ToString() + "\n");
            byte[] passportSearchBytes = new byte[2];
            passportSearchBytes[0] = 57;
            passportSearchBytes[1] = 50;
            //passportSearchBytes[2] = 48;
            //passportSearchBytes[3] = 48;
            //passportSearchBytes[4] = 44;
            //passportSearchBytes[5] = 48;
            //passportSearchBytes[6] = 57;
            //passportSearchBytes[7] = 54;
            //passportSearchBytes[8] = 56;
            //passportSearchBytes[9] = 51;
            //passportSearchBytes[10] = 50;



            while ((bytesRead = await fs.ReadAsync(buffer)) > 0) {
                if (i < taskLimit) {
                    var findTask = Task.Run(() => GetMatchesFromBuffer(buffer, passportSearchBytes));
                    Tasks[i] = findTask;
                    i++;
                    //var findTask = Task.Run(() => ProcessChunk(bytesBuffer, bytesRead, series, number));
                    //Tasks[i] = findTask;
                    //TasksOrderMap.Add(i, i);
                    //i++;
                } else {
                    index = Task.WaitAny(Tasks);
                    match += MatchTaskRoutine(ref i, index);
                    Tasks[index] = Task.Run(() => GetMatchesFromBuffer(buffer, passportSearchBytes));
                }
                buffer = new Memory<byte>(new byte[chunkSize]);
            }

            //Task.WaitAll(Tasks);
            //for (int l = 0; l < Tasks.Length; l++) {
            //    if (l == index) {
            //        continue;
            //    }
            //    match += MatchTaskRoutine(ref i, l);
            //}
            //var mergedConflicts = Task.Run(() => GetConflictsRowsList(ConflictsDictionary));
            //mergedConflicts.Wait();
            //var list = mergedConflicts.Result;
            //match += MatchesEnumerableStringCompare(list, series, number);
        }
        return match;
    }
    private int MatchTaskRoutine(ref int i, int taskIndex) {
        int matches = Tasks[taskIndex].Result;
        i++;
        return matches;
    }
    private int GetMatchesFromBuffer(Memory<byte> buffer, byte[] searchPassport) {
        int matches = 0;
        int bufferIndex = 0;
        while (bufferIndex < buffer.Length) {
            if (bufferIndex + searchPassport.Length> buffer.Length) {
                bufferIndex += buffer.Length;
                break;
            }
            for (int i = 0; i < searchPassport.Length; i++) {
                if (buffer.Span[bufferIndex + i] == searchPassport[i]) {
                    if (i == searchPassport.Length - 1) {
                        matches++;
                        bufferIndex += i;
                        break;
                    }
                    continue;
                } else {
                    bufferIndex += i + 1;
                    break;
                }
            }
        }
        return matches;
    }
}

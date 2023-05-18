using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Repositories.Interfaces;
using System.Text;

namespace Passports.Repositories;

public class PassportRepository : GenericRepository<Passport>, IPassportRepository {
    private FileInfo _csvFileInfo =
        new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");

    private Task<List<Passport>>[] Tasks { get; set; }
    private Dictionary<int, string[]> ConflictsDictionary { get; set; }
    private Dictionary<int, int> TasksOrderMap { get; set; }
    private const int taskLimit = 5;
    private int all = 0;
    public PassportRepository(PassportContext context) : base(context) {
    }

    public override async Task<IEnumerable<Passport>> All() {
        try {
            return await _context.Passports.Select(x => x).ToListAsync();
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Passport?> GetBySeriesNumber(short series, int number) {
        try {
            return await _context.Passports.FirstOrDefaultAsync(p => p.Series == series && p.Number == number);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> MigrateFromFile() {
        int chunkSize = 120000;
        int headersOffset = 26;
        int bytesRead;
        int match = 0;
        int readIndex = 0;
        int lastTaskIndex = 0;
        int bufferIndex = 0;
        Tasks = new Task<List<Passport>>[taskLimit];
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None, chunkSize, useAsync:true)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            bytesRead = await fs.ReadAsync(buffer);
            var buffers = new Memory<byte>[taskLimit + 1];
            for (int k = 0; k < buffers.Length; k++) {
                buffers[k] = new Memory<byte>(new byte[chunkSize]);
            }

            while ((bytesRead = await fs.ReadAsync(buffers[bufferIndex])) > 0) {
                if (readIndex < taskLimit) {
                    var findTask = Task.Run(() => ParseChunk(buffers[bufferIndex].Span));
                    Tasks[readIndex] = findTask;
                    readIndex++;
                    bufferIndex = readIndex;
                } else {
                    all++;
                    lastTaskIndex = Task.WaitAny(Tasks);
                    MatchTaskRoutine(ref readIndex, lastTaskIndex);
                    if (lastTaskIndex >= 5) {
                    }

                    bufferIndex = lastTaskIndex;
                    Tasks[lastTaskIndex] = Task.Run(() => ParseChunk(buffers[bufferIndex].Span));
                    lastTaskIndex = Task.WaitAny(Tasks);
                    MatchTaskRoutine(ref readIndex, lastTaskIndex);
                }
            }
        }

        return true;
    }

    private void MatchTaskRoutine(ref int i, int taskIndex) {
        var passports = Tasks[taskIndex].Result;
        //FillDatabase(passports);
    }

    private List<Passport> ParseChunk(Span<byte> buffer) {
        List<Passport> passports = new();
        int firstPassportStart = buffer.IndexOf((byte)'\n') + 1;
        int lastPassportEnds = buffer.LastIndexOf((byte)'\n');
        int commapos = 0;
        int passportStart = firstPassportStart;
        var bufferEnter = Encoding.UTF8.GetString(buffer.Slice(0, 1));
        bool isReplaced = false;
        for (int i = firstPassportStart; i < lastPassportEnds; i++) {
            if (buffer[i] == (byte)',') {
                commapos = i;
            }

            if (i > 0) {
                var bufferCurrent =  Encoding.UTF8.GetString(buffer.Slice(0, 1));
                if (bufferEnter != bufferCurrent) {
                    isReplaced = true;
                }
            }

            string ErrorString;
            if (i > 10 && i < lastPassportEnds - 10) {
                ErrorString=Encoding.UTF8.GetString(buffer.Slice(i-10, 20));
            }

            if (buffer[i] == (byte)'\n') {
                if (commapos < passportStart) {
                }

                var seriesString = Encoding.UTF8.GetString(buffer.Slice(passportStart, commapos - passportStart));
                var numberString = Encoding.UTF8.GetString(buffer.Slice(commapos + 1, i - commapos - 1));
                short series = 0;
                int number = 0;
                if (short.TryParse(seriesString, out series) | int.TryParse(numberString, out number)) {
                    passports.Add(new Passport((short)series, number));
                }

                passportStart = i + 1;
            }

        }
        return passports;
    }

    private void FillDatabase(List<Passport> passports) {
        foreach (var passport in passports) {
            _context.Passports.Add(passport);
            _context.SaveChangesAsync();
        }
    }
}
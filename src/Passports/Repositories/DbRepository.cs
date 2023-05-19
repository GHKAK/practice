using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Passports.Data;
using Passports.Models;
using Passports.Repositories.Interfaces;

namespace Passports.Repositories;

public class DbRepository : GenericRepository<Passport>, IPassportRepository {
    private FileInfo _csvFileInfo =
        new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");

    private Task<List<Passport>>[] Tasks { get; set; }
    protected Dictionary<int, string[]> ConflictsDictionary { get; set; }
    protected Dictionary<int, int> TasksOrderMap { get; set; }
    private const int TaskLimit = 10;
    private MemoryPool _memoryPool;
    private List<List<Passport>> _listPool;
    private const int ChunkSize = 6000000;

    public DbRepository(PassportContext context) : base(context) {
        Tasks = new Task<List<Passport>>[TaskLimit];
        _memoryPool = new MemoryPool(TaskLimit + 1, ChunkSize);
        _listPool = new();
        for (int i = 0; i < TaskLimit; i++) {
            _listPool.Add(new List<Passport>(ChunkSize / 12));
        }
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
            return await _context.Passports.FindAsync(series, number);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> MigrateFromFile() {
        int headersOffset = 26;
        int readIndex = 0;
        int lastTaskIndex = 0;
        int bufferIndex = 0;
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None,
                   ChunkSize, useAsync: true)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            await fs.ReadAsync(buffer);
            var buffers = new List<Memory<byte>>();

            var bufferCurrent = _memoryPool.GetMemory();
            while ((await fs.ReadAsync(bufferCurrent)) > 0) {
                var bufferTransfer = bufferCurrent;
                if (readIndex < TaskLimit) {
                    var listIndex = readIndex;
                    var findTask = Task.Run(() => ParseChunk(bufferTransfer.Span, _listPool[listIndex]));
                    buffers.Add(bufferCurrent);
                    Tasks[readIndex] = findTask;
                    readIndex++;
                    bufferIndex = readIndex;
                } else {
                    lastTaskIndex = Task.WaitAny(Tasks);
                    await MatchTaskRoutine(lastTaskIndex);
                    bufferIndex = lastTaskIndex;
                    _memoryPool.ReturnMemory(buffers[lastTaskIndex]);
                    var listIndex = lastTaskIndex;
                    Tasks[lastTaskIndex] = Task.Run(() => ParseChunk(bufferTransfer.Span, _listPool[listIndex]));
                    buffers[lastTaskIndex] = bufferTransfer;
                }

                bufferCurrent = _memoryPool.GetMemory();
            }

            var x = await _context.Passports.Select(x => x).ToListAsync();
        }

        return true;
    }

    private async Task MatchTaskRoutine(int taskIndex) {
        var passports = Tasks[taskIndex].Result;
        await FillDatabase(passports);
    }

    protected List<Passport> ParseChunk(Span<byte> buffer, List<Passport> passports) {
        int firstPassportStart = buffer.IndexOf((byte)'\n') + 1;
        int lastPassportEnds = buffer.LastIndexOf((byte)'\n');
        int commapos = 0;
        int passportStart = firstPassportStart;
        bool isReplaced = false;
        for (int i = passports.Count; i < ChunkSize / 12; i++) {
            passports.Add(new Passport(0, 0));
        }

        int passportIndex = 0;
        for (int i = firstPassportStart; i < lastPassportEnds; i++) {
            if (buffer[i] == (byte)',') {
                commapos = i;
            }

            if (buffer[i] == (byte)'\n') {
                var seriesString = Encoding.UTF8.GetString(buffer.Slice(passportStart, commapos - passportStart));
                var numberString = Encoding.UTF8.GetString(buffer.Slice(commapos + 1, i - commapos - 1));

                short series;
                int number;
                if (short.TryParse(seriesString, out series) | int.TryParse(numberString, out number)) {
                    passports[passportIndex].Series = series;
                    passports[passportIndex].Number = number;
                }

                passportIndex++;
                passportStart = i + 1;
            }
        }

        for (int i = passports.Count - 1; i >= passportIndex; i--) {
            passports.RemoveAt(i);
        }

        return passports;
    }

    protected virtual async Task FillDatabase(List<Passport> passports) {
        await _context.BulkInsertAsync(passports, options => {
            options.ColumnPrimaryKeyExpression = x => new { x.Series, x.Number };
            options.InsertIfNotExists = true;
        });
        //await _context.Passports.AddRangeAsync(passports);
        //await _context.SaveChangesAsync();
    }
}
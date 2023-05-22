using System.Diagnostics;
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

    private Task<(List<Passport>, ConflictStrings)>[] Tasks { get; set; }
    private protected Dictionary<int, ConflictStrings> ConflictsDictionary { get; set; }
    private protected Dictionary<int, int> TasksOrderMap { get; set; }
    private const int TaskLimit = 10;
    private MemoryPool _memoryPool;
    private List<List<Passport>> _listPool;
    private List<Memory<byte>> _buffers;
    private const int ChunkSize = 6000000;
    private int _readIndex = 0;

    public DbRepository(PassportContext context) : base(context) {
        Tasks = new Task<(List<Passport>, ConflictStrings)>[TaskLimit];
        TasksOrderMap = new();
        ConflictsDictionary = new();
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
        _readIndex = 0;
        int headersOffset = 26;
        int lastTaskIndex = 0;
        using (FileStream fs = new FileStream(_csvFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.None,
                   ChunkSize, useAsync: true)) {
            Memory<byte> buffer = new Memory<byte>(new byte[headersOffset]);
            await fs.ReadAsync(buffer);
            _buffers = new List<Memory<byte>>();

            var bufferCurrent = _memoryPool.GetMemory();
            while ((await fs.ReadAsync(bufferCurrent)) > 0) {
                var bufferTransfer = bufferCurrent;
                if (_readIndex < TaskLimit) {
                    var listIndex = _readIndex;
                    var findTask = Task.Run(() => ParseChunk(bufferTransfer.Span, _listPool[listIndex]));
                    _buffers.Add(bufferCurrent);
                    Tasks[_readIndex] = findTask;
                    TasksOrderMap.Add(_readIndex, _readIndex);
                } else {
                    lastTaskIndex = Task.WaitAny(Tasks);
                    await EndTaskRoutine(lastTaskIndex);
                    _memoryPool.ReturnMemory(_buffers[lastTaskIndex]);
                    StartTask(lastTaskIndex, bufferTransfer);
                }

                _readIndex++;
                bufferCurrent = _memoryPool.GetMemory();
            }

            Task.WaitAll(Tasks);
            await ProcessLastTasks(lastTaskIndex);
            await InsertConflicts();
        }

        return true;
    }

    private async Task InsertConflicts() {
        List<Passport> passports = new();
        var prevConflict = ConflictsDictionary.GetValueOrDefault(0);
        passports.Add(GetPassportFromString(prevConflict.Start));
        for (int i = 1; i < ConflictsDictionary.Count; i++) {
            var nextConflict = ConflictsDictionary.GetValueOrDefault(i);
            try {
                passports.Add(GetPassportFromString(prevConflict.End + nextConflict.Start));
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            prevConflict = nextConflict;
        }

        await FillDatabase(passports);
    }

    private Passport GetPassportFromString(string passportString) {
        var splitted = passportString.Split(',');
        short series;
        int number;
        if (short.TryParse(splitted[0], out series) && int.TryParse(splitted[1], out number)) {
            return new Passport(series, number);
        }

        throw new ArgumentException("Not correct data in string");
    }

    private void StartTask(int lastTaskIndex, Memory<byte> bufferTransfer) {
        var listIndex = lastTaskIndex;
        TasksOrderMap[lastTaskIndex] = _readIndex;
        Tasks[lastTaskIndex] = Task.Run(() => ParseChunk(bufferTransfer.Span, _listPool[listIndex]));
        _buffers[lastTaskIndex] = bufferTransfer;
    }

    private async Task EndTaskRoutine(int taskIndex) {
        (var passports, var conflict) = Tasks[taskIndex].Result;
        ConflictsDictionary.Add(TasksOrderMap[taskIndex], conflict);
        await FillDatabase(passports);
    }

    private async Task ProcessLastTasks(int lastTaskIndex) {
        Task.WaitAll(Tasks);
        for (int l = 0; l < Tasks.Length; l++) {
            await EndTaskRoutine(l);
            _readIndex++;
        }
    }

    protected (List<Passport>, ConflictStrings) ParseChunk(Span<byte> buffer, List<Passport> passports) {
        int firstPassportStart = buffer.IndexOf((byte)'\n') + 1;
        int lastPassportEnds = buffer.LastIndexOf((byte)'\n');
        int commapos = 0;
        int passportStart = firstPassportStart;
        for (int i = passports.Count; i < ChunkSize / 12; i++) {
            passports.Add(new Passport(0, 0));
        }

        string start = Encoding.UTF8.GetString(buffer.Slice(0, passportStart - 1));
        string end = Encoding.UTF8.GetString(buffer.Slice(lastPassportEnds + 1, buffer.Length - lastPassportEnds - 1));

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
                if (short.TryParse(seriesString, out series) && int.TryParse(numberString, out number)) {
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

        return (passports, new ConflictStrings(start, end));
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
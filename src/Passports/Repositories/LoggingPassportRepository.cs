using System.Diagnostics;
using System.Reflection;
using Passports.Models;
using Passports.Repositories.Interfaces;
using Serilog.Core;
namespace Passports.Repositories; 

public class LoggingPassportRepository : IPassportRepository {
    private readonly IPassportRepository _decoratedRepository;
    private readonly Serilog.ILogger _logger;
    private readonly Stopwatch _sw;

    public LoggingPassportRepository(IPassportRepository decoratedRepository, Serilog.ILogger logger) {
        _decoratedRepository = decoratedRepository;
        _logger = logger;
        _sw = new Stopwatch();
    }

    public async Task<Passport?> GetBySeriesNumber(short series, int number) {
        return await LogAndExecute(()=>_decoratedRepository.GetBySeriesNumber(series,number),MethodBase.GetCurrentMethod().Name);
    }

    public async Task<int> CountActual(bool isActual) {
        return await LogAndExecute(()=>_decoratedRepository.CountActual(isActual),MethodBase.GetCurrentMethod().Name);
    }

    public async Task<bool> MigrateFromFile() {
        return await LogAndExecute(_decoratedRepository.MigrateFromFile,MethodBase.GetCurrentMethod().Name);
    }
    private async Task<T> LogAndExecute<T>(Func<Task<T>> method, string methodName)
    {
        _sw.Restart();
        try {
            var result = await method.Invoke();
            _sw.Stop();            
            _logger.Information($"{methodName} finished in {_sw.Elapsed} seconds");
            return result;
        } catch {
            _sw.Stop();
            _logger.Information($"{methodName} finished in {_sw.Elapsed} seconds");
            throw;
        }
    }
}
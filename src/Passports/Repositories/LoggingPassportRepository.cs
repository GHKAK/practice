using System.Diagnostics;
using Passports.Models;
using Passports.Repositories.Interfaces;

namespace Passports.Repositories; 

public class LoggingPassportRepository : IPassportRepository {
    private readonly IPassportRepository _decoratedRepository;
    private readonly ILogger _logger;
    private readonly Stopwatch _sw;

    public LoggingPassportRepository(IPassportRepository decoratedRepository, ILogger logger) {
        _decoratedRepository = decoratedRepository;
        _logger = logger;
        _sw = new Stopwatch();
    }

    public async Task<Passport?> GetBySeriesNumber(short series, int number) {
        return await LogAndExecute(()=>_decoratedRepository.GetBySeriesNumber(series,number));
    }

    public async Task<int> CountActual(bool isActual) {
        return await LogAndExecute(()=>_decoratedRepository.CountActual(isActual));
    }

    public async Task<bool> MigrateFromFile() {
        return await LogAndExecute(_decoratedRepository.MigrateFromFile);
    }
    private async Task<T> LogAndExecute<T>(Func<Task<T>> method)
    {
        _sw.Restart();
        try {
            var result = await method.Invoke();
            _sw.Stop();            
            _logger.LogInformation($"{method} finished in {_sw.Elapsed} seconds");
            return result;
        } catch {
            _sw.Stop();
            _logger.LogInformation($"{method} finished in {_sw.Elapsed} seconds");
            throw;
        }
    }
}
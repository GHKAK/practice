using Passports.Models;

namespace Passports.Repositories.Interfaces; 

public interface IPassportRepository : IGenericRepository<Passport> {
    Task<Passport?> GetBySeriesNumber(short series, int number);
    Task<int> CountActual(bool isActual);
    Task<bool> MigrateFromFile();
}
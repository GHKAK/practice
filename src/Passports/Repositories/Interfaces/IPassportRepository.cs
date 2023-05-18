using Passports.Models;

namespace Passports.Repositories.Interfaces; 

public interface IPassportRepository : IGenericRepository<Passport> {
    Task<Passport?> GetBySeriesNumber(short series, int number);
    Task<bool> MigrateFromFile();
}
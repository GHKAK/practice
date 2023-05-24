using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;
using StackExchange.Redis;

namespace Passports.Data; 

public class UnitOfWorkRedis :IUnitOfWork {
    private readonly IConnectionMultiplexer _redis;
    public IPassportRepository Passports { get; private set; }
    public UnitOfWorkRedis(IConnectionMultiplexer redis, IPassportRepository passports) {
        _redis = redis;
        Passports = passports;
    }
    public async Task CompleteAsync() {
        throw new NotImplementedException();
    }
}
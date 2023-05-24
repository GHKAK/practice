using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Passports.Data;
using Passports.Models;
using Passports.Repositories.Interfaces;
using StackExchange.Redis;

namespace Passports.Repositories;

public class RedisRepository : DbRepository {
    private readonly IConnectionMultiplexer _redis;

    public RedisRepository(IConnectionMultiplexer redis) {
        _redis = redis;
    }

    public override async Task<Passport?> GetBySeriesNumber(short series, int number) {
        try {
            var db = _redis.GetDatabase();
            string key = $"{series}-{number}";
            var passport = db.StringGet(key);
            return JsonConvert.DeserializeObject<Passport>(passport);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task<int> CountActual(bool isActual) {
        throw new NotImplementedException();
        //return count;
    }

    protected override async Task FillDatabase(List<Passport> passports) {
        var db = _redis.GetDatabase();
        foreach (var passport in passports) {
            string key = $"{passport.Series}-{passport.Number}";
            string value = JsonConvert.SerializeObject(passport);

            await db.StringSetAsync(key, value);
        }
    }
}
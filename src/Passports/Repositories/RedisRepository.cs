using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Passports.Data;
using Passports.Models;
using Passports.Repositories.Interfaces;
using StackExchange.Redis;

namespace Passports.Repositories;

public class RedisRepository : DbRepository {
    private const string ActiveCountKey = "active";
    private const string UnactiveCountKey = "unactive";

    private readonly IConnectionMultiplexer _redis;

    public RedisRepository(IConnectionMultiplexer redis) {
        _redis = redis;
        var db = _redis.GetDatabase();
        if (!db.StringGet(ActiveCountKey).HasValue) {
            db.StringSet(ActiveCountKey, 0);
            db.StringSet(UnactiveCountKey, 0);
        }
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
        var db = _redis.GetDatabase();
        int result;
        if (isActual) {
            var active = await db.StringGetAsync(ActiveCountKey);
            active.TryParse(out result);
        } else {
            var unactive = await db.StringGetAsync(UnactiveCountKey);
            unactive.TryParse(out result);
        }
        return result;
    }

    protected override async Task FillDatabase(List<Passport> passports) {
        var db = _redis.GetDatabase();
        var DateString = DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "");
        long dateNumber;
        Int64.TryParse(DateString, out dateNumber);
        int activeCount = 0, unactiveCount = 0;
        foreach (var passport in passports) {
            string key = $"{passport.Series}-{passport.Number}";
            string value = JsonConvert.SerializeObject(passport);
            if (passport.IsActual) {
                activeCount++;
            } else {
                unactiveCount++;
            } 
            db.SortedSetAddAsync(key, value, (double)dateNumber);
        }
        await db.StringIncrementAsync(ActiveCountKey, activeCount);
        await db.StringIncrementAsync(UnactiveCountKey,unactiveCount);
    }
}
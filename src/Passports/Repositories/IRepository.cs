using Passports.Models;

namespace Passports.Repositories {
    public interface IRepository {
        List<Passport> ReadAll();
    }
}

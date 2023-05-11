using Passports.Models;

namespace Passports.Repositories {
    public class PostgresRepository : IRepository {
        private PassportContext _context;
        public PostgresRepository(PassportContext context) {
            _context = context;
        }
        public List<Passport> ReadAll() {
            return _context.Passports.ToList();
        }
        public void WriteAll(List<Passport> passports) {
            int chunkLength = 100000;
            int totalPassports = passports.Count;
            for (int i = 0; i < totalPassports; i += chunkLength) {
                int remainingPassports = totalPassports - i;
                int passportsToAdd = Math.Min(chunkLength, remainingPassports);

                for (int j = i; j < i + passportsToAdd; j++) {
                    try {
                        _context.Passports.Add(passports[j]);
                    } catch {
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}

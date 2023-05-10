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
            int i = 0;
            int chunkLength = 100000;
            while(i < passports.Count) {
                for(int j = i; j < i + chunkLength; j++) {
                    try {
                        _context.Passports.Add(passports[j]);

                    } catch(Exception e) {
                    }
                }
                i += chunkLength;
                _context.SaveChanges();
            }
            for(int j = i - chunkLength; j < passports.Count; j++) {
                try {
                    _context.Passports.Add(passports[j]);

                } catch(Exception e) {
                }
            }
            _context.SaveChanges();
        }
    }
}

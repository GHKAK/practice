using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;

namespace Passports.Data; 

public class UnitOfWork :IUnitOfWork {
    private readonly PassportContext _context;
    public IPassportRepository Passports { get; private set; }
    public UnitOfWork(PassportContext context) {
        _context = context;
        Passports = new PassportRepository(context);
    }
    public async Task CompleteAsync() {
        await _context.SaveChangesAsync();
    }
}
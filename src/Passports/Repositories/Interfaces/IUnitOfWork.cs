namespace Passports.Repositories.Interfaces;

public interface IUnitOfWork {
    IPassportRepository Passports { get; }

    Task CompleteAsync();
}
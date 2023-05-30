namespace Passports.Models; 

public interface IAuditablePassport {
    bool IsActual { get; set; }
    DateOnly ChangeDate { get; set; }
}
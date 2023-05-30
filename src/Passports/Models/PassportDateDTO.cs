namespace Passports.Models; 

public struct PassportDateDTO {
    public bool isActual;
    public DateOnly changeDate;
    public PassportDateDTO(Passport passport) {
        isActual = passport.IsActual;
        changeDate = passport.ChangeDate;
    }
    public PassportDateDTO(IAuditablePassport passport) {
        isActual = passport.IsActual;
        changeDate = passport.ChangeDate;
    }
}
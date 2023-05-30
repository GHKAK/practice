using CsvHelper.Configuration.Attributes;

namespace Passports.Models;

public class Passport : IAuditablePassport {
    public Passport(short series, int number) {
        Series = series;
        Number = number;
        ChangeDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    }

    public short Series { get; set; }
    public int Number { get; set; }
    public bool IsActual { get; set; }
    public DateOnly ChangeDate { get; set; }
}
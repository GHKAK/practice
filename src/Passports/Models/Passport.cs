using CsvHelper.Configuration.Attributes;

namespace Passports.Models {
    public class Passport {
        public Passport(short series, int number) {
            Series = series;
            Number = number;
        }
        public short Series { get; private set; }
        public int Number { get; private set; }
    }
}

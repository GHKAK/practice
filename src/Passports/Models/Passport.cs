using CsvHelper.Configuration.Attributes;

namespace Passports.Models {
    public class Passport {
        public int Series { get; set; }
        public int Number { get; set; }
    }
}

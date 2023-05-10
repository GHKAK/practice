using CsvHelper.Configuration.Attributes;

namespace Passports.Models {
    public class Passport {
        [Index(0)]
        public int Series { get; set; }
        [Index(1)]
        public int Number { get; set; }
    }
}

using CsvHelper.Configuration.Attributes;

namespace Passports.Models {
    public class Passport {
        [Index(0)]
        public string? Series { get; set; }
        [Index(1)]
        public string? Number { get; set; }
    }
}

using CsvHelper.Configuration;

namespace Passports.Models {
    public class PassportMap : ClassMap<Passport> {
        public PassportMap() {
            Map(m => m.Series).Name("PASSP_SERIES").Validate(field => int.TryParse(field.Field, out _)) ;
            Map(m => m.Number).Name("PASSP_NUMBER").Validate(field => int.TryParse(field.Field, out _));
        }
    }
}

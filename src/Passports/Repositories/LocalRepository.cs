using CsvHelper;
using CsvHelper.Configuration;
using ICSharpCode.SharpZipLib.BZip2;
using Passports.Models;
using System.Globalization;
using System.Text;

namespace Passports.Repositories {
    public class LocalRepository :IRepository {
        private FileInfo _bzFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv.bz2");
        private FileInfo _csvFileInfo = new FileInfo($@"C:\Users\{System.Environment.UserName}\Downloads\list_of_expired_passports.csv");
        private List<Passport> _records = new();
        public string readedStr;
        public List<Passport> ReadAll() {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                Delimiter = ",",
                MissingFieldFound = null,
                ReadingExceptionOccurred = args => {
                    if(args.Exception is CsvHelper.FieldValidationException)
                        return false;

                    return true;
                }
            };
            using(var reader = new StreamReader(_csvFileInfo.FullName, Encoding.UTF8))
            using(var csv = new CsvReader(reader, configuration)) {
                csv.Context.RegisterClassMap<PassportMap>();
                var records = csv.GetRecords<Passport>();
                _records = records.ToList();
            }
            return _records;
        }
        public void DecompressFile() {
            using(FileStream fileToDecompressAsStream = _bzFileInfo.OpenRead()) {
                string decompressedFileName = _csvFileInfo.FullName;
                using(FileStream decompressedStream = File.Create(decompressedFileName)) {
                    try {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    } catch(Exception ex) {
                        throw;
                    }
                }
            }
        }
    }
}

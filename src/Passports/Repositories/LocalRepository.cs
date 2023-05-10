using ICSharpCode.SharpZipLib.BZip2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Formats.Asn1;
using System;
using System.Runtime.Intrinsics.X86;
using System.Text;
using CsvHelper;
using System.Globalization;
using Passports.Models;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;

namespace Passports.Repositories {
    public class LocalRepository {
        private FileInfo _bzFileInfo = new FileInfo(@"C:\Users\user\Downloads\list_of_expired_passports.csv.bz2");
        private FileInfo _csvFileInfo = new FileInfo(@"C:\Users\user\Downloads\list_of_expired_passports.csv");
        private List<Passport> _records = new();
        private List<string> _badRecords = new List<string>();
        public string readedStr;
        public void ReadFile() {
            //DecompressFile();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                Delimiter = ",",
                ShouldSkipRecord = args => Single.TryParse(args.Row.GetField(0), out _) && Single.TryParse(args.Row.GetField(1), out _),
            };
            configuration.MissingFieldFound = null;
            configuration.BadDataFound = context => {
                //if (!(Single.TryParse(context.RawRecord.ToString()),out _)) {
                //    // Handle the specific conversion error for the "Series" property
                //    context.SkipThisRecord = true; // Skip the row
                //}
            };
            using (var reader = new StreamReader(_csvFileInfo.FullName, Encoding.UTF8))
            using (var csv = new CsvReader(reader, configuration)) {
                while (csv.Read()) {
                    try {
                        var record = csv.GetRecord<Passport>();
                        _records.Add(record);
                    } catch {
                        continue;
                    }
                }

                //var records = csv.GetRecords<Passport>();
                //foreach (var item in records) {
                //    _records.Add(item);
                //}

            }

        }
        public void DecompressFile() {
            using (FileStream fileToDecompressAsStream = _bzFileInfo.OpenRead()) {
                string decompressedFileName = _csvFileInfo.FullName;
                using (FileStream decompressedStream = File.Create(decompressedFileName)) {
                    try {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    } catch (Exception ex) {
                        throw;
                    }
                }
            }
        }
    }
}

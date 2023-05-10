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

namespace Passports.Repositories {
    public class LocalRepository {
        private FileInfo _bzFileInfo = new FileInfo(@"C:\Users\user\Downloads\list_of_expired_passports.csv.bz2");
        private FileInfo _csvFileInfo = new FileInfo(@"C:\Users\user\Downloads\list_of_expired_passports.csv");
        private List<Passport> _records = new();
        public string readedStr;
        public void ReadFile() {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                Delimiter = ","
            };
            configuration.MissingFieldFound = null;
            using (var reader = new StreamReader(_csvFileInfo.FullName, Encoding.UTF8))
            using (var csv = new CsvReader(reader, configuration)) {
                try {
                    var records = csv.GetRecords<Passport>();
                    foreach (var item in records) {
                        _records.Add(item);
                    }
                } catch {
                    throw;
                }
            }

            //DecompressFile();
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

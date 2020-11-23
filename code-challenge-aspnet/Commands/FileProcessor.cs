using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace code_challenge_aspnet.Commands
{
    public static class FileProcessor
    {
        public static List<T> InportCsv<T>(string filepath)
        {
            try
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<T>();
                    return records.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }  
        }

        public static void ExportCsv<T>(string filepath, IEnumerable<T> records)
        {
            try
            {
                using (var writer = new StreamWriter(filepath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

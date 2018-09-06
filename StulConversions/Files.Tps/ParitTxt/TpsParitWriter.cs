using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Files.Tps.ParitTxt
{
    public class TpsParitWriter
    {
        private const string DefaultCsvDelimiter = ",";

        private string ParitFileName { get; }
        public IList<TpsParitLine> ParitLines { get; set; }

        public TpsParitWriter(string paritFileName)
        {
            ParitFileName = paritFileName;
            ParitLines = new List<TpsParitLine>();
        }

        public void WriteAll()
        {
            using (StreamWriter stream = new StreamWriter(File.Open(ParitFileName, FileMode.Create), Encoding.UTF8))
            {
                var writer = new CsvWriter(stream);
                writer.Configuration.Delimiter = DefaultCsvDelimiter;
                writer.Configuration.QuoteAllFields = true;
                writer.Configuration.HasHeaderRecord = true;
                writer.Configuration.RegisterClassMap<TpsParitLineClassMap>();

                writer.WriteHeader<TpsParitLine>();
                foreach (TpsParitLine pari in ParitLines)
                {
                    writer.WriteRecord<TpsParitLine>(pari);
                    writer.NextRecord();
                }
            }
        }
    }
}

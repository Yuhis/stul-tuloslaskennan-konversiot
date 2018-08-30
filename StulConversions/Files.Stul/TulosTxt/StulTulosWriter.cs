using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Files.Stul.TulosTxt
{
    public class StulTulosWriter
    {
        private const string DefaultCsvDelimiter = ",";

        private string TulosFileName { get; }
        public IList<StulTulosLine> TulosLines { get; set; }

        public StulTulosWriter(string tulosFileName)
        {
            TulosFileName = tulosFileName;
            TulosLines = new List<StulTulosLine>();
        }

        public void WriteAll()
        {
            var outFileStream = File.Create(TulosFileName);
            using (StreamWriter stream = new StreamWriter(outFileStream, Encoding.UTF8))
            {
                var writer = new CsvWriter(stream);
                writer.Configuration.Delimiter = DefaultCsvDelimiter;
                writer.Configuration.QuoteAllFields = true;
                writer.Configuration.RegisterClassMap<StulTulosLineClassMap>();

                foreach (StulTulosLine tulos in TulosLines.OrderBy(t => t.CoupleNumber))
                {
                    writer.WriteRecord<StulTulosLine>(tulos);
                    writer.NextRecord();
                }
            }
        }
    }
}

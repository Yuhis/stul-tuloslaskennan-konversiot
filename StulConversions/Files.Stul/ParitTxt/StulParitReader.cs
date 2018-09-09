using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Files.Stul.ParitTxt
{
    public class StulParitReader
    {
        private string ParitFileName { get; }
        public IList<StulParitLine> ParitLines { get; set; }
        public IList<string> LineErrors { get; set; }

        public StulParitReader(string paritFileName)
        {
            ParitFileName = paritFileName;
            ParitLines = new List<StulParitLine>();
            LineErrors = new List<string>();
            ReadAll();
        }

        private void ReadAll()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (FileStream fs = File.Open(ParitFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader stream = new StreamReader(fs, Encoding.GetEncoding(1252)))
                {
                    var reader = new CsvReader(stream);
                    reader.Configuration.HasHeaderRecord = false;
                    reader.Configuration.RegisterClassMap<StulParitLineClassMap>();

                    var records = reader.GetRecords<StulParitLine>();
                    foreach (StulParitLine pari in records)
                    {
                        ParitLines.Add(pari);
                        foreach (string e in pari.Errors)
                        {
                            LineErrors.Add(e);
                        }
                    }
                }
            }
        }
    }
}

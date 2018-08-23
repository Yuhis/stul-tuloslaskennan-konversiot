﻿using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StulFiles.StulParitTxt
{
    class StulParitReader
    {
        private const string DefaultCsvDelimiter = ",";
        private string ParitFileName { get; set; }
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
            var inFileStream = System.IO.File.Open(ParitFileName, FileMode.Open, FileAccess.Read);
            using (StreamReader stream = new StreamReader(inFileStream, Encoding.UTF8))
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
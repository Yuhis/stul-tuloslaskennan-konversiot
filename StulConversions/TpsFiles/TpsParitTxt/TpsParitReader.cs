using System.Collections.Generic;

namespace TpsFiles.TpsParitTxt
{
    public class TpsParitReader
    {
        private const string DefaultCsvDelimiter = ",";
        private string ParitFileName { get; set; }
        public IList<TpsParitLine> ParitLines { get; set; }

        public TpsParitReader(string paritFileName)
        {
            ParitFileName = paritFileName;
            ParitLines = new List<TpsParitLine>();
        }
    }
}

using System.Collections.Generic;

namespace Files.Tps.ParitTxt
{
    public class TpsParitReader
    {
        private string ParitFileName { get; }
        public IList<TpsParitLine> ParitLines { get; set; }

        public TpsParitReader(string paritFileName)
        {
            ParitFileName = paritFileName;
            ParitLines = new List<TpsParitLine>();
        }
    }
}

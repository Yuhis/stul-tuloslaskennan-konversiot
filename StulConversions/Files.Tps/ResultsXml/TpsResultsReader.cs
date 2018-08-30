using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Files.Tps.ResultsXml
{
    public class TpsResultsReader
    {
        public TpsEventElement Event { get; }

        public TpsResultsReader(string resultFileName)
        {
            XDocument xdoc = XDocument.Parse(File.ReadAllText(resultFileName, Encoding.UTF8));
            Event = new TpsEventElement(xdoc.Element("Event"));
        }
    }
}

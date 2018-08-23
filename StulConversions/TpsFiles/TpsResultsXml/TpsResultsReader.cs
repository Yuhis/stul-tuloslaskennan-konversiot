using System.IO;
using System.Text;
using System.Xml.Linq;

namespace TpsFiles.TpsResultsXml
{
    public class TpsResultsReader
    {
        public TpsEventElement Event { get; private set; }

        public TpsResultsReader(string resultFileName)
        {
            XDocument xdoc = XDocument.Parse(File.ReadAllText(resultFileName, Encoding.UTF8));
            Event = new TpsEventElement(xdoc.Element("Event"));
        }
    }
}

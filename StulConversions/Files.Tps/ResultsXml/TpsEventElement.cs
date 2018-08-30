using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Files.Tps.ResultsXml
{
    public class TpsEventElement
    {
        public IList<TpsResultsElement> Results { get; set; }

        public TpsEventElement(XElement xe)
        {
            Results = new List<TpsResultsElement>();
            var resultElements = from el in xe.Elements("Results") select el;
            foreach (XElement r in resultElements)
            {
                Results.Add(new TpsResultsElement(r));
            }
        }
    }
}

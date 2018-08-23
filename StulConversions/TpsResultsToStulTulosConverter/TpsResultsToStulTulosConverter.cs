using StulFiles.StulParitTxt;
using System;
using System.Collections.Generic;
using System.IO;
using TpsFiles.TpsResultsXml;

namespace TpsResultsToStulTulosConverter
{
    public class TpsResultsToStulTulosConverter
    {
        private StulParitReader _stulParitReader;
        private TpsResultsReader _tpsResultsReader;
        private string _stulTulosFileName = "";
        private IDictionary<string, TpsEvent> _tpsEvents;

        public TpsResultsToStulTulosConverter(string stulParitFileName, string tpsResultsFileName, string stulTulosFileName)
        {
            // parit.txt tiedosto
            if (!File.Exists(stulParitFileName))
            {
                Console.WriteLine($"Virhe: Tiedostoa {stulParitFileName} ei löydy.");
                Environment.Exit(-1);
            }
            _stulParitReader = new StulParitReader(stulParitFileName);
            foreach (string e in _stulParitReader.LineErrors)
            {
                Console.WriteLine($"Virhe: {e}");
            }

            // result.xml tiedosto
            if (!File.Exists(tpsResultsFileName))
            {
                Console.WriteLine($"Virhe: Tiedostoa {tpsResultsFileName} ei löydy.");
                Environment.Exit(-1);
            }
            _tpsResultsReader = new TpsResultsReader(tpsResultsFileName);

            // tulos.txt tiedoston nimi
            _stulTulosFileName = stulTulosFileName;

            _tpsEvents = new Dictionary<string, TpsEvent>();
        }
    }
}

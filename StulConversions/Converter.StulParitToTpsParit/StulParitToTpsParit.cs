using System;
using System.IO;
using Files.Stul.ParitTxt;
using Files.Tps.ParitTxt;

namespace Converter.StulParitToTpsParit
{
    public class StulParitToTpsParit
    {
        private readonly StulParitReader _stulParitReader;
        private readonly TpsParitWriter _tpsParitWriter;

        public StulParitToTpsParit(string stulParitFileName, string tpsParitFileName)
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
                Console.WriteLine($"Virhe tiedostossa: {stulParitFileName}: {e}");
            }

            // parit.tps.txt tiedosto
            _tpsParitWriter = new TpsParitWriter(tpsParitFileName);
        }

        public void WriteTpsParit()
        {
            foreach (StulParitLine pari in _stulParitReader.ParitLines)
            {
                var tpspari = new TpsParitLine()
                {
                    FirstNameMan = pari.ManFirstName,
                    LastNameMan = pari.ManLastName,
                    FirstNameWoman = pari.WomanFirstName,
                    LastNameWoman = pari.WomanLastName,
                    Club = $"{pari.ClubName} / {pari.ClubTown}",
                    MinMan = pari.CoupleCode,
                    MinWoman = pari.CoupleCode
                };
                _tpsParitWriter.ParitLines.Add(tpspari);
            }

            _tpsParitWriter.WriteAll();
        }
    }
}

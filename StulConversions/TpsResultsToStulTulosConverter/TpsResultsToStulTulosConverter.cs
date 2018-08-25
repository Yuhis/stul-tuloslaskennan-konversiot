using StulFiles.StulParitTxt;
using StulFiles.StulTulosTxt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public void WriteTulosTxt()
        {
            SetResults();

            if (0 == _tpsEvents.Count)
            {
                Console.WriteLine("Ei tuloksia");
                return;
            }

            bool multipleEvents = _tpsEvents.Count > 1;

            foreach (KeyValuePair<string, TpsEvent> tpsEvent in _tpsEvents)
            {
                string filename = _stulTulosFileName;
                if (multipleEvents)
                {
                    filename = tpsEvent.Key + _stulTulosFileName;
                }
                WriteTulosTxtForEvent(tpsEvent.Value, filename);
            }
        }

        private void SetResults()
        {
            foreach (TpsResultsElement element in _tpsResultsReader.Event.Results)
            {
                if (!_tpsEvents.ContainsKey(element.EventCode))
                {
                    _tpsEvents[element.EventCode] = new TpsEvent(element.EventCode);
                }
                TpsEvent tpsEvent = _tpsEvents[element.EventCode];

                TpsCompetition.TpsCompetitionType tpsCompetitionType;
                switch (element.CompetitionType)
                {
                    case (TpsResultsElement.CompetitionTypeCode.TenDance):
                        tpsCompetitionType = TpsCompetition.TpsCompetitionType.TenDance;
                        break;
                    case (TpsResultsElement.CompetitionTypeCode.Standard):
                        tpsCompetitionType = TpsCompetition.TpsCompetitionType.Standard;
                        break;
                    case (TpsResultsElement.CompetitionTypeCode.Latin):
                        tpsCompetitionType = TpsCompetition.TpsCompetitionType.Latin;
                        break;
                    default:
                        Console.WriteLine($"Virhe: Tuntematon kilpailun tyyppi: {element.CompetitionType}");
                        tpsCompetitionType = TpsCompetition.TpsCompetitionType.TenDance;
                        break;
                }
                TpsCompetition tpsCompetition = tpsEvent.Competitions.SingleOrDefault(
                    c => c.CompetitionCode == element.CompetitionCode && c.CompetitionType == tpsCompetitionType);
                if (null == tpsCompetition)
                {
                    tpsCompetition = new TpsCompetition(element.CompetitionCode, tpsCompetitionType);
                    tpsCompetition.CompetitionLevel = element.CompetitionLevel;
                    tpsCompetition.TotalCouples = element.TotalCouples;
                    tpsEvent.Competitions.Add(tpsCompetition);
                }
                // TODO: Else check for property consistency

                TpsCouple.TpsMissing tpsMissing;
                switch (element.CoupleMissing)
                {
                    case TpsResultsElement.MissingCode.NotMissing:
                        tpsMissing = TpsCouple.TpsMissing.NotMissing;
                        break;
                    case TpsResultsElement.MissingCode.Dancing:
                        tpsMissing = TpsCouple.TpsMissing.Dancing;
                        break;
                    case TpsResultsElement.MissingCode.Excused:
                        tpsMissing = TpsCouple.TpsMissing.Excused;
                        break;
                    case TpsResultsElement.MissingCode.Missing:
                        tpsMissing = TpsCouple.TpsMissing.Missing;
                        break;
                    default:
                        Console.WriteLine($"Virhe: Tuntematon 'missing'arvo: {element.CoupleMissing}");
                        tpsMissing = TpsCouple.TpsMissing.NotMissing;
                        break;
                }
                TpsCouple tpsCouple = new TpsCouple(element.CoupleCode);
                tpsCouple.CoupleNumber = element.CoupleNumber;
                tpsCouple.CoupleNames = element.CoupleNames;
                tpsCouple.Position1 = element.Position1;
                tpsCouple.Position2 = element.Position2;
                tpsCouple.Missing = tpsMissing;
                tpsCouple.RoundsDanced = element.RoundsDanced;

                int coupleRoundNbr = tpsCouple.RoundsDanced;
                TpsCompetition.TpsCompetitionRound round = tpsCompetition.Rounds.SingleOrDefault(r => r.RoundNumber == coupleRoundNbr);
                if (null == round)
                {
                    round = new TpsCompetition.TpsCompetitionRound(coupleRoundNbr);
                    tpsCompetition.Rounds.Add(round);
                }
                round.Couples.Add(tpsCouple);
            }

            foreach (KeyValuePair<string, TpsEvent> eventItem in _tpsEvents)
            {
                var EventCoupleResults = eventItem.Value.CoupleResults;
                foreach (TpsCompetition tpsCompetition in eventItem.Value.Competitions)
                {
                    foreach (TpsCompetition.TpsCompetitionRound round in tpsCompetition.Rounds)
                    {
                        foreach (TpsCouple couple in round.Couples)
                        {
                            TpsCoupleResult coupleResult = EventCoupleResults.SingleOrDefault(
                                p => p.CoupleCode == couple.CoupleCode &&
                                     string.Equals(p.CompetitionCode, tpsCompetition.CompetitionCode) &&
                                     p.CoupleNumber == couple.CoupleNumber);
                            if (null == coupleResult)
                            {
                                coupleResult = new TpsCoupleResult(tpsCompetition.CompetitionCode, couple.CoupleCode, couple.CoupleNumber)
                                {
                                    CoupleNames = couple.CoupleNames
                                };
                                EventCoupleResults.Add(coupleResult);
                            }
                            string result = "";
                            if (couple.RoundsDanced == tpsCompetition.Rounds.Count)
                            {
                                // Final round
                                if (couple.RoundsDanced > 1)
                                {
                                    result = $"{couple.Position1}/{round.Couples.Count}+{couple.RoundsDanced - 1}";
                                }
                                else
                                {
                                    result = $"{couple.Position1}/{round.Couples.Count}";
                                }
                            }
                            else
                            {
                                // Qualification round
                                result = $"+{couple.RoundsDanced}";
                            }
                            if (tpsCompetition.CompetitionType == TpsCompetition.TpsCompetitionType.Latin)
                            {
                                if (!string.IsNullOrEmpty(coupleResult.Result2))
                                {
                                    Console.WriteLine($"Virhe: Parilla {coupleResult.CoupleCode} on jo tulos 2 {coupleResult.Result2}");
                                }
                                coupleResult.Result2 = result;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(coupleResult.Result1))
                                {
                                    Console.WriteLine($"Virhe: Parilla {coupleResult.CoupleCode} on jo tulos 1 {coupleResult.Result1}");
                                }
                                coupleResult.Result1 = result;
                            }
                        }
                    }
                }
            }

        }

        private void WriteTulosTxtForEvent(TpsEvent tpsEvent, string tulosTxtFileName)
        {
            string eventName = "(tyhjä)";
            if (!string.IsNullOrEmpty(tpsEvent.EventCode))
            {
                eventName = tpsEvent.EventCode;
            }
            Console.WriteLine($"Tapahtuma: {eventName}");
            Console.WriteLine($"Tulostiedosto: {tulosTxtFileName}");
            StulTulosWriter stulTulosWriter = new StulTulosWriter(tulosTxtFileName);

            foreach (TpsCoupleResult coupleResult in tpsEvent.CoupleResults)
            {
                StulParitLine stulPari = _stulParitReader.ParitLines.SingleOrDefault(p => p.CoupleCode == coupleResult.CoupleCode);
                if (null == stulPari)
                {
                    stulPari = new StulParitLine()
                    {
                        CoupleCode = coupleResult.CoupleCode,
                        CoupleNames = coupleResult.CoupleNames // TODO: Convert format
                    };
                    Console.WriteLine($"Virhe: Paria {coupleResult.CoupleNames} ei ole STUL paritiedostossa");
                }

                var tulos = new StulTulosLine()
                {
                    CoupleCode = coupleResult.CoupleCode,
                    CoupleNumber = coupleResult.CoupleNumber,
                    CoupleNames = stulPari.CoupleNames,
                    ClubName = stulPari.ClubName,
                    ClubTown = stulPari.ClubTown,
                    AgeGroup = "", // TODO: Competition age group here
                    Category = "", // TODO: Competition category here
                    Result1 = coupleResult.Result1,
                    Result2 = coupleResult.Result2
                };
                stulTulosWriter.TulosLines.Add(tulos);
            }

            stulTulosWriter.WriteAll();
        }
    }
}

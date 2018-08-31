using System.Collections.Generic;

namespace Converter.TpsResultsToStulTulos
{
    public class TpsCompetition
    {
        public string CompetitionCode { get; }
        public string CompetitionLevel { get; set; } = "";
        public TpsCompetitionType CompetitionType { get; set; }
        public int TotalCouples { get; set; } = 0;
        public IList<TpsCompetitionRound> Rounds { get; }

        public TpsCompetition(string competitionCode, TpsCompetitionType competitionType = TpsCompetitionType.TenDance)
        {
            CompetitionCode = competitionCode;
            CompetitionType = competitionType;
            Rounds = new List<TpsCompetitionRound>();
        }

        public class TpsCompetitionRound
        {
            public int RoundNumber { get; }
            public IList<TpsCouple> Couples { get; }

            public TpsCompetitionRound(int roundNumber)
            {
                RoundNumber = roundNumber;
                Couples = new List<TpsCouple>();
            }
        }

        public enum TpsCompetitionType
        {
            TenDance = 0,
            Standard,
            Latin
        }
    }
}

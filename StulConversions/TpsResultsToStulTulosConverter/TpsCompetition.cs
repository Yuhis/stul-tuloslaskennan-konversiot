using System.Collections.Generic;

namespace TpsResultsToStulTulosConverter
{
    public class TpsCompetition
    {
        public string CompetitionCode { get; private set; }
        public string CompetitionLevel { get; set; } = "";
        public TpsCompetitionType CompetitionType { get; set; } = TpsCompetitionType.TenDance;
        public int TotalCouples { get; set; } = 0;
        public IList<TpsCompetitionRound> Rounds { get; private set; }

        public TpsCompetition(string competitionCode, TpsCompetitionType competitionType)
        {
            CompetitionCode = competitionCode;
            CompetitionType = competitionType;
            Rounds = new List<TpsCompetitionRound>();
        }

        public class TpsCompetitionRound
        {
            public int RoundNumber { get; private set; }
            public IList<TpsCouple> Couples { get; private set; }

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

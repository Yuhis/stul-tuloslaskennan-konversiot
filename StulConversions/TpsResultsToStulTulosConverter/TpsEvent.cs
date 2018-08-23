using System.Collections.Generic;

namespace TpsResultsToStulTulosConverter
{
    public class TpsEvent
    {
        public string EventCode { get; private set; }
        public IList<TpsCompetition> Competitions { get; private set; }
        public IList<TpsCoupleResult> CoupleResults { get; private set; }

        public TpsEvent(string eventCode)
        {
            EventCode = eventCode;
            Competitions = new List<TpsCompetition>();
            CoupleResults = new List<TpsCoupleResult>();
        }
    }
}

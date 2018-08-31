using System.Collections.Generic;

namespace Converter.TpsResultsToStulTulos
{
    public class TpsEvent
    {
        public string EventCode { get; }
        public IList<TpsCompetition> Competitions { get; }
        public IList<TpsCoupleResult> CoupleResults { get; }

        public TpsEvent(string eventCode)
        {
            EventCode = eventCode;
            Competitions = new List<TpsCompetition>();
            CoupleResults = new List<TpsCoupleResult>();
        }
    }
}

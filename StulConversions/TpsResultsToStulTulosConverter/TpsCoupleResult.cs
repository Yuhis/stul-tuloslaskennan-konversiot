namespace TpsResultsToStulTulosConverter
{
    public class TpsCoupleResult
    {
        public string CompetitionCode { get; private set; }
        public int CoupleCode { get; private set; }
        public int CoupleNumber { get; private set; }
        public string CoupleNames { get; set; } = "";
        public string Result1 { get; set; } = "";
        public string Result2 { get; set; } = "";
        public string Missing { get; set; } = "";

        public TpsCoupleResult(string competitionCode, int coupleCode, int coupleNumber)
        {
            CompetitionCode = competitionCode;
            CoupleCode = coupleCode;
            CoupleNumber = coupleNumber;
        }
    }
}

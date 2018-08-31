namespace Converter.TpsResultsToStulTulos
{
    public class TpsCoupleResult
    {
        public string CompetitionCode { get; }
        public int CoupleCode { get; }
        public int CoupleNumber { get; }
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

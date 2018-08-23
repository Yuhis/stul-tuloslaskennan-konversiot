namespace TpsResultsToStulTulosConverter
{
    public class TpsCouple
    {
        public int CoupleCode { get; private set; }
        public int CoupleNumber { get; set; } = 0;
        public string CoupleNames { get; set; } = "";
        public int Position1 { get; set; } = 0;
        public int Position2 { get; set; } = 0;
        public TpsMissing Missing { get; set; } = TpsMissing.NotMissing;
        public int RoundsDanced { get; set; } = 0;

        public TpsCouple(int coupleCode)
        {
            CoupleCode = coupleCode;
        }

        public enum TpsMissing
        {
            NotMissing = 0,
            Dancing = 1,
            Excused = 2,
            Missing = 3
        }
    }
}

using CsvHelper.Configuration;

namespace Files.Stul.TulosTxt
{
    public class StulTulosLine
    {
        public int CoupleCode { get; set; } = 0;
        public int CoupleNumber { get; set; } = 0;
        public string CoupleNames { get; set; } = "";
        public string ClubName { get; set; } = "";
        public string ClubTown { get; set; } = "";
        public string AgeGroup { get; set; } = "";
        public string Category { get; set; } = "";
        public string Result1 { get; set; } = "";
        public string Result2 { get; set; } = "";
    }

    public sealed class StulTulosLineClassMap : ClassMap<StulTulosLine>
    {
        public StulTulosLineClassMap()
        {
            Map(m => m.CoupleCode).Index(0);
            Map(m => m.CoupleNumber).Index(1);
            Map(m => m.CoupleNames).Index(2);
            Map(m => m.ClubName).Index(3);
            Map(m => m.ClubTown).Index(4);
            Map(m => m.AgeGroup).Index(5);
            Map(m => m.Category).Index(6);
            Map(m => m.Result1).Index(7);
            Map(m => m.Result2).Index(8);
        }
    }
}

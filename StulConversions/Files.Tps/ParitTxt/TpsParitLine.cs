using CsvHelper.Configuration;

namespace Files.Tps.ParitTxt
{
    public class TpsParitLine
    {
        public string CompetitionCode { get; set; } = "";
        public string Fill1 { get; set; } = "";
        public string Fill2 { get; set; } = "";
        public int Entry { get; set; } = 0;
        public string FirstNameMan { get; set; } = "";
        public string LastNameMan { get; set; } = "";
        public string FirstNameWoman { get; set; } = "";
        public string LastNameWoman { get; set; } = "";
        public string Club { get; set; } = "";
        public string Fill3 { get; set; } = "";
        public int MinMan { get; set; } = 0;
        public int MinWoman { get; set; } = 0;
        public int CoupleNumber { get; set; } = 0;
    }

    public sealed class TpsParitLineClassMap : ClassMap<TpsParitLine>
    {
        public TpsParitLineClassMap()
        {
            Map(m => m.CompetitionCode).Name("Comp.ID");
            Map(m => m.Fill1).Name("tyhjä");
            Map(m => m.Fill2).Name("tyhjä");
            Map(m => m.Entry).Name("Entry");
            Map(m => m.FirstNameMan).Name("Miehen nimi");
            Map(m => m.LastNameMan).Name("M sukunimi");
            Map(m => m.FirstNameWoman).Name("Naisen nimi");
            Map(m => m.LastNameWoman).Name("N sukunimi");
            Map(m => m.Club).Name("Seura / paikkakunta");
            Map(m => m.Fill3).Name("tyhjä");
            Map(m => m.MinMan).Name("Min Man");
            Map(m => m.MinWoman).Name("Min Woman");
            Map(m => m.CoupleNumber).Name("Nro in comp.");

        }
    }
}

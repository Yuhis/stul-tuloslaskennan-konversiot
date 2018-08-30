using System;
using System.Xml.Linq;

namespace Files.Tps.ResultsXml
{
    public class TpsResultsElement
    {
        public string EventCode { get; }
        public string CompetitionCode { get; }
        public string CompetitionLevel { get; }
        public CompetitionTypeCode CompetitionType { get; }
        public int TotalCouples { get; }
        public int CoupleCode { get; }
        public int CoupleNumber { get; }
        public string CoupleNames { get; }
        public int Position1 { get; }
        public int Position2 { get; }
        public MissingCode CoupleMissing { get; set; }
        public int RoundsDanced { get; set; }


        public TpsResultsElement(XElement xe)
        {
            EventCode = xe?.Element("EventCode")?.Value ?? "null";
            CompetitionCode = xe?.Element("CompCode")?.Value ?? "null";
            CompetitionLevel = xe?.Element("CompLevel")?.Value ?? "null";
            CompetitionType = MapCompetitionType(xe?.Element("CompType")?.Value ?? "null");
            TotalCouples = ParseInteger(xe?.Element("TotalCouples")?.Value ?? "null", "TotalCouples");
            CoupleCode = ParseInteger(xe?.Element("CoupleCode")?.Value ?? "null", "CoupleCode");
            CoupleNumber = ParseInteger(xe?.Element("CoupleNumber")?.Value ?? "null", "CoupleNumber");
            CoupleNames = xe?.Element("Names")?.Value ?? "";
            Position1 = ParseInteger(xe?.Element("Position1")?.Value ?? "null", "Position1");
            Position2 = ParseInteger(xe?.Element("Position2")?.Value ?? "null", "Position2");
            CoupleMissing = MapMissing(xe?.Element("Missing")?.Value ?? "null");
            RoundsDanced = ParseInteger(xe?.Element("RoundsDanced")?.Value, "RoundsDanced");
        }

        private int ParseInteger(string strValue, string elemName)
        {
            if (int.TryParse(strValue, out int v))
            {
                return v;
            }
            throw new ArgumentOutOfRangeException($"Tunnistamaton {elemName} elementin arvo: {strValue} (oletettiin kokonaisluvuksi)");
        }

        private CompetitionTypeCode MapCompetitionType(string compType)
        {
            string s = " ";
            if (!string.IsNullOrEmpty(compType))
            {
                s = compType.ToUpper();
            }

            switch (s[0])
            {
                case 'S':
                    return CompetitionTypeCode.Standard;
                case 'V':
                    return CompetitionTypeCode.Standard;
                case 'L':
                    return CompetitionTypeCode.Latin;
                case '1':
                    return CompetitionTypeCode.TenDance;
                default:
                    throw new ArgumentOutOfRangeException($"Tunnistamaton CompType elementin arvo: {compType} (oletettiin alkavan kirjaimella 'S', 'V', 'L' tai numerolla '1')");
            }
        }

        private MissingCode MapMissing(string missing)
        {
            switch (missing)
            {
                case "0":
                    return MissingCode.NotMissing;
                case "1":
                    return MissingCode.Dancing;
                case "2":
                    return MissingCode.Excused;
                case "3":
                    return MissingCode.Missing;
                default:
                    throw new ArgumentOutOfRangeException($"Tunnistamaton Missing elementin arvo: {missing} (oletettiin numeroksi 0-3)");
            }
        }

        public enum CompetitionTypeCode
        {
            TenDance = 0,
            Standard,
            Latin
        }

        public enum MissingCode
        {
            NotMissing = 0,
            Dancing = 1,
            Excused = 2,
            Missing = 3
        }
    }
}

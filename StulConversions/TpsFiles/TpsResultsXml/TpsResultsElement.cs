using System;
using System.Xml.Linq;

namespace TpsFiles.TpsResultsXml
{
    public class TpsResultsElement
    {
        public string EventCode { get; private set; } = "";
        public string CompetitionCode { get; private set; } = "";
        public string CompetitionLevel { get; private set; } = "";
        public CompetitionTypeCode CompetitionType { get; private set; } = CompetitionTypeCode.TenDance;
        public int TotalCouples { get; private set; } = 0;
        public int CoupleCode { get; private set; } = 0;
        public int CoupleNumber { get; private set; } = 0;
        public string CoupleNames { get; private set; } = "";
        public int Position1 { get; private set; } = 0;
        public int Position2 { get; private set; } = 0;
        public MissingCode CoupleMissing { get; set; } = MissingCode.NotMissing;
        public int RoundsDanced { get; set; } = 0;


        public TpsResultsElement(XElement xe)
        {
            EventCode = xe.Element("EventCode").Value;
            CompetitionCode = xe.Element("CompCode").Value;
            CompetitionLevel = xe.Element("CompLevel").Value;
            CompetitionType = MapCompetitionType(xe.Element("CompType").Value);
            TotalCouples = ParseInteger(xe.Element("TotalCouples").Value, "TotalCouples");
            CoupleCode = ParseInteger(xe.Element("CoupleCode").Value, "CoupleCode");
            CoupleNumber = ParseInteger(xe.Element("CoupleNumber").Value, "CoupleNumber");
            CoupleNames = xe.Element("Names").Value;
            Position1 = ParseInteger(xe.Element("Position1").Value, "Position1");
            Position2 = ParseInteger(xe.Element("Position2").Value, "Position2");
            CoupleMissing = MapMissing(xe.Element("Missing").Value);
            RoundsDanced = ParseInteger(xe.Element("RoundsDanced").Value, "RoundsDanced");
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

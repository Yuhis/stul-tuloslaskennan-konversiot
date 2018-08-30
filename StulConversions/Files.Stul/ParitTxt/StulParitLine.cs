using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Files.Stul.ParitTxt
{
    public class StulParitLine
    {
        private const string CoupleNamesPattern = @"((?<mf>\S+)\s+){0,1}(?<ml>\S+)\s+-\s+((?<wf>\S+)\s+){0,1}(?<wl>\S+)\s*";
        private string _coupleNames;

        public int CoupleCode { get; set; } = 0;
        public int AreaCode { get; set; } = 0;
        public string CoupleNames
        {
            get { return _coupleNames; }
            set { SetNames(value); _coupleNames = value; }
        }
        public string ClubName { get; set; } = "";
        public string ClubTown { get; set; } = "";
        public string AgeGroup { get; set; } = "";
        public string CategoryStandard { get; set; } = "";
        public string CategoryLatin { get; set; } = "";
        public string ManFirstName { get; private set; } = "";
        public string ManLastName { get; private set; } = "";
        public string WomanFirstName { get; private set; } = "";
        public string WomanLastName { get; private set; } = "";
        public IList<string> Errors { get; private set; } = new List<string>();

        private void SetNames(string couple)
        {
            Match m = Regex.Match(couple, CoupleNamesPattern);
            if (!m.Success)
            {
                SetError($"Puuttuva tai virheellinen nimi parilla nro {CoupleCode}");
                return;
            }
            ManFirstName = m.Groups["mf"].Value;
            ManLastName = m.Groups["ml"].Value;
            WomanFirstName = m.Groups["wf"].Value;
            WomanLastName = m.Groups["wl"].Value;
            if (string.IsNullOrWhiteSpace(ManFirstName) || string.IsNullOrWhiteSpace(WomanFirstName))
            {
                SetError($"Puuttuva etunimi parilla nro {CoupleCode}");
            }
        }

        private void SetError(string error)
        {
            Errors.Add(error);
            return;
        }
    }

    public sealed class StulParitLineClassMap : ClassMap<StulParitLine>
    {
        public StulParitLineClassMap()
        {
            Map(m => m.CoupleCode).Index(0);
            Map(m => m.AreaCode).Index(1);
            Map(m => m.CoupleNames).Index(2);
            Map(m => m.ClubName).Index(3);
            Map(m => m.ClubTown).Index(4);
            Map(m => m.AgeGroup).Index(5);
            Map(m => m.CategoryStandard).Index(6);
            Map(m => m.CategoryLatin).Index(7);
        }
    }
}

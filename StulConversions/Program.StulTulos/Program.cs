using Converter.TpsResultsToStulTulos;
using McMaster.Extensions.CommandLineUtils;

namespace Program.StulTulos
{
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "StulTulos",
                Description = "Konvertoi TPS results tiedoston STUL tulos muotoon",
                ThrowOnUnexpectedArgument = false
            };
            app.HelpOption();
            app.VersionOption("-v|--version", Program.GetVersion());

            var optionParitTxt = app.Option("-p|--parit <parit.txt>", "STUL parit.txt tiedoston nimi", CommandOptionType.SingleValue)
                .Accepts(v => v.ExistingFile());
            var optionResultsXml = app.Option("-r|--results <results.xml>", "TPS results.xml tiedoston nimi", CommandOptionType.SingleValue)
                .Accepts(v => v.ExistingFile());
            var optionTulosTxt = app.Option("-t|--tulos <tulos.txt>", "STUL tulos.txt tiedoston nimi", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var paritTiedosto = optionParitTxt.HasValue()
                    ? optionParitTxt.Value()
                    : "parit.txt";
                var resultsTiedosto = optionResultsXml.HasValue()
                    ? optionResultsXml.Value()
                    : "results.xml";
                var tulosTiedosto = optionTulosTxt.HasValue()
                    ? optionTulosTxt.Value()
                    : "tulos.txt";

                var program = new TpsResultToStulTulos(paritTiedosto, resultsTiedosto, tulosTiedosto);
                program.WriteTulosTxt();
                return 1;
            });

            return app.Execute(args);
        }

        private static string GetVersion()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Revision}";
        }
    }
}

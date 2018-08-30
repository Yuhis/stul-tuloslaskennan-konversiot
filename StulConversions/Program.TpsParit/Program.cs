using Converter.StulParitToTpsParit;
using McMaster.Extensions.CommandLineUtils;

namespace Program.TpsParit
{
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "TpsParit",
                Description = "Konvertoi STUL parit tiedoston TPS parit muotoon",
                ThrowOnUnexpectedArgument = false
            };
            app.HelpOption();
            app.VersionOption("-v|--version", Program.GetVersion());

            var optionParitTxt = app.Option("-p|--parit <parit.txt>", "STUL parit.txt tiedoston nimi", CommandOptionType.SingleValue)
                .Accepts(v => v.ExistingFile());
            var optionParitTpsTxt = app.Option("-t|--tps <parit.tps.txt>", "parit.tps.txt tiedoston nimi", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var paritTiedosto = optionParitTxt.HasValue()
                    ? optionParitTxt.Value()
                    : "parit.txt";
                var paritTpsTiedosto = optionParitTpsTxt.HasValue()
                    ? optionParitTpsTxt.Value()
                    : "parit.tps.txt";

                var program = new StulParitToTpsParit(paritTiedosto, paritTpsTiedosto);
                program.WriteTpsParit();
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

﻿using McMaster.Extensions.CommandLineUtils;

namespace ParitTps
{
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "TulosTps",
                Description = "Konvertoi STUL parit tiedosto TPS muotoon"
            };
            app.HelpOption(inherited: true);

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

                var program = new StulParitToTpsParitConverter.StulParitToTpsParitConverter(paritTiedosto, paritTpsTiedosto);
                program.WriteTpsParit();
                return 1;
            });

            return app.Execute(args);
        }
    }
}

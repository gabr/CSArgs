using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Args
{
  class Program
  {
    static void Main(string[] args)
    {
      // dotnet add package Microsoft.Extensions.CommandLineUtils --version 1.1.1
      var cla = new CommandLineApplication(throwOnUnexpectedArg: false)
      {
        // name of application used in help output
        Name = "Args",
        FullName = "Args full description printed on the top",

        // for help description as well
        ShortVersionGetter = () => { return "(version 0.1)"; }
      };

      // the "/?" option is impossible to get with this lib
      //cla.HelpOption("/?|-h|--help");
      cla.HelpOption("-?|-h|--help");

      cla.Command("hide", (command) =>
      {
        command.FullName = "Full 'hide' name";
        command.Description = "Instruct the ninja to hide in a specific location.";
        command.HelpOption("-?|-h|--help");

        var locationArgument = command.Argument("[location]", "Where the ninja should hide.");

        command.OnExecute(() =>
        {
          var location = locationArgument.Value != null ? locationArgument.Value : "under a turtle";
          Console.WriteLine("Ninja is hidden " + location);
          return 0;
        });
      }, throwOnUnexpectedArg: false);

      cla.OnExecute(() =>
      {
        cla.ShowHelp();
        return 0;
      });

      cla.Execute(args);
    }
  }
}

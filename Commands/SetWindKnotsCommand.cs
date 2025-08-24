using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;

namespace NANDCommand.Commands
{
    internal class SetWindKnotsCommand : Command
    {
        public override string Name => "setWindKnots";
        public override int MinArgs => 1;
        public override string Usage => "<wind speed (int knots)>";

        public override string Description => "Set wind speed";

        public override void OnRun(List<string> args)
        {
            if (int.TryParse(args[0], out int windSpeed))
            {
                Wind.instance.SetPrivateField("currentWindTarget", Wind.currentBaseWind.normalized * windSpeed);
                Wind.instance.SetPrivateField("timer", 90);
                ModConsoleLog.Log(Plugin.instance.Info, "Wind speed set!");
            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Not a valid wind value!");
            }
        }
    }
}

using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;

namespace NANDCommand.Commands
{
    internal class SetWindKnotsCommand : Command
    {
        public override string Name => "setWindKnots";
        public override int MinArgs => 1;
        public override string Usage => "<wind speed (knots)>";

        public override string Description => "Set wind speed for 90 seconds";

        public override void OnRun(List<string> args)
        {
            if (float.TryParse(args[0], out float windSpeed))
            {
                Wind.instance.SetPrivateField("currentWindTarget", Wind.currentBaseWind.normalized * (windSpeed / 1.865f));
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

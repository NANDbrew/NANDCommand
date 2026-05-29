using System.Collections.Generic;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SetLevelCommand : Command
    {
        public override string Name => "SetLevel";
        public override int MinArgs => 2;

        public override string Usage => "<region (0-2)> <amount>";

        public override string Description => "Add reputation in a certain region";

        public override void OnRun(List<string> args)
        {
            int.TryParse(args[0], out var result);
            if (result < 3)
            {
                PortRegion portRegion = (PortRegion)result;
                int.TryParse(args[1], out var result2);
                int currentRep = PlayerReputation.GetRep(portRegion);
                PlayerReputation.ChangeReputation(PlayerReputation.GetRequiredRep(result2) - currentRep, portRegion);
                ModConsoleLog.Log(Plugin.instance.Info, $"Set level to {result2} for {portRegion}");

            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Invalid region value!");
            }
        }
    }
}

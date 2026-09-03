using System;
using System.Collections.Generic;
using System.Linq;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class GetCurrentRegionCommand : Command
    {
        public override string Name => "getCurrentRegion";
        public override string[] Aliases => new string[1] { "gcr" };
        public override string Usage => "";
        public override string Description => "Get the current weather/port region";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            try {
                Region region = RegionBlender.instance.GetPrivateField<Region>("currentTargetRegion");

                ModConsoleLog.Log(Plugin.instance.Info, $"Current region is {region.gameObject.name}, aka PortRegion.{region.portRegion} or {(int)region.portRegion}");
            }
            catch {
                ModConsoleLog.Error(Plugin.instance.Info, "Couldn't find region");
            }

        }

    }
}

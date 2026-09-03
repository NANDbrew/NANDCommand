using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class FillNeedsCommand : Command
    {
        public override string Name => "FillNeeds";
        public override int MinArgs => 0;
        public override string Usage => "FillNeeds";

        public override string Description => "Fill your survival completely";

        public override void OnRun(List<string> args)
        {
            PlayerNeeds.sleep = 100;
            PlayerNeeds.sleepDebt = 100;
            PlayerNeeds.water = 100;
            PlayerNeeds.food = 100;
            PlayerNeeds.foodDebt = 100;
            PlayerNeeds.vitamins = 100;
            PlayerNeeds.protein = 100;

            ModConsoleLog.Log(Plugin.instance.Info, "You feel refreshed");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class GetNeedsCommand : Command
    {
        public override string Name => "getNeeds";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Get the player's health stats";

        public override void OnRun(List<string> args)
        {
            string text = $"Sleep: {PlayerNeeds.sleep}, Sleep Debt: {PlayerNeeds.sleepDebt}, " +
                $"Water: {PlayerNeeds.water}, " +
                $"Food: {PlayerNeeds.food}, Food Debt: {PlayerNeeds.foodDebt}, " +
                $"Protein: {PlayerNeeds.protein}, Vitamins: {PlayerNeeds.vitamins}";
            ModConsoleLog.Log(Plugin.instance.Info, text);

        }
    }
}

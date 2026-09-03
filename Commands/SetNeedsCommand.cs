using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class SetNeedsCommand : Command
    {
        public override string Name => "SetNeeds";
        public override int MinArgs => 3;
        public override string Usage => "SetNeeds <float sleep (100 +/-)> <float thirst (0-100)> <float hunger (100 +/-)> [float vitamins (0-100)]";

        public override string Description => "Set the player's health stats. Sleep and hunger can be negative, and will dip into their respective debts";

        public override void OnRun(List<string> args)
        {
            if (float.TryParse(args[0], out var value0) && float.TryParse(args[1], out var value1) && float.TryParse(args[2], out var value2))
            {
                if (value0 < 0f)
                {
                    PlayerNeeds.sleepDebt = 100f + value0;
                    value0 = 0f;
                }
                PlayerNeeds.sleep = value0;
                PlayerNeeds.water = value1 > 0f ? value1 : 0f;
                if (value2 < 0f)
                {
                    PlayerNeeds.foodDebt = 100f + value2;
                    value2 = 0f;
                }
                PlayerNeeds.food = value2;
            }
            if (args.Count > 3 && float.TryParse(args[3], out var value))
            {
                PlayerNeeds.vitamins = value;
            }
            string text = $"Sleep: {PlayerNeeds.sleep}, Sleep Debt: {PlayerNeeds.sleepDebt}, " +
                $"Water: {PlayerNeeds.water}, " +
                $"Food: {PlayerNeeds.food}, Food Debt: {PlayerNeeds.foodDebt}, " +
                $"Vitamins: {PlayerNeeds.vitamins}, Protein: {PlayerNeeds.protein}";
            ModConsoleLog.Log(Plugin.instance.Info, text);

        }
    }
}

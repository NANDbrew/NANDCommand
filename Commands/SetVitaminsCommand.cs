using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class SetVitaminsCommand : Command
    {
        public override string Name => "setVitamins";
        public override int MinArgs => 1;
        public override string Usage => "<amount>";

        public override string Description => "Set your vitamins";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out float amount);
            if (amount > 0)
            {
                PlayerNeeds.vitamins = amount;
                UISoundPlayer.instance.PlayOpenSound();
                ModConsoleLog.Log(Plugin.instance.Info, $"Set player vitamins to {amount}");
            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Cannot have a value below 0!");
            }
        }
    }
}

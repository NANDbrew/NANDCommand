using System.Collections.Generic;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class AddMoneyCommand : Command
    {
        public override string Name => "AddMoney";
        public override int MinArgs => 2;

        public override string Usage => "<currency type (0-3)> <amount>";

        public override string Description => "Add or subtract gold";

        public override void OnRun(List<string> args)
        {
            int.TryParse(args[0], out var result);
            int.TryParse(args[1], out var result2);
            if (result < 0 || result > 3)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Currency type out of range");
            }
            else
            {
                PlayerGold.currency[result] += result2;
                UISoundPlayer.instance.PlayGoldSound();
                ModConsoleLog.Log(Plugin.instance.Info, $"Changed {PlayerGold.GetCurrencyName(result)} by {result2}");
            }

        }

    }
}

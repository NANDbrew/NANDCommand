using SailwindConsole;
using SailwindConsole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NANDCommand.Commands
{
    internal class FindItemCommand : Command
    {
        public override string Name => "FindItem";
        public override int MinArgs => 1;
        public override string Usage => "<search term>";

        public override string Description => "Find an item's index and internal name";

        public override void OnRun(List<string> args)
        {
            string text = "found items:";
            for (int i = 0; i < PrefabsDirectory.instance.shipItems.Length; i++)
            {
                var item = PrefabsDirectory.instance.shipItems[i];
                if (item != null && (item.name.ToLower().Contains(args[0].ToLower()) || item.gameObject.name.ToLower().Contains(args[0].ToLower())))
                {
                    text += $"{Environment.NewLine}[<color=blue>index {i}</color>]: {item.name}<color={LogColor.InternalName}> {item.gameObject.name}</color>";
                }
            }
            ModConsoleLog.Log(Plugin.instance.Info, Plugin.ClipEnd(text));

        }
    }
}

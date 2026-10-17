using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace NANDCommand.Commands
{
    internal class SmallItemsCommand : Command
    {
        public override string Name => "SmallItems";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Set the \'big\' flag to false on all items when they're picked up";

        public override void OnRun(List<string> args)
        {
            PickupItemPatch.setSmall = !PickupItemPatch.setSmall;

            string verb = PickupItemPatch.setSmall ? "made small" : "left as they are";

            ModConsoleLog.Log(Plugin.instance.Info, "items will be " + verb);

        }
    }

    [HarmonyPatch(typeof(GoPointer), "PickUpItem")]
    internal static class PickupItemPatch
    {
        internal static bool setSmall;
        public static void Postfix(PickupableItem item)
        {
            if (setSmall)
            {
                item.big = false;
            }
        }
    }
}

using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;

namespace NANDCommand.Commands
{
    public class CookFoodCommand : Command
    {
        public override string Name => "CookFood";
        //public override string[] Aliases => new string[1] { "" };

        public override string Usage => "";
        public override string Description => "Cook currently held food item";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            var pointer = Refs.ovrCameraRig.GetComponentInChildren<GoPointer>(false);
            if (pointer != null)
            {
                var helditem = pointer.GetHeldItem();
                if (helditem != null && helditem.GetComponent<ShipItemFood>() is ShipItemFood food)
                {
                    food.amount = 1.2f;

                    helditem.GetComponent<CookableFood>().UpdateMaterial();
                    ModConsoleLog.Log(Plugin.instance.Info, $"Cooked food: {helditem.name}");
                    return;
                }
            }

            ModConsoleLog.Error(Plugin.instance.Info, $"no cookable item found");
        }

    }
}

using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;

namespace NANDCommand.Commands
{
    public class SmokeFoodCommand : Command
    {
        public override string Name => "SmokeFood";
        //public override string[] Aliases => new string[1] { "" };

        public override string Usage => "";
        public override string Description => "Smoke currently held food item";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            var pointer = Refs.ovrCameraRig.GetComponentInChildren<GoPointer>(false);
            if (pointer != null)
            {
                var helditem = pointer.GetHeldItem();
                if (helditem != null && helditem.GetComponent<FoodState>() is FoodState food)
                {
                    food.AddSmoked(1f);
                    if (args.Count == 1 && args[0].ToLower() == "cook" && helditem.GetComponent<ShipItemFood>() is ShipItemFood food2)
                    {
                        food2.amount = 1.01f;
                    }
                    helditem.GetComponent<CookableFood>().UpdateMaterial();
                    ModConsoleLog.Log(Plugin.instance.Info, $"smoked food: {helditem.name}");
                    return;
                }
            }

            ModConsoleLog.Error(Plugin.instance.Info, $"no smokable item found");
        }

    }
}

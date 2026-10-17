using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NANDCommand.Commands
{
    internal class GetWeightCommand : Command
    {
        public override string Name => "GetWeight";
        //public override int MinArgs => 0;
        //public override string Usage => "";

        public override string Description => "Get weight of held item";

        public override void OnRun(List<string> args)
        {
            GoPointer goPointer = Object.FindObjectOfType<GoPointer>();
            PickupableItem heldItem = goPointer.GetHeldItem();
            if ((bool)heldItem)
            {
                ShipItem component = heldItem.GetComponent<ShipItem>();
                if ((bool)component && component.GetItemRigidbody() != null)
                {
                    float mass = component.GetItemRigidbody().GetBody().mass;
                    if (heldItem.GetComponent<CrateInventory>() is CrateInventory inventory)
                    {
                        foreach (var item in inventory.containedItems)
                        {
                            mass += item.mass;
                        }
                    }

                    ModConsoleLog.Log(Plugin.instance.Info, $"Held Item Weight: {mass}");

                }
                else
                {
                    ModConsoleLog.Log(Plugin.instance.Info, "Item held has no mass!");
                }
            }
            else
            {
                ModConsoleLog.Log(Plugin.instance.Info, "Not holding anything!");
            }

        }
    }
}

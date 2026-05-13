using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SetOwnedCommand : Command
    {
        public override string Name => "setOwned";
        //public override string[] Aliases => new string[1] { "getBilge" };
        public override string Usage => "[index or vanilla boat name] [true, false]";
        public override string Description => "Set the owned status of the targeted boat. Assumes \"true\" if unspecified";

        public override void OnRun(List<string> args)
        {
            Transform boat = (args.Count > 0 && args[0].ToLower() != "true" && args[0].ToLower() != "false")? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            if (args.Count > 0 && args.Last().ToLower() == "false")
            {
                boat.GetComponent<SaveableObject>().extraSetting = false;
                var purchasable = boat.GetComponent<PurchasableBoat>();
                GameObject ui = (GameObject)AccessTools.Field(purchasable.GetType(), "purchaseUI").GetValue(purchasable);
                if (ui != null) { ui.SetActive(true); }
                ModConsoleLog.Log(Plugin.instance.Info, $"Set {boat.name} as unowned");

            }
            else 
            {
                boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
                //boat.GetComponent<SaveableObject>().extraSetting = true;
                ModConsoleLog.Log(Plugin.instance.Info, $"Set {boat.name} as owned");

            }


            //ModConsoleLog.Log(Plugin.instance.Info, $"Set {boat.name}");
        }



    }
}

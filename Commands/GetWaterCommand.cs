using System;
using System.Collections.Generic;
using System.Linq;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class GetWaterCommand : Command
    {
        public override string Name => "getWater";
        public override string[] Aliases => new string[1] { "getBilge" };
        public override string Usage => "[index or vanilla boat name]";
        public override string Description => "Get the current water level and capacity of the targeted boat\nAlias: getBilge";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            var boatDamage = boat.GetComponent<BoatDamage>();

            float damage = boatDamage.waterLevel;
            float capacity = boatDamage.waterUnitsCapacity;


            ModConsoleLog.Log(Plugin.instance.Info, $"{boat.name} water level is {Math.Round(capacity * damage, 2)} of {capacity} or {Math.Round(damage * 100, 2)}%");
        }



    }
}

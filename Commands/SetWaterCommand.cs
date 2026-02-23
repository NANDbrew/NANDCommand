using System;
using System.Collections.Generic;
using System.Linq;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SetWaterCommand : Command
    {
        public override string Name => "setWater";
        public override string[] Aliases => new string[1] { "setBilge" };
        public override string Usage => "[index or vanilla boat name] <water>";
        public override string Description => "Set the bilge water level of the targeted boat (or current/last boat if unspecified)\n                   Accepts \"units\" or percentage ('setWater 20' will assume units, 'setWater 20%' will be percentage\nAlias: setBilge";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 1 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            var boatDamage = boat.GetComponent<BoatDamage>();
            float damage = float.Parse(args.Last().TrimEnd('%'));


            if (args.Last().Last() == '%')
            {
                damage /= 100;
            }
            else
            {
                damage /= boatDamage.waterUnitsCapacity;

            }
            damage = Mathf.Max(Mathf.Min(damage, 1), 0);
            boatDamage.waterLevel = damage;

            ModConsoleLog.Log(Plugin.instance.Info, $"Set {boat.name} water level to {Math.Round(damage * boatDamage.waterUnitsCapacity, 2)} of {boatDamage.waterUnitsCapacity} or {Math.Round(damage * 100, 2)}%");
        }



    }
}

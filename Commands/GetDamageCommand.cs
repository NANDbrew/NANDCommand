using System;
using System.Collections.Generic;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class GetDamageCommand : Command
    {
        public override string Name => "getDamage";
        //public override string[] Aliases => new string[1] { "BTS" };
        public override string Usage => "[index or vanilla boat name]";
        public override string Description => "Returns the current damage percentage of the targeted boat (or current/last boat if unspecified)";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }


            string damage = Math.Round(boat.GetComponent<BoatDamage>().hullDamage * 100, 2).ToString();

            ModConsoleLog.Log(Plugin.instance.Info, $"{boat.name} is {damage}% damaged");
        }



    }
}

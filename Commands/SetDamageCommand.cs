using System;
using System.Collections.Generic;
using System.Linq;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SetDamageCommand : Command
    {
        public override string Name => "setDamage";
        //public override string[] Aliases => new string[1] { "BTS" };
        public override string Usage => "[index or vanilla boat name] <damage percent>";
        public override string Description => "Set the current damage percentage of the targeted boat (or current/last boat if unspecified)";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 1 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            float damage = float.Parse(args.Last());
            damage = Mathf.Max(Mathf.Min(damage, 100), 0);
            boat.GetComponent<BoatDamage>().hullDamage = damage / 100;

            ModConsoleLog.Log(Plugin.instance.Info, $"Set {boat.name} damage to {damage}%");
        }



    }
}

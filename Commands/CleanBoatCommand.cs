using System;
using System.Collections.Generic;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class CleanBoatCommand : Command
    {
        public override string Name => "cleanBoat";
        //public override string[] Aliases => new string[1] { "getBilge" };
        public override string Usage => "[index or vanilla boat name]";
        public override string Description => "Clean the targeted boat";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            var cleanable = boat.GetComponent<SaveableObject>().GetCleanable();
            cleanable.CleanFully();

            ModConsoleLog.Log(Plugin.instance.Info, $"cleaned {boat.name}");
        }



    }
}

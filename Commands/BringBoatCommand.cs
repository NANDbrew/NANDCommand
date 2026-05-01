using System.Collections.Generic;
using System.Linq;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class BringBoatCommand : Command
    {
        public override string Name => "bringToShipyard";
        public override string[] Aliases => new string[1] { "BTS" };
        public override string Usage => "[scene index or vanilla boat name]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned\nAlias: BTS";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            Shipyard shipyard = GameObject.FindObjectsOfType<Shipyard>().OrderBy(s => (s.transform.position - Refs.observerMirror.transform.position).sqrMagnitude).FirstOrDefault();
            if (shipyard == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find shipyard");
                return;
            }

            Plugin.instance.StartCoroutine(BoatMover.IMoveBoat(shipyard.shipReleasePosition.position, shipyard.shipReleasePosition.rotation, boat.transform));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved boat {boat.name} to {shipyard.name}");
        }


    }
}

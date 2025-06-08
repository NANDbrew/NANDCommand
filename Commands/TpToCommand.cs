using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class TpToCommand : Command
    {
        public override string Name => "TeleportTo";
        public override string[] Aliases => new string[1] { "TPTo" };

        public override string Usage => "<target type (island, boat)> <target (island index, boat index or vanilla boat name)>";
        public override string Description => "Teleport to scenery, or boat if first argument is \"boat\"";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            float x = 0;
            float y = 10;
            float z = 0;

            if (args[0].ToLower() == "island")
            {
                int index = args.Count == 1 ? Convert.ToInt32(args[0]) : Convert.ToInt32(args[1]);
                var island = Refs.islands[index];
                if (island == null)
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                    return;
                }
                if (island.GetComponent<IslandHorizon>().overrideCenter is Transform center)
                {
                    x = center.position.x;
                    z = center.position.z;
                }
                else
                {
                    x = island.position.x;
                    z = island.position.z;
                }
                y = 100;
            }
            else if (args[0].ToLower() == "boat")
            {
                if (BoatFinder.FindBoat(args[1]) is Transform target)
                {
                    x = target.position.x;
                    z = target.position.z;
                    y = 5;
                }
                else
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                }
            }

            PlayerMover.MovePlayer(new Vector3(x, y, z));

            //FloatingOriginManager.instance.StartCoroutine(MoveToObject(island, pad));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved player");
        }

    }
}

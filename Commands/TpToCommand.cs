using System;
using System.Collections.Generic;
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

        public override string Usage => "<target type (island, coords, boat, object)> <target (island index, lat long, boat index or vanilla boat name)>";
        public override string Description => "Teleport to scenery, coords, or boat";
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
                if (BoatFinder.FindBoat(args[1]) is Transform boat)
                {
                    var transPos = boat.TransformPoint(0, 30, 0);
                    x = transPos.x;//oat.position.x;
                    y = transPos.y;//oat.position.z;
                    z = transPos.z;//boat.position.y + 10;
                }
                else
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                    return;
                }
            }
            else if (args[0].ToLower() == "coords")
            {
                float longitude = 0;
                float latitude = 0;
                try
                {
                    longitude = Convert.ToSingle(args[2]);
                    latitude = Convert.ToSingle(args[1]);
                }
                catch 
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "invalid coordinates");                
                }

                Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
                Vector3 targetPos = new Vector3(longitude, 0f, latitude) * 9000 + globeOffset;
                targetPos = FloatingOriginManager.instance.RealPosToShiftingPos(targetPos);
                x = targetPos.x;
                z = targetPos.z;
                y = 20;
            }
            else if (args[0].ToLower() == "object")
            {
                if (SaveLoadManager.instance.GetCurrentObjects()[Convert.ToInt32(args[1])] is SaveableObject obj)
                {
                    x = obj.transform.position.x;
                    y = obj.transform.position.y;
                    z = obj.transform.position.z;

                }
                else
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                    return;
                }
            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "invalid target");
                return;
            }
            ModConsoleLog.Log(Plugin.instance.Info, $"moving player...");

            PlayerMover.MovePlayer(new Vector3(x, y, z));
        }

    }
}

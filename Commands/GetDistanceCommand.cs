using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class GetDistanceCommand : Command
    {
        public override string Name => "getDistance";
        public override string[] Aliases => new string[1] { "getDist" };

        public override string Usage => "<target type (island, boat, port)> <target (island index, boat index or vanilla boat name, port name)>";
        public override string Description => "Get distance from player to target\nAlias: getDist";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            string targetName = "";
            float x = 0;
            float z = 0;

            if (args[0].ToLower() == "island")
            {
                int index = Convert.ToInt32(args[1]);
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
                targetName = island.gameObject.name;
            }
            else if (args[0].ToLower() == "boat")
            {
                if (BoatFinder.FindBoat(args[1]) is Transform target)
                {
                    x = target.position.x;
                    z = target.position.z;
                    targetName = target.gameObject.name;
                }
                else
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                    return;
                }
            }
            else if (args[0].ToLower() == "port")
            {
                string text = string.Join(" ", args.GetRange(1, args.Count - 1));
                bool found = false;
                Port[] ports = Port.ports;
                foreach (Port port in ports)
                {
                    if ((bool)port && port.GetPortName().ToLower() == text.ToLower())
                    {
                        x = port.transform.position.x;
                        z = port.transform.position.z;
                        found = true;   
                        targetName = port.GetPortName();
                    }
                }
                if (!found)
                {
                    ModConsoleLog.Error(Plugin.instance.Info, "couldn't find target");
                    return;
                }
            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "invalid target type");
                return;
            }

            float distance = Vector3.Distance(new Vector3(x, 0, z), new Vector3(Refs.observerMirror.transform.position.x, 0, Refs.observerMirror.transform.position.z));

            ModConsoleLog.Log(Plugin.instance.Info, $"distance to {targetName} = {distance / 100} miles, or {distance / 9000} degrees");
        }

    }
}

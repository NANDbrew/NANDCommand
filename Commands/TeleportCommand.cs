using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NANDCommand.Commands
{
    internal class TeleportCommand : Command
    {
        public override string Name => "Teleport";
        public override int MinArgs => 1;
        public override string Usage => "<port name>";

        public override string Description => "Teleport to a port (do not pause for a while after teleporting)";

        public override void OnRun(List<string> args)
        {
            string text = string.Join(" ", args);
            Port[] ports = Port.ports;
            foreach (Port port in ports)
            {
                if ((bool)port && port.GetPortName().ToLower() == text.ToLower())
                {
                    ModConsoleLog.Log(Plugin.instance.Info, $"moving player...");

                    PlayerMover.MovePlayer(new Vector3(port.transform.position.x, 10f, port.transform.position.z));

                    return;
                }
            }
            
            ModConsoleLog.Error(Plugin.instance.Info, "Can't find port!");

        }
    }
}

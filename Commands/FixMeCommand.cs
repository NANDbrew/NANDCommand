using System.Collections.Generic;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class FixMeCommand : Command
    {
        public override string Name => "FixMe";
        public override string[] Aliases => new string[1] { "UnstuckMe" };

        public override string Usage => "FixMe [height]";
        public override string Description => "Unstuck yourself";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            float distance = 5;
            if (args.Count > 0 && float.TryParse(args[0], out float dist)) distance = dist;
            Scripts.PlayerMover.MovePlayer(Refs.charController.transform.position + (Vector3.up * distance));

            ModConsoleLog.Log(Plugin.instance.Info, $"moved player upward {distance}m");
        }

    }
}

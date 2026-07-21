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

        public override string Usage => "";
        public override string Description => "Unstuck yourself";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            Scripts.PlayerMover.MovePlayer(Refs.charController.transform.position + (Vector3.up * 8));

            ModConsoleLog.Log(Plugin.instance.Info, $"moved player");
        }

    }
}

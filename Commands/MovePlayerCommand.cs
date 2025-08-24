using System;
using System.Collections.Generic;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class MovePlayerCommand : Command
    {
        public override string Name => "movePlayer";
        public override string Usage => "<lat> <long>";
        public override string Description => "Teleport to globe coords";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            Debug.Log("moving?");
            float longitude = Convert.ToSingle(args[1]);
            float latitude = Convert.ToSingle(args[0]);

            Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
            Vector3 targetPos = new Vector3(longitude, 0f, latitude) * 9000 + globeOffset;
            targetPos = FloatingOriginManager.instance.RealPosToShiftingPos(targetPos);
            PlayerMover.MovePlayer(targetPos + Vector3.up * 20, null);

            ModConsoleLog.Log(Plugin.instance.Info, $"moved to {latitude}, {longitude}");
        }
    }
}

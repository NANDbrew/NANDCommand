using System;
using System.Collections.Generic;
using SailwindConsole.Commands;
using SailwindConsole;
using HarmonyLib;

namespace NANDCommand.Commands
{
    public class SetWalkSpeedCommand : Command
    {
        public override string Name => "setWalkSpeed";
        public override string[] Aliases => new string[] { "setMoveSpeed", "sws", "sms" };
        public override string Usage => "<speed>";
        public override string Description => "Set player walking speed. 1 is default";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out var speed);
            AccessTools.Field(typeof(OVRPlayerController), "MoveScaleMultiplier").SetValue(Refs.ovrController, speed);

            ModConsoleLog.Log(Plugin.instance.Info, $"Set player movement multiplier to {speed}");
        }



    }
}

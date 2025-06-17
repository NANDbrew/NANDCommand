using System;
using System.Collections.Generic;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class CheatSpeedCommand : Command
    {
        public override string Name => "CheatSpeed";
        //public override string[] Aliases => new string[1] { "TPTo" };

        public override string Usage => "[speed]";
        public override string Description => "Set Cheaty Speed. Resets to 0 if unspecified";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            float speed = 0;
            if (args.Count > 0)
            {
                speed = float.Parse(args[0]);
            }
            CheatsPatch.cheatSpeed = speed;
            ModConsoleLog.Log(Plugin.instance.Info, $"set cheaty speed to {speed}");
        }

    }
}

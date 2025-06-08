using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SetTimescaleCommand : Command
    {
        public override string Name => "setTimeScale";

        public override string Usage => "[float time] [use ratio (-r)]";

        public override string Description => "Sets day/night time scale. if unspecified, resets to default (0.008 aka 28.8)";

        public override void OnRun(List<string> args)
        {
            float scale = 0.008f;
            if (args.Count >= 1)
            {
                if (!char.IsNumber(args[0][0]))
                {
                    ModConsoleLog.Error(Plugin.instance.Info, $"invalid multiplier");
                    return;
                }
                scale = Convert.ToSingle(args[0]);
            }
            if (args.Count > 1 && args.Last().ToLower() == "-r")
            {
                scale /= 3600;
            }
            Sun.sun.initialTimescale = scale;
            ModConsoleLog.Log(Plugin.instance.Info, $"setting time scale to {scale} aka {scale * 3600}");

        }

    }
}

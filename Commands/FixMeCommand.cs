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
    public class FixMeCommand : Command
    {
        public override string Name => "FixMe";
        //public override string[] Aliases => new string[1] { "" };

        public override string Usage => "";
        public override string Description => "Unstuck yourself";
        public override int MinArgs => 0;

        public override void OnRun(List<string> args)
        {
            Refs.charController.transform.Translate(Vector3.up);

            ModConsoleLog.Log(Plugin.instance.Info, $"moved player");
        }

    }
}

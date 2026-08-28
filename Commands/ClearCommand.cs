using HarmonyLib;
using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using UnityEngine.UI;

namespace NANDCommand.Commands
{
    internal class ClearCommand : Command
    {
        public override string Name => "clear";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Clear console history";

        public override void OnRun(List<string> args)
        {
            Patches.ConsoleTextThing.logText.text = "";
            //ModConsole.MoveScrollToEnd();
            //ModConsoleLog.WriteNewLine();
        }
    }
}

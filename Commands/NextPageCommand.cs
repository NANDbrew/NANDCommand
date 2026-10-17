using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;

namespace NANDCommand.Commands
{
    internal class NextPageCommand : Command
    {
        public override string Name => "Next";
        public override string[] Aliases => new string[1] { "np" };
        public override int MinArgs => 0;
        public override string Usage => "";
        public override string Description => "Show the next page of clipped text";

        public override void OnRun(List<string> args)
        {
            if (string.IsNullOrEmpty(Plugin.consoleOverflow))
            {
                ModConsoleLog.Log(Plugin.instance.Info, "No more pages");
            }
            else
            {
                ModConsoleLog.Log(Plugin.instance.Info, Plugin.ClipEnd(Plugin.consoleOverflow));
            }
        }
    }
}

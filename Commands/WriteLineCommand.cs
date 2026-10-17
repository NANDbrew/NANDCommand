using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;

namespace NANDCommand.Commands
{
    internal class WriteLineCommand : Command
    {
        public override string Name => "WriteLine";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Write text to console. Good for testing text formatting stuff";

        public override void OnRun(List<string> args)
        {

            ModConsoleLog.Log(Plugin.instance.Info, string.Join(" ", args));

        }
    }
}

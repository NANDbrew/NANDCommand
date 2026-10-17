using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;
using System.Linq;

namespace NANDCommand.Commands
{
    internal class EchoCommand : Command
    {
        public override string Name => "Echo";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Write text to console. Good for testing text formatting stuff";

        public override void OnRun(List<string> args)
        {

            ModConsoleLog.Log(Plugin.instance.Info, string.Join(" ", args));

        }
    }
}

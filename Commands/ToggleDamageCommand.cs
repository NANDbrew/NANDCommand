using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections.Generic;

namespace NANDCommand.Commands
{
    internal class ToggleDamageCommand : Command
    {
        public override string Name => "toggleDamage";
        public override int MinArgs => 0;
        public override string Usage => "";

        public override string Description => "Toggle impact damage";

        public override void OnRun(List<string> args)
        {
            if (Plugin.ignoreDamage)
            {
                Plugin.ignoreDamage = false;
                ModConsoleLog.Log(Plugin.instance.Info, "Impact damage disabled!");
            }
            else
            {
                Plugin.ignoreDamage = true;
                ModConsoleLog.Log(Plugin.instance.Info, "Impact damage enabled!");
            }

        }
    }
}

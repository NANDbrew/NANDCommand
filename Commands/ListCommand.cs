using System.Collections.Generic;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class ListCommand : Command
    {
        public override string Name => "list";
        //public override string[] Aliases => new string[1] { "" };

        public override string Usage => "<type(boats, islands)>";
        public override string Description => "List all objects of the specified type";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            string text = "";
            if (args[0].ToLower() == "boats")
            {
                text += "found boats:";
                for (int i = 0; i < SaveLoadManager.instance.GetCurrentObjects().Length; i++)
                {
                    var boat = SaveLoadManager.instance.GetCurrentObjects()[i];
                    if (boat != null && boat.GetComponent<PurchasableBoat>() != null)
                    {
                        text += $"\nindex {i}: {boat.name}";
                    }
                }
            }
            else if (args[0].ToLower() == "islands")
            {
                text += "found islands:";
                for (int i = 0; i < Refs.islands.Length; i++)
                {
                    if (Refs.islands[i] != null)
                    {
                        text += "\n" + Refs.islands[i].name;
                    }
                }
            }
            ModConsoleLog.Log(Plugin.instance.Info, text);
        }

    }
}

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

        public override string Usage => "<type(boats, islands, items, ports, objects)>";
        public override string Description => "List all objects of the specified type";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            string text = "";
            if (args[0].ToLower() == "boats")
            {
                text = "found boats:";
                for (int i = 0; i < SaveLoadManager.instance.GetCurrentObjects().Length; i++)
                {
                    var boat = SaveLoadManager.instance.GetCurrentObjects()[i];
                    if (boat != null && boat.GetComponent<PurchasableBoat>() != null)
                    {
                        text += $"\nindex {i}: {boat.name}";
                    }
                }
            }
            if (args[0].ToLower() == "objects")
            {
                text = "found objects:";
                for (int i = 0; i < SaveLoadManager.instance.GetCurrentObjects().Length; i++)
                {
                    var obj = SaveLoadManager.instance.GetCurrentObjects()[i];
                    if (obj != null)
                    {
                        text += $"\nindex {i}: {obj.name}";
                    }
                }
            }
            else if (args[0].ToLower() == "islands")
            {
                text = "found islands:";
                for (int i = 0; i < Refs.islands.Length; i++)
                {
                    if (Refs.islands[i] != null)
                    {
                        text += "\n" + Refs.islands[i].name;
                    }
                }
            }
            else if (args[0].ToLower() == "ports")
            {
                text = "found ports:";
                for (int i = 0; i < Port.ports.Length; i++)
                {
                    if (Port.ports[i] != null)
                    {
                        text += "\n" + Port.ports[i].GetPortName();
                    }
                }
            }
            else if (args[0].ToLower() == "items")
            {
                text = "found items:";
                int i = 0;
                try
                {
                    while (i < PrefabsDirectory.instance.directory.Length)
                    {
                        if (PrefabsDirectory.instance.directory[i] != null)
                        {
                            text += $"\nindex {i}: {PrefabsDirectory.instance.directory[i].name}";
                        }
                        i++;
                    }
                }
                catch { ModConsoleLog.Error(Plugin.instance.Info, "Something went wrong at index " + i); }
            }
            else
            {
                text = "unknown object type"; 
            }
            ModConsoleLog.Log(Plugin.instance.Info, text);
        }

    }
}

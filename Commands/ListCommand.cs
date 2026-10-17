using System;
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

        public override string Usage => "<type (boats, islands, items, ports, objects, sails)>";
        public override string Description => "List objects of the specified type";
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
                        text += $"{Environment.NewLine}[<color=blue>index {i}</color>]: {boat.name}";
                    }
                }
            }
            else if (args[0].ToLower() == "objects")
            {
                text = "found objects:";
                for (int i = 0; i < SaveLoadManager.instance.GetCurrentObjects().Length; i++)
                {
                    var obj = SaveLoadManager.instance.GetCurrentObjects()[i];
                    if (obj != null)
                    {
                        text += $"{Environment.NewLine}[<color=blue>index {i}</color>]: {obj.name}";
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
                        text += Environment.NewLine + Refs.islands[i].name;
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
                        text += $"{Environment.NewLine}[<color=blue>port {i}</color>]: {Port.ports[i].GetPortName()}";
                    }
                }
            }
            else if (args[0].ToLower() == "items")
            {
                text = "found items:";
                for (int i = 0; i < PrefabsDirectory.instance.shipItems.Length; i++)
                {
                    if (PrefabsDirectory.instance.shipItems[i] != null)
                    {
                        text += $"{Environment.NewLine}[<color=blue>index {i}</color>]: {PrefabsDirectory.instance.shipItems[i].name}<color={LogColor.InternalName}> {PrefabsDirectory.instance.shipItems[i].gameObject.name}</color>";
                    }
                }
            }
            else if (args[0].ToLower() == "sails")
            {
                text = "found sails:";
                for (int i = 0; i < PrefabsDirectory.instance.sails.Length; i++)
                {
                    var obj = PrefabsDirectory.instance.sails[i];
                    if (obj != null)
                    {
                        text += $"{Environment.NewLine}[<color=blue>index {i}</color>]:{obj.GetComponent<Sail>().sailName}<color={LogColor.InternalName}> {obj.name}</color>";
                    }
                }
            }
            else
            {
                text = "unknown object type"; 
            }
            //text += Environment.NewLine;

            ModConsoleLog.Log(Plugin.instance.Info, Plugin.ClipEnd(text));
        }

    }
}

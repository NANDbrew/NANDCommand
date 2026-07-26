using System.Collections.Generic;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class SpawnItemCommand : Command
    {
        public override string Name => "SpawnItem";
        //public override string[] Aliases => new string[1] { "UnstuckMe" };

        public override string Usage => "SpawnItem <int id>";
        public override string Description => "Create the specified item at your crosshair. Use \"List items\" to find item IDs";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            var directory = SaveLoadManager.instance.GetComponent<PrefabsDirectory>();
            if (int.TryParse(args[0], out int id) && id >= 0 && id < directory.directory.Length)
            {
                GameObject proto = directory.directory[id];
                if (proto == null)
                {
                    ModConsoleLog.Error(Plugin.instance.Info, $"invalid item");
                    return;
                }

                GameObject obj = UnityEngine.Object.Instantiate(proto, Refs.ovrCameraRig.position + Refs.ovrCameraRig.forward, Refs.ovrCameraRig.rotation);
                obj.GetComponent<ShipItem>().sold = true;
                obj.GetComponent<SaveablePrefab>().RegisterToSave();
                obj.GetComponent<Good>()?.RegisterAsMissionless();
                ModConsoleLog.Log(Plugin.instance.Info, $"spawned item: {proto.name}");

            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, $"invalid item ID");
            }
            //ModConsoleLog.Log(Plugin.instance.Info, $"moved player");
        }

    }
}

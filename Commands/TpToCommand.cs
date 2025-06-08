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
    public class TpToCommand : Command
    {
        public override string Name => "TeleportTo";
        public override string[] Aliases => new string[1] { "TPTo" };

        public override string Usage => "<island index>";
        public override string Description => "Teleport to island scenery";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            Debug.Log("moving?");
            int index = Convert.ToInt32(args[0]);
            Transform island = Refs.islands[index];
            if (island == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find island");
                return;
            }
            FloatingOriginManager.instance.StartCoroutine(TPToIsland(island));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved to {island.name}");
        }

        public static IEnumerator TPToIsland(Transform island)
        {
            GameState.recovering = true;

            /*Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
            Vector3 targetPos = new Vector3(x, 20f, z) * 9000 + globeOffset;*/
            Refs.charController.transform.position = island.position + Vector3.up * 150;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            //yield return new WaitForSeconds(1);
            GameState.recovering = false;

        }
    }
}

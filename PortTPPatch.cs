using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using NANDCommand.Scripts;
using UnityEngine;

namespace NANDCommand
{
    [HarmonyPatch(typeof(Port), "Update")]
    internal static class PortPatches
    {
        private static bool Prefix(Port __instance, string ___portName, ref bool ___teleportPlayer)
        {
            if (!Plugin.patchPortTeleport.Value) return true;
            if (___teleportPlayer)
            {
                var targetPos = new Vector3(__instance.transform.position.x, __instance.transform.localPosition.y, __instance.transform.position.z);
                PlayerMover.MovePlayer(targetPos);
                ___teleportPlayer = false;
                Debug.Log("Debug teleporting player to " + ___portName);

            }
            return false;
        }

    }
}

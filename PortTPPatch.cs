using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDCommand
{
    [HarmonyPatch(typeof(Port), "Update")]
    internal static class PortPatches
    {
        private static void Prefix(Port __instance, string ___portName, ref bool ___teleportPlayer)
        {
            if (!Plugin.patchPortTeleport.Value) return;
            if (___teleportPlayer)
            {
                __instance.StartCoroutine(Teleport(__instance.transform));
                //___teleportPlayer = false;
                //Debug.Log("Debug teleporting player to " + ___portName);
                //return false;
            }
            //return true;
        }

        public static IEnumerator Teleport(Transform dest)
        {
            GameState.recovering = true;
            //Refs.charController.transform.position = dest.transform.position + Vector3.up * 50;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            GameState.recovering = false;
            Refs.charController.transform.position = dest.transform.position + Vector3.up * 2;

        }
    }
}

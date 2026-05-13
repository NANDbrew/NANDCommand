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
                //var targetPos = new Vector3(__instance.transform.position.x, __instance.transform.localPosition.y, __instance.transform.position.z);
                PlayerMover.MovePlayer(new Vector3(__instance.transform.position.x, 10, __instance.transform.position.z));
                ___teleportPlayer = false;
                Debug.Log("Debug teleporting player to " + ___portName);

            }
            return false;

        }

    }
}

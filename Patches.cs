using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NANDCommand
{
    internal class Patches
    {
        [HarmonyPatch(typeof(BoatDamage))]
        private static class BoatDamagePatches
        {
            [HarmonyPatch("Impact")]
            [HarmonyPrefix]
            public static void ImpactPatch(ref float force)
            {
                if (Plugin.ignoreDamage)
                {
                    force = 0;
                }
            }
            [HarmonyPatch("Impact")]
            [HarmonyPrefix]
            public static bool DailyDamagePatch()
            {
                return !Plugin.ignoreDamage;
            }
        }
    }
}

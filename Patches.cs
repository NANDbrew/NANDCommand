using HarmonyLib;
using SailwindConsole;
using System;
using UnityEngine;
using UnityEngine.UI;

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

        [HarmonyPatch(typeof(ModConsole), "MoveScrollToEnd")]
        private static class ConsoleTextLimit
        {
            private static void Prefix(ref Text ___logText)
            {
                if (___logText.text.Length > 20000)
                {
                    int amount = ___logText.text.Length - 20100;
                    ___logText.text = "..." + ___logText.text.Remove(0, amount);// ___logText.text.IndexOf(Environment.NewLine, amount));
                    Debug.Log($"NANDCommand: trimmed {amount} chars off mod console log");
                }
            }
        }
    }
}

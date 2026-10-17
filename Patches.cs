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
            public static bool ImpactPatch()
            {
                return !Plugin.ignoreDamage;
            }
            [HarmonyPatch("DailyDamage")]
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
                ___logText.text = Plugin.ClipStart(___logText.text);
            }
        }

        [HarmonyPatch(typeof(ModConsole), "OnEndEdit")]
        private static class ConsoleNextPage
        {
            private static bool Prefix(InputField ___consoleInput, ref bool ___inputFocused)
            {
                if (!string.IsNullOrEmpty(Plugin.consoleOverflow))
                {
                    if (string.IsNullOrEmpty(___consoleInput.text))
                    {
                        ModConsoleLog.Log(Plugin.instance.Info, Plugin.ClipEnd(Plugin.consoleOverflow));
                        ___consoleInput.Select();
                        ___consoleInput.ActivateInputField();
                        ___inputFocused = true;
                        return false;
                    }
                    else
                    {
                        ModConsoleLog.Log(Plugin.instance.Info, "<color=orange>Cancelled</color>");
                        Plugin.consoleOverflow = null;
                    }
                }
                return true;
            }
        }

/*        [HarmonyPatch(typeof(ModConsoleLog), "GetString")]
        private static class ConsoleTextClip
        {
            private static void Postfix(ref string __result)
            {
                __result = Plugin.ClipEnd(__result);
            }
        }*/

        [HarmonyPatch(typeof(ModConsole), "InitConsole")]
        internal static class ConsoleTextThing
        {
            internal static Text logText;
            private static void Postfix(Text ___logText)
            {
                logText = ___logText;
            }
        }
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NANDCommand
{
    internal class Patches
    {
        [HarmonyPatch(typeof(ShipItem))]
        private static class SomeClassPatch
        {
            [HarmonyPatch("Awake")]
            [HarmonyPrefix]
            public static void SomeMethodPatch()
            {

            }
        }
    }
}

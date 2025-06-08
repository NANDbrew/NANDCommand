using SailwindConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDCommand.Scripts
{
    internal class BoatFinder
    {
        public static Dictionary<string, int> boatNames = new Dictionary<string, int>
        {
            { "dhow", 10 },
            { "sanbuq", 20 },
            { "cog", 40 },
            { "brig", 50 },
            { "kakam", 90 },
            { "junk", 80 },
            { "jong", 70 },
            { "baghlah", 30 },
        };


        public static Transform FindBoat()
        {
            return GameState.lastBoat;
        }

        public static Transform FindBoat(string input)
        {
            Transform boat;
            if (!boatNames.TryGetValue(input, out int index) || !SaveLoadManager.instance.GetCurrentObjects()[index])
            {
                int.TryParse(input, out index);
            }
            boat = SaveLoadManager.instance.GetCurrentObjects()[index]?.GetComponent<PurchasableBoat>()?.transform ?? GameState.lastBoat;
            return boat;
        }
    }
}

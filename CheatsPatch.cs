using HarmonyLib;
using UnityEngine;

namespace NANDCommand
{
    [HarmonyPatch(typeof(GPButtonSteeringWheel), "ExtraFixedUpdate")]
    internal static class CheatsPatch
    {
        public static float cheatSpeed = 0f;
        private static void Postfix(GoPointer ___stickyClickedBy)
        {
            if (!___stickyClickedBy) return;
            if (cheatSpeed > 0)
            {
                if (GameInput.GetKey(InputName.MoveUp))
                {
                    Rigidbody bote = GameState.currentBoat.parent.GetComponent<Rigidbody>();
                    float run = GameInput.GetKey(InputName.Run) ? 3f : 1f;
                    bote.AddForceAtPosition(bote.transform.forward * cheatSpeed * run, bote.transform.position, ForceMode.Acceleration);
                }
                else if (GameInput.GetKey(InputName.MoveDown))
                {
                    Rigidbody bote = GameState.currentBoat.parent.GetComponent<Rigidbody>();
                    float run = GameInput.GetKey(InputName.Run) ? 1.5f : 1f;
                    bote.AddForceAtPosition(-bote.transform.forward * cheatSpeed * run, bote.transform.position, ForceMode.Acceleration);
                }
            }
        }
    }
}

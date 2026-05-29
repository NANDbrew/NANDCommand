using HarmonyLib;
using SailwindConsole;
using System.Collections;
using UnityEngine;

namespace NANDCommand.Scripts
{
    public class PlayerMover
    {
        static bool moving;
        public static void MovePlayer(Vector3 targetPos)
        {
            if (GameState.recovering || moving)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Can't teleport; already teleporting!");
                return;
            }
            moving = true;
            FloatingOriginManager.instance.StartCoroutine(MovePlayerToGlobePos(targetPos, null));
        }

        public static IEnumerator MovePlayerToGlobePos(Vector3 targetPos, Transform target)
        {
            Transform player = Refs.charController.transform;

            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            try
            {
                GameState.recovering = true;
                if (GameState.currentBoat != null)
                {
                    GameObject.FindObjectOfType<PlayerEmbarkerNew>().InvokePrivateMethod("PlayerDisembark", null);
                }

                if (target != null)
                {
                    player.position = target.position + (Vector3.up * 200);
                }
                else
                {
                    player.position = targetPos + (Vector3.up * 200);
                }

                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                RefsDirectory.instance.oceanRenderer.enabled = false;
                yield return new WaitForEndOfFrame();
                RefsDirectory.instance.oceanRenderer.enabled = true;

                if (target != null)
                {
                    player.position = target.position + targetPos;
                }
                else
                {
                    player.Translate(0, targetPos.y - player.position.y, 0);
                }
                yield return new WaitForSeconds(0.5f);
            }
            finally
            {
                GameState.recovering = false;
                moving = false;
                ModConsoleLog.Log(Plugin.instance.Info, "moved player to " + player.position);
                Debug.Log("teleported player to " + player.position);
            }
        }
    }
}

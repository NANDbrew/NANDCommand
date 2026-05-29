using HarmonyLib;
using SailwindConsole;
using System.Collections;
using UnityEngine;

namespace NANDCommand.Scripts
{
    public class PlayerMover
    {
        public static void MovePlayer(Vector3 targetPos)
        {
            if (GameState.recovering)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Cannot teleport; already teleporting!");
                return;
            }
            FloatingOriginManager.instance.StartCoroutine(MovePlayerToGlobePos(targetPos, null));
        }

        public static IEnumerator MovePlayerToGlobePos(Vector3 targetPos, Transform target)
        {
            Transform player = Refs.charController.transform;

            if (GameState.currentBoat != null)
            {
                GameObject.FindObjectOfType<PlayerEmbarkerNew>().InvokePrivateMethod("PlayerDisembark", null);
            }
            
            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            GameState.recovering = true;

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

            GameState.recovering = false;

            ModConsoleLog.Log(Plugin.instance.Info, "moved player to " + player.position);
            Debug.Log("teleported player to " + player.position);
        }
    }
}

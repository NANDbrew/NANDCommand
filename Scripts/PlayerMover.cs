using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace NANDCommand.Scripts
{
    public class PlayerMover
    {
        public static void MovePlayer(Vector3 targetPos, Transform target)
        {
            FloatingOriginManager.instance.StartCoroutine(MovePlayerToGlobePos(targetPos, target));
        }

        public static IEnumerator MovePlayerToGlobePos(Vector3 targetPos, Transform target)
        {
            Transform player = Refs.charController.transform;
            if (GameState.currentBoat != null)
            {
                Refs.observerMirror.transform.Translate(Vector3.up * 50);
                var embarkTrigger = Refs.observerMirror.GetComponentInChildren<PlayerEmbarkDisembarkTrigger>();
                AccessTools.Method(typeof(PlayerEmbarkDisembarkTrigger), "ExitBoat").Invoke(embarkTrigger, null);
            }
            //yield return new WaitForFixedUpdate();

            GameState.recovering = true;
            if (target != null)
            {
                player.position = target.position + Vector3.up * 200;
            }
            else
            {
                player.position = targetPos + Vector3.up * 200;
            }
            RefsDirectory.instance.oceanRenderer.enabled = false;

            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            //yield return new WaitForSecondsRealtime(1);
            GameState.recovering = false;
            RefsDirectory.instance.oceanRenderer.enabled = true;

            //yield return new WaitForSecondsRealtime(1);
            if (target != null)
            {
                player.position = target.position + targetPos;
            }
            else
            {
                player.position = new Vector3(player.position.x, targetPos.y, player.position.z);
            }


            Debug.Log("teleported player to " + player.position);
        }
    }
}

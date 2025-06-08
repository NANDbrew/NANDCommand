using Crest;
using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace NANDCommand.Scripts
{
    public class PlayerMover
    {
        public static void MovePlayer(Vector3 targetPos)
        {
            FloatingOriginManager.instance.StartCoroutine(MovePlayerToGlobePos(targetPos));
        }
        public static IEnumerator MovePlayerToGlobePos(Vector3 targetPos)
        {
            Transform player = Refs.charController.transform;
            if (GameState.currentBoat != null)
            {
                var embarkTrigger = Refs.observerMirror.GetComponentInChildren<PlayerEmbarkDisembarkTrigger>();
                AccessTools.Method(typeof(PlayerEmbarkDisembarkTrigger), "ExitBoat").Invoke(embarkTrigger, null);
            }
            GameState.recovering = true;
            player.position = targetPos + Vector3.up * 100;
            RefsDirectory.instance.oceanRenderer.enabled = false;
            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            GameState.recovering = false;
            RefsDirectory.instance.oceanRenderer.enabled = true;
            player.position = new Vector3(player.position.x, targetPos.y, player.position.z);

        }
    }
}

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
            FloatingOriginManager.instance.StartCoroutine(MovePlayerToGlobePos(targetPos, null));
        }

        public static IEnumerator MovePlayerToGlobePos(Vector3 targetPos, Transform target)
        {
            Transform player = Refs.charController.transform;
            if (GameState.currentBoat != null)
            {
                //player.Translate(Vector3.up * 50);
                
                GameObject.FindObjectOfType<PlayerEmbarkerNew>().InvokePrivateMethod("PlayerDisembark", null);

                //yield return new WaitForEndOfFrame();
            }
            //yield return new WaitForFixedUpdate();


            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            GameState.recovering = true;

            RefsDirectory.instance.oceanRenderer.enabled = false;
            //yield return new WaitForSecondsRealtime(1);
            if (target != null)
            {
                player.position = target.position + (Vector3.up * 200);
            }
            else
            {
                player.position = targetPos + (Vector3.up * 200);
            }            
            yield return new WaitForSeconds(0.5f);
            //yield return new WaitForSecondsRealtime(1);
            if (target != null)
            {
                player.position = target.position + targetPos;
            }
            else
            {
                player.Translate(0, targetPos.y - player.position.y, 0);
                //player.position = new Vector3(player.position.x, targetPos.y, player.position.z);
            }
            RefsDirectory.instance.oceanRenderer.enabled = true;
            yield return new WaitForSeconds(1f);

            GameState.recovering = false;

            ModConsoleLog.Log(Plugin.instance.Info, "moved player to " + player.position);
            Debug.Log("teleported player to " + player.position);
        }
    }
}

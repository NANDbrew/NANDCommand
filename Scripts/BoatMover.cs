using Crest;
using SailwindConsole;
using System.Collections;
using UnityEngine;

namespace NANDCommand.Scripts
{
    internal class BoatMover
    {
        public static bool movingBoat;
        public static IEnumerator IMoveBoat(Vector3 targetPos, Quaternion targetRot, Transform boat)
        {
            movingBoat = true;
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == false));

            try
            {
                GameState.recovering = true;
                //boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
                var damage = boat.GetComponent<BoatDamage>();
                BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
                ropes.UnmoorAllRopes();
                var anchor = ropes.anchor;
                Vector3 anchorPos = Vector3.zero;
                if (anchor) 
                {
                    ropes.GetAnchorController().ResetAnchor();
                    anchorPos = anchor.transform.position - boat.position;
                }
                //else ModConsoleLog.Warn(Plugin.instance.Info, "couldn't find boat's anchor!");

                damage.waterLevel = 0;
                damage.enabled = true;

                boat.GetComponent<BoatProbes>().dontUpdateVelocity = true;

                //yield return new WaitForFixedUpdate();
                //yield return new WaitForFixedUpdate();
                //yield return new WaitForEndOfFrame();
                //yield return new WaitForEndOfFrame();

                boat.position = targetPos;
                boat.GetComponent<Rigidbody>().velocity = Vector3.zero;

                if (anchor)
                {
                    anchor.transform.position = anchorPos + boat.position;
                    anchor.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    //ModConsoleLog.Log(Plugin.instance.Info, "moved anchor to " + anchor.transform.position);

                }

                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();

                if (damage.sunk)
                {
                    boat.transform.rotation = damage.GetSinkRotation();
                    boat.GetComponent<BoatLocalItems>().SetItemsLoaded(state: false);
                }

                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();

                boat.transform.rotation = targetRot;
                boat.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();

                boat.GetComponent<BoatProbes>().dontUpdateVelocity = false;
            }
            finally
            {
                GameState.recovering = false;
                movingBoat = false;
                ModConsoleLog.Log(Plugin.instance.Info, "moved boat " + boat.name);
            }
        }
    }
}

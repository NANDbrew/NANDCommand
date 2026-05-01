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
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == true));
            GameState.recovering = true;
            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            var damage = boat.GetComponent<BoatDamage>();
            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
            ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();

            damage.waterLevel = 0;
            damage.enabled = true;

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            boat.transform.position = targetPos;
            boat.GetComponent<Rigidbody>().velocity = Vector3.zero;

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

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            GameState.recovering = false;
            movingBoat = false;
        }
    }
}

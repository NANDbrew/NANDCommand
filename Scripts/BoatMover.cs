using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDCommand.Scripts
{
    internal class BoatMover
    {
        public static void MoveBoat(Vector3 targetPos, Quaternion targetRot, Transform boat)
        {

        }

        public static IEnumerator IMoveBoat(Vector3 targetPos, Quaternion targetRot, Transform boat)
        {
            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            boat.GetComponent<BoatDamage>().waterLevel = 0;
            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == true));
            //yield return new WaitForSeconds(2);
            ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            boat.transform.SetPositionAndRotation(targetPos, targetRot);
            boat.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}

using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class BringBoatCommand3 : Command
    {
        public override string Name => "bringToMe";
        public override string[] Aliases => new string[1] { "BTM" };
        public override string Usage => "[scene index or vanilla boat name]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to you. Will set boat as owned\nAlias: BTM\n DO NOT USE IF YOU ARE ON LAND";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }
            var player = Refs.observerMirror;
            Vector3 targetPos = player.transform.position;
            player.transform.Translate(Vector3.up * 20f);
            player.StartCoroutine(BoatMover.IMoveBoat(targetPos, boat.rotation, boat.transform));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved boat {boat.name}");
        }

/*        public static IEnumerator MoveBoatToPlayer(Transform player, Transform boat)
        {
            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            boat.GetComponent<BoatDamage>().waterLevel = 0;
            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == true));
            ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();
            //yield return new WaitForSeconds(0.2f);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            boat.transform.SetPositionAndRotation(targetPos, boat.rotation);
            boat.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }*/

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class BringBoatCommand : Command
    {
        public override string Name => "bringToShipyard";
        public override string[] Aliases => new string[1] { "BTS" };
        public override string Usage => "[scene index or vanilla boat name]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to the nearest shipyard (if loaded). Will set boat as owned\nAlias: BTS";

        public override void OnRun(List<string> args)
        {
            Transform boat = BoatFinder.FindBoat(args.Count > 0 ? args[0] : "");
            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            Shipyard shipyard = GameObject.FindObjectsOfType<Shipyard>().OrderBy(s => (s.transform.position - Refs.charController.transform.position).sqrMagnitude).FirstOrDefault();
            if (shipyard == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find shipyard");
                return;
            }

            shipyard.StartCoroutine(MoveBoatToShipyard(shipyard, boat.transform));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved boat {boat.name} to {shipyard.name}");
        }

        public static IEnumerator MoveBoatToShipyard(Shipyard shipyard, Transform boat)
        {
            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            boat.GetComponent<BoatDamage>().waterLevel = 0;
            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
            ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == true));
            yield return new WaitForSeconds(0.2f);

            boat.transform.SetPositionAndRotation(shipyard.shipReleasePosition.position, shipyard.shipReleasePosition.rotation);
            boat.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }

    }
}

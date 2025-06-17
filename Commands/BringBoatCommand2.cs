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
    public class BringBoatCommand2 : Command
    {
        public override string Name => "bringToPort";
        public override string[] Aliases => new string[1] { "BTP" };
        public override string Usage => "[scene index or vanilla boat name]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to the nearest recovery location. \nAlias: BTP";

        public override void OnRun(List<string> args)
        {
            Transform boat = args.Count > 0 ? BoatFinder.FindBoat(args[0]) : BoatFinder.FindBoat();
            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find boat");
                return;
            }

            RecoveryPort port = GameObject.FindObjectsOfType<RecoveryPort>().OrderBy(s => (s.transform.position - Refs.observerMirror.transform.position).sqrMagnitude).FirstOrDefault();
            if (port == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't find port");
                return;
            }
            port.StartCoroutine(MoveBoatToPort(port, boat.transform));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved boat {boat.name} to {port.name}");
        }
        
        public static IEnumerator MoveBoatToPort(RecoveryPort port, Transform boat)
        {
            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            boat.GetComponent<BoatDamage>().waterLevel = 0;
            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();
            ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();
            yield return new WaitUntil(() => (GameState.wasInSettingsMenu == true));
            yield return new WaitForSeconds(0.2f);

            boat.transform.SetPositionAndRotation(port.GetBoatPos(), port.boatPos.rotation);
            boat.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ropes.MoorClosestRope(port.mooringFront);
            ropes.MoorClosestRope(port.mooringBack);
        }

    }
}

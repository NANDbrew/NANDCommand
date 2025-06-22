using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class MoveBoatCommand : Command
    {
        public override string Name => "moveBoat";
        public override string Usage => "<lat> <long> [scene index or vanilla boat name] [bring nearby (-y)]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to lat/long. If \"bring nearby\" is on, will also teleport all owned boats within 100m of the primary boat";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            Debug.Log("moving?");
            float longitude = Convert.ToSingle(args[1]);
            float latitude = Convert.ToSingle(args[0]);

            bool bringNearby = args.Count > 2 && args.Last().ToLower() == "-y";
            //Transform boat = BoatFinder.FindBoat(args.Count > 2? args[2] : "");
            Transform boat = args.Count > 2 ? BoatFinder.FindBoat(args[2]) : BoatFinder.FindBoat();

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "couldn't find a boat to move");
                return;
            }
            FloatingOriginManager.instance.StartCoroutine(MoveBoatToGlobeCoords(longitude, latitude, bringNearby, boat));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved boat {boat.name} to {latitude}, {longitude}");

        }

        public static IEnumerator MoveBoatToGlobeCoords(float x, float z, bool bringNearby, Transform boat)
        {

            boat.GetComponent<PurchasableBoat>().LoadAsPurchased();
            boat.GetComponent<BoatDamage>().waterLevel = 0;

            bool movingPlayer = GameState.currentBoat;
            if (movingPlayer) GameState.recovering = true;

            Dictionary<PickupableBoatMooringRope, GameObject> ropeMoorings = new Dictionary<PickupableBoatMooringRope, GameObject>();

            Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
            Vector3 currentPos = FloatingOriginManager.instance.ShiftingPosToRealPos(boat.position);
            Vector3 targetPos = new Vector3(x, 0f, z) * 9000 + globeOffset;
            targetPos = new Vector3(targetPos.x, currentPos.y, targetPos.z);

            BoatMooringRopes ropes = boat.GetComponent<BoatMooringRopes>();

            foreach (var rope in ropes.ropes)
            {
                if (rope.IsMoored() && !rope.transform.parent.CompareTag("Boat"))
                {
                    rope.Unmoor();
                    rope.ResetRopePos();
                }
            }            //ropes.UnmoorAllRopes();
            ropes.GetAnchorController().ResetAnchor();

            PurchasableBoat[] nearbyBoats = new PurchasableBoat[0];
            Vector3[] relVectors = new Vector3[0];
            if (bringNearby)
            {

                // Record nearby boat positions relative to main boat
                nearbyBoats = GameObject.FindObjectsOfType<PurchasableBoat>().Where(o => (o.transform != boat && o.isPurchased() && (o.transform.position - GameState.lastBoat.transform.position).sqrMagnitude < 10000)).ToArray();
                relVectors = new Vector3[nearbyBoats.Length];
                for (int i = 0; i < nearbyBoats.Length; i++)
                {
                    var nearBoat = nearbyBoats[i];
                    var NBropeSet = nearBoat.GetComponent<BoatMooringRopes>();
                    NBropeSet.GetAnchorController().ResetAnchor();
                    foreach (var rope in NBropeSet.ropes)
                    {
                        if (rope.IsMoored() && !rope.transform.parent.CompareTag("Boat"))
                        {
                            rope.Unmoor();
                            rope.ResetRopePos();
                        }
                    }
                    relVectors[i] = nearBoat.transform.position - boat.position;
                }
            }

            // Move main boat and set safe rotation
            boat.position = FloatingOriginManager.instance.RealPosToShiftingPos(targetPos);
            boat.eulerAngles = new Vector3(0f, boat.eulerAngles.y, 0f);
            if (bringNearby)
            {
                // Move nearby boats
                for (int i = 0; i < nearbyBoats.Length; i++)
                {
                    var nearBoat = nearbyBoats[i];
                    nearBoat.GetComponent<BoatDamage>().waterLevel = 0;
                    nearBoat.transform.position = relVectors[i] + boat.position;
                    nearBoat.transform.eulerAngles = new Vector3(0f, nearBoat.transform.eulerAngles.y, 0f);
                    ModConsoleLog.Log(Plugin.instance.Info, $"moved nearby boat {nearBoat.name}");
                }
            }
            if (movingPlayer)
            {
                RefsDirectory.instance.oceanRenderer.enabled = false;
                yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
                GameState.recovering = false;
                RefsDirectory.instance.oceanRenderer.enabled = true;
            }
        }
    }
}

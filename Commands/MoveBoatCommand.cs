using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public override string Usage => "<lat> <long> [index or vanilla boat name] [bring nearby (-y)]";
        public override string Description => "Teleport a boat (or current/last boat if unspecified) to lat/long. If \"bring nearby\" is on, will also teleport all owned boats within 100m of the primary boat";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            if (GameState.recovering)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't move; already moving");
                return;
            }
/*            if (FloatingOriginManager.ShiftingThisFrame)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "can't move; already moving");
                return;
            }*/
            float longitude;
            float latitude;
            if (args[0].ToLower() == "random" && args[1].ToLower() == "location")
            {
                longitude = UnityEngine.Random.Range(-12, 32);
                latitude = UnityEngine.Random.Range(26, 46);
            }
            else
            {
                longitude = Convert.ToSingle(args[1]);
                latitude = Convert.ToSingle(args[0]);
            }
            Transform boat = BoatFinder.FindBoat();
            bool bringNearby = (args.Count > 2 && args.Last().ToLower() == "-y");
            if (args.Count > 2)
            {
                if (args[2].ToLower() != "-y")
                {
                    boat = BoatFinder.FindBoat(args[2]);
                } 
            }

            if (boat == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "couldn't find a boat to move");
                return;
            }

            Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
            Vector3 targetPos = new Vector3(longitude, 0f, latitude) * 9000 + globeOffset + Vector3.up * 5;
            targetPos = FloatingOriginManager.instance.RealPosToShiftingPos(targetPos);

            PurchasableBoat[] nearbyBoats = new PurchasableBoat[0];
            Vector3[] relVectors = new Vector3[0];
            if (bringNearby)
            {
                nearbyBoats = GameObject.FindObjectsOfType<PurchasableBoat>().Where(o => (o.transform != boat && o.isPurchased() && (o.transform.position - boat.position).sqrMagnitude < 10000)).ToArray();
                relVectors = new Vector3[nearbyBoats.Length];
                for (int i = 0; i < nearbyBoats.Length; i++)
                {
                    relVectors[i] = nearbyBoats[i].transform.position - boat.position;
                }
            }

            FloatingOriginManager.instance.StartCoroutine(BoatMover.IMoveBoat(targetPos, boat.rotation, boat));
            ModConsoleLog.Log(Plugin.instance.Info, $"moving boat {boat.name} to {latitude}, {longitude}...");

            if (bringNearby && nearbyBoats.Length > 0 && relVectors.Length > 0)
            {
                for (int j = 0; j < nearbyBoats.Length; j++)
                {
                    Transform nb = nearbyBoats[j].transform;
                    FloatingOriginManager.instance.StartCoroutine(BoatMover.IMoveBoat(targetPos + relVectors[j], nb.transform.rotation, nb));
                    ModConsoleLog.Log(Plugin.instance.Info, $"moving boat {nb.name}...");

                }
            }

        }

    }
}

using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NANDCommand.Commands
{
    public class BringToMeCommand : Command
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
            if (GameState.currentBoat != null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "already on a boat. don't know where to put new one");
                return;
            }

            var player = Refs.charController.transform;
            Vector3 targetPos = player.transform.position;
            //player.position += (Vector3.up * 20f);
            Plugin.instance.StartCoroutine(DoTheThing(player));
            Plugin.instance.StartCoroutine(BoatMover.IMoveBoat(targetPos, boat.rotation, boat.transform));
            ModConsoleLog.Log(Plugin.instance.Info, $"moving boat {boat.name}...");
        }

        internal static IEnumerator DoTheThing(Transform player)
        {
            yield return new WaitUntil(() => !GameState.wasInSettingsMenu);
            player.Translate(Vector3.up * 20);
        }
    }
}

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
    public class MovePlayerCommand : Command
    {
        public override string Name => "movePlayer";
        public override string Usage => "<lat> <long>";
        public override string Description => "Teleport to globe coords";
        public override int MinArgs => 2;

        public override void OnRun(List<string> args)
        {
            Debug.Log("moving?");
            float longitude = Convert.ToSingle(args[1]);
            float latitude = Convert.ToSingle(args[0]);

            FloatingOriginManager.instance.StartCoroutine(TPToGlobeCoords(longitude, latitude));
            ModConsoleLog.Log(Plugin.instance.Info, $"moved to {latitude}, {longitude}");
        }

        public static IEnumerator TPToGlobeCoords(float x, float z)
        {
            GameState.recovering = true;

            Vector3 globeOffset = (Vector3)Traverse.Create(FloatingOriginManager.instance).Field("globeOffset").GetValue();
            Vector3 targetPos = new Vector3(x, 20f, z) * 9000 + globeOffset;
            Refs.charController.transform.position = FloatingOriginManager.instance.RealPosToShiftingPos(targetPos);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            //yield return new WaitForSeconds(1);
            GameState.recovering = false;

        }
    }
}

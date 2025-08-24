using SailwindConsole;
using SailwindConsole.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NANDCommand.Commands
{
    internal class SetWeatherCommand : Command
    {
        public override string Name => "setWeather";
        public override int MinArgs => 1;
        public override string Usage => "<weather set (clear, cloudy, rain, storm)> [seconds]";
        public override string Description => "Change the weather for the specified number of frames, or 1000 if unspecified";

        private int coroutinesRunning = 0;
        public override void OnRun(List<string> args)
        {
            int seconds = 10; // Default seconds if not specified
            if (args.Count == 2 && int.TryParse(args[1], out seconds))
            {
                // Valid second argument, use it as seconds
            }

            if (args[0].ToLower() == "clear")
            {
                Weather.instance.StartCoroutine(ForceWeather(RegionBlender.instance.blendedRegion.clearWeather, seconds));
                ModConsoleLog.Log(Plugin.instance.Info, "Setting weather to clear for " + seconds + " seconds");
            }
            else if (args[0].ToLower() == "cloudy")
            {
                Weather.instance.StartCoroutine(ForceWeather(RegionBlender.instance.blendedRegion.cloudyWeather, seconds));
                ModConsoleLog.Log(Plugin.instance.Info, "Setting weather to cloudy for " + seconds + " seconds");
            }
            else if (args[0].ToLower() == "rain")
            {
                Weather.instance.StartCoroutine(ForceWeather(RegionBlender.instance.blendedRegion.rainWeather, seconds));
                ModConsoleLog.Log(Plugin.instance.Info, "Setting weather to rain for " + seconds + " seconds");
            }
            else if (args[0].ToLower() == "storm")
            {
                Weather.instance.StartCoroutine(ForceWeather(RegionBlender.instance.blendedRegion.stormWeather, seconds));
                ModConsoleLog.Log(Plugin.instance.Info, "Setting weather to storm for " + seconds + " seconds");
            }
            else
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Not a valid weather type!");
                return;
            }
        }

        private IEnumerator ForceWeather(WeatherSet weather, float seconds)
        {
            coroutinesRunning++;
            if (coroutinesRunning > 1)                 
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
            while (seconds > 0)
            {
                if (coroutinesRunning > 1)
                {
                    Debug.Log("Cancelling weather routine with " + System.Math.Round(seconds, 1) + " seconds left to make way for a new one");
                    break;
                }
                Weather.instance.ChangeWeather(weather);
                yield return new WaitForEndOfFrame();
                seconds -= Time.deltaTime;
            }
            coroutinesRunning--;
        } 
    }
}

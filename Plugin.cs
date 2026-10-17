using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NANDCommand.Commands;
using SailwindConsole;
using System;
using System.Reflection;

namespace NANDCommand
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInDependency("com.app24.sailwindconsole", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.nandcommand";
        public const string PLUGIN_NAME = "NANDCommand";
        public const string PLUGIN_VERSION = "1.0.16";

        //--settings--
        internal static int logLimit = 20000;
        internal static ConfigEntry<bool> commandOverrides;

        public static string consoleOverflow;

        internal static bool ignoreDamage = false;

        internal static Plugin instance;

        public void Awake()
        {
            instance = this;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            //ModConsole.AddCommand(new MovePlayerCommand());
            ModConsole.AddCommand(new MoveBoatCommand());
            ModConsole.AddCommand(new BringToShipyardCommand());
            ModConsole.AddCommand(new BringToPortCommand());
            ModConsole.AddCommand(new BringToMeCommand());
            ModConsole.AddCommand(new SetTimescaleCommand());
            ModConsole.AddCommand(new TpToCommand());
            ModConsole.AddCommand(new ExportInfoCommand());
            ModConsole.AddCommand(new CheatSpeedCommand());
            ModConsole.AddCommand(new FixMeCommand());
            ModConsole.AddCommand(new SetWindKnotsCommand());
            ModConsole.AddCommand(new SetWeatherCommand());
            ModConsole.AddCommand(new GetDistanceCommand());
            ModConsole.AddCommand(new ToggleDamageCommand());
            ModConsole.AddCommand(new SmokeFoodCommand());
            ModConsole.AddCommand(new GetDamageCommand());
            ModConsole.AddCommand(new SetDamageCommand());
            ModConsole.AddCommand(new GetWaterCommand());
            ModConsole.AddCommand(new SetWaterCommand());
            ModConsole.AddCommand(new SetOwnedCommand());
            ModConsole.AddCommand(new ListCommand());
            ModConsole.AddCommand(new AddMoneyCommand());
            ModConsole.AddCommand(new CleanBoatCommand());
            ModConsole.AddCommand(new SetLevelCommand());
            ModConsole.AddCommand(new GetHeadingCommand());
            ModConsole.AddCommand(new SetWalkSpeedCommand());
            ModConsole.AddCommand(new SpawnItemCommand());
            ModConsole.AddCommand(new SetProteinCommand());
            ModConsole.AddCommand(new SetVitaminsCommand());
            ModConsole.AddCommand(new GetPriceCommand());
            ModConsole.AddCommand(new GetPriceReportCommand());
            ModConsole.AddCommand(new GetNeedsCommand());
            ModConsole.AddCommand(new ClearCommand());
            ModConsole.AddCommand(new SetNeedsCommand());
            ModConsole.AddCommand(new FillNeedsCommand());
            ModConsole.AddCommand(new NextPageCommand());
            ModConsole.AddCommand(new EchoCommand());
            ModConsole.AddCommand(new GetWeightCommand());
            ModConsole.AddCommand(new FindItemCommand());
            ModConsole.AddCommand(new SmallItemsCommand());

            commandOverrides = Config.Bind("Settings", "Command overrides", true, new ConfigDescription("Replace the base console mod's Teleport and GetWeight commands with this mod's improved versions"));

            if (commandOverrides.Value)
            {
                ModConsole.AddCommand(new TeleportCommand());
            }
        }

        public static string ClipStart(string text)
        {
            if (text.Length > logLimit)
            {
                int cutPoint = text.IndexOf(Environment.NewLine, text.Length - logLimit);
                UnityEngine.Debug.Log($"NANDCommand: trimmed {cutPoint} chars off mod console log start");
                text = "*snip*" + text.Substring(cutPoint);
            }
            return text;
        }

        public static string ClipEnd(string text)
        {
            if (text.Length > logLimit)
            {
                int cutPoint = text.LastIndexOf(Environment.NewLine, logLimit - 200);

                consoleOverflow = text.Substring(cutPoint);
                UnityEngine.Debug.Log($"NANDCommand: trimmed {text.Length - cutPoint} chars off mod console log end");

                text = text.Substring(0, cutPoint);
                text += Environment.NewLine + $"<color=green>Continues... press enter</color>";

            }
            else { consoleOverflow = null; }
            
            return text;
        }
    }

    internal static class LogColor
    {
        internal static string InternalName = "#606060";
        internal static string Orange = "#a05500";
    }
}

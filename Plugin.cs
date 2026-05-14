using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NANDCommand.Commands;
using SailwindConsole;
using System.Reflection;

namespace NANDCommand
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInDependency("com.app24.sailwindconsole", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.nandcommand";
        public const string PLUGIN_NAME = "NANDCommand";
        public const string PLUGIN_VERSION = "1.0.8";

        //--settings--
        internal static ConfigEntry<bool> patchPortTeleport;

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


            patchPortTeleport = Config.Bind("Settings", "Patch Port teleport", true, new ConfigDescription("Patch SailwindConsole's Teleport command so it puts you on the ground"));

        }
    }
}

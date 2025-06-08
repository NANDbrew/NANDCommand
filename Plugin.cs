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
    [BepInDependency("com.app24.sailwindconsole", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.nandcommand";
        public const string PLUGIN_NAME = "NANDCommand";
        public const string PLUGIN_VERSION = "1.0.0";

        //--settings--
        //internal ConfigEntry<bool> someSetting;

        internal static Plugin instance;

        public void Awake()
        {
            instance = this;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            ModConsole.AddCommand(new MovePlayerCommand());
            ModConsole.AddCommand(new MoveBoatCommand());
            ModConsole.AddCommand(new BringBoatCommand());
            ModConsole.AddCommand(new SetTimescaleCommand());
            ModConsole.AddCommand(new TpToCommand());
            ModConsole.AddCommand(new ExportInfoCommand());
            //someSetting = Config.Bind("Settings", "Some setting", false);

        }
    }
}

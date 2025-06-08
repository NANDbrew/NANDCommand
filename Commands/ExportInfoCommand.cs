using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using NANDCommand.Scripts;
using SailwindConsole;
using SailwindConsole.Commands;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace NANDCommand.Commands
{
    public class ExportInfoCommand : Command
    {
        public override string Name => "ExportInfo";
        public override string Usage => "<ExportInfo> <parts, objects, food>";
        public override string Description => "Export info to a .csv in \"Documents/Sailwind info dump\"";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            //string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Sailwind info dump");
            string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sailwind info dump");
            if (!Directory.Exists(docPath)) Directory.CreateDirectory(docPath);
            if (args[0].ToLower() == "parts") 
            {
                Transform boat = BoatFinder.FindBoat(args.Count > 1 ? args[1] : "");
                if (boat == null)
                {
                    ModConsoleLog.Log(Plugin.instance.Info, "Can't find boat. Exporting all");
                    foreach (var obj in SaveLoadManager.instance.GetCurrentObjects())
                    {
                        if (obj != null && obj.GetComponent<BoatCustomParts>() is BoatCustomParts parts)
                        {
                            // special file writing
                            File.WriteAllText(Path.Combine(docPath, obj.name + ".csv"), GetBoatPartsInfo(parts));
                        }
                    }
                }
                else
                {
                    File.WriteAllText(Path.Combine(docPath, boat.name + ".csv"), GetBoatPartsInfo(boat.GetComponent<BoatCustomParts>()));
                }
            }
            else if (args[0].ToLower() == "food")
            {
                File.WriteAllText(Path.Combine(docPath, "food items" + ".csv"), GetFoodInfo());
            }
            else if (args[0].ToLower() == "objects")
            {
                File.WriteAllText(Path.Combine(docPath, "object indexes" + ".csv"), GetObjectIndexes());
            }

            
        }

        public static string GetObjectIndexes()
        {
            string text = "";
            SaveableObject[] list = SaveLoadManager.instance.GetCurrentObjects();
            for (int i = 1; i < list.Length; i++)
            {
                SaveableObject obj = list[i];
                if (obj != null)
                {
                    text += obj.name;
                }
                text += Environment.NewLine;
            }
            return text;
        }

        public static string GetFoodInfo()
        {
            string text = "name,protein,vitamins,energy per bite,raw energy mult,spoil time,slice count,mass,value" + Environment.NewLine;
            string separator = ",";
            for (int i = 1; i < PrefabsDirectory.instance.directory.Length; i++)
            {
                GameObject obj = PrefabsDirectory.instance.directory[i];
                if (obj != null)
                {
                    if (obj.GetComponent<ShipItemFood>() is ShipItemFood food)
                    {
                        text += food.name + separator;
                        text += food.GetPrivateField("protein").ToString() + separator;
                        text += food.GetPrivateField("vitamins").ToString() + separator;
                        text += food.GetEnergyPerBite().ToString() + separator;
                        text += food.GetPrivateField("rawEnergyMult").ToString() + separator;
                        if (food.GetComponent<FoodState>() is FoodState state)
                        {
                            Debug.Log("found food state component");
                            text += state.spoilDuration.ToString() + separator;
                            text += state.slicesCount.ToString() + separator;
                        }
                        text += food.mass + separator;
                        text += food.value;
                        text += Environment.NewLine;
                    }
                }
            }
            return text;
        }
        public static string GetBoatPartsInfo(BoatCustomParts parts)
        {
            string separator = ",";
            Debug.Log("attempting to save part info");
            string text = "index,transform,option name,base price,install cost,mass,mast height,max. sails,index" + Environment.NewLine;
            for (int i = 0; i < parts.availableParts.Count; i++)
            {
                var part = parts.availableParts[i];
                text += i.ToString() + Environment.NewLine;
                for (int j = 0; j < part.partOptions.Count; j++)
                {
                    var partOption = part.partOptions[j];
                    text += j.ToString() + separator;
                    text += partOption.name + separator;
                    text += partOption.optionName + separator;
                    text += partOption.basePrice.ToString() + separator;
                    text += partOption.installCost.ToString() + separator;
                    text += partOption.mass;

                    Mast mast = partOption.gameObject.GetComponent<Mast>();
                    if (mast != null)
                    {
                        text += separator;
                        text += mast.mastHeight.ToString() + separator;
                        text += mast.maxSails.ToString();
                        text += separator + mast.orderIndex.ToString();
                        //text += mast.extraBottomHeight;
                    }
                    text += Environment.NewLine;
                }
            }
            // Write the text to a new file named "WriteFile.txt".
            return text;
        }


    }
}

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
        public override string Usage => "<parts, indexes, items, food, islands> [scene index or vanilla boat name]";
        public override string Description => "Export info to a .csv in \"Documents/Sailwind info dump\"";
        public override int MinArgs => 1;

        public override void OnRun(List<string> args)
        {
            Dictionary<string, string> texts = new Dictionary<string, string>();
            //string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Sailwind info dump");
            string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sailwind info dump");
            //try
            //{
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
                                texts.Add(obj.name, GetBoatPartsInfo(parts));
                                // special file writing
                                //File.WriteAllText(Path.Combine(docPath, obj.name + ".csv"), GetBoatPartsInfo(parts));
                            }
                        }
                    }
                    else
                    {
                        texts.Add(boat.name, GetBoatPartsInfo(boat.GetComponent<BoatCustomParts>()));
                        //File.WriteAllText(Path.Combine(docPath, boat.name + ".csv"), GetBoatPartsInfo(boat.GetComponent<BoatCustomParts>()));
                    }
                }
                else if (args[0].ToLower() == "food")
                {
                    texts.Add("food items", GetFoodInfo());
                    //File.WriteAllText(Path.Combine(docPath, "food items" + ".csv"), GetFoodInfo());
                }
                else if (args[0].ToLower() == "indexes")
                {
                    texts.Add("object indexes", GetObjectIndexes());
                    //File.WriteAllText(Path.Combine(docPath, "object indexes" + ".csv"), GetObjectIndexes());
                }
                else if (args[0].ToLower() == "items")
                {
                    texts.Add("items", GetItemInfo());
                    //File.WriteAllText(Path.Combine(docPath, "items" + ".csv"), GetItemInfo());
                }
                else if (args[0].ToLower() == "islands")
                {
                    texts.Add("islands", GetIslandsInfo());
                }

                foreach (var text in texts)
                {
                    string path = Path.Combine(docPath, text.Key + ".csv");
                    File.WriteAllText(path, text.Value);
                    ModConsoleLog.Log(Plugin.instance.Info, $"Wrote file: {path}");
                }
            //}
            //catch
            //{
            //    ModConsoleLog.Error(Plugin.instance.Info, "Failed to export");
            //}
        }

        public static string GetIslandsInfo()
        {

            string separator = ",";
            string text = "index,name,lat,long,port index,port name,has carrier,currency exchange,main import,main export" + Environment.NewLine;
            for (int i = 0; i < Refs.islands.Count(); i++)
            {
                text += i + separator;
                if (Refs.islands[i]?.GetComponent<IslandHorizon>() is IslandHorizon horizon)
                {
                    Transform center = horizon.overrideCenter ?? Refs.islands[i];
                    Vector3 coords = FloatingOriginManager.instance.GetGlobeCoords(center);
                    text += horizon.name + separator;
                    text += coords.z + separator;
                    text += coords.x + separator;

                    if (horizon.economy is IslandEconomy econ && econ.GetComponent<Port>() is Port port)
                    {
                        text += port.portIndex + separator;
                        text += port.GetPortName() + separator;
                        text += (i < CargoCarrier.carriers.Length && CargoCarrier.carriers[i] != null)? "yes" : "no";
                        text += separator;

                        if (econ.GetComponent<IslandMarket>() is IslandMarket market)
                        {
                            text += market.allowCurrencyConversion? "yes" : "no";
                            text += separator;
                            KeyValuePair<int, float> highest = new KeyValuePair<int, float>();
                            KeyValuePair<int, float> lowest = new KeyValuePair<int, float>();
                            for (int j = 0; j < market.production.Length; j++)
                            {
                                var good = market.production[j];
                                if (good > 0 && good > highest.Value)
                                {
                                    highest = new KeyValuePair<int, float>(j, good);
                                }
                                else if (good < 0 && good < lowest.Value)
                                {
                                    lowest = new KeyValuePair<int, float>(j, good);
                                }
                            }
                            text += PrefabsDirectory.instance.GetGood(lowest.Key).name + separator;
                            text += PrefabsDirectory.instance.GetGood(highest.Key).name + separator;
                        }
                    }
                }
                text += Environment.NewLine;
            }
            return text;
        }

        public static string GetObjectIndexes()
        {
            string text = "index, name" + Environment.NewLine;
            SaveableObject[] list = SaveLoadManager.instance.GetCurrentObjects();
            for (int i = 1; i < list.Length; i++)
            {
                SaveableObject obj = list[i];
                if (obj != null)
                {
                    text += obj.sceneIndex + ",";
                    text += obj.name;
                }
                else text += i;
                text += Environment.NewLine;
            }
            return text;
        }
        public static string GetItemInfo()
        {
            string text = "index,name,mass,value" + Environment.NewLine;
            string separator = ",";
            for (int i = 1; i < PrefabsDirectory.instance.directory.Length; i++)
            {
                GameObject obj = PrefabsDirectory.instance.directory[i];
                if (obj != null)
                {
                    if (obj.GetComponent<ShipItem>() is ShipItem item)
                    {
                        text += i + separator;
                        text += item.name + separator;
                        text += item.mass + separator;
                        text += item.value;
                        text += Environment.NewLine;
                    }
                }
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

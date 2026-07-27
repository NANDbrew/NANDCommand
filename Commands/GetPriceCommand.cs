using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class GetPriceCommand : Command
    {
        public override string Name => "getPrice";
        public override int MinArgs => 1;
        public override string Usage => "<good>";

        public override string Description => "Get the price of a good at all ports";

        public override void OnRun(List<string> args)
        {
            //PriceReport prices;
            
            string text = "";
            //Port targetPort = GameState.lastVisitedPort;
            /*            if (args.Count > 1)
                        {
                            string portName = string.Join(" ", args.GetRange(1, args.Count - 1));
                            foreach (Port port in Port.ports)
                            {
                                if (port && port.GetPortName().ToLower() == portName.ToLower())
                                {
                                    targetPort = port;
                                    break;
                                }
                            }
                        }*/
            string itemName = string.Join(" ", args);

            int goodIndex = -1;
            foreach (var port in Port.ports)
            {
                if (port == null) continue;
                PriceReport report = port.island.GetComponent<IslandMarket>()?.GetSelfPriceReport();
                if (report == null) continue;
                if (goodIndex == -1)
                {
                    for (int i = 0; i < report.buyPrices.Length; i++)
                    {
                        if (PrefabsDirectory.instance.GetGood(i) is ShipItem item && item.name == itemName)
                        {
                            goodIndex = i;
                            break;
                        }
                    }
                }

                string buy = (report.buyPrices[goodIndex] != 0) ? report.buyPrices[goodIndex].ToString() : "n/a";
                string sell = (report.sellPrices[goodIndex] != 0) ? report.sellPrices[goodIndex].ToString() : "n/a";

                text += $"\nat {port.GetPortName()} => buy: {buy}, sell: {sell}";

            }
/*            if (targetPort == null) 
            { 
                ModConsoleLog.Error(Plugin.instance.Info, "Invalid port"); 
                return;
            }

            prices = targetPort.island.GetComponent<IslandMarket>()?.GetSelfPriceReport();
            if (prices == null)
            {
                ModConsoleLog.Error(Plugin.instance.Info, "Couldn't get price report");
                return;
            }
            for (int i = 0; i < prices.buyPrices.Length; i++)
            {
                if (PrefabsDirectory.instance.GetGood(i) is ShipItem item && item.name == itemName)
                {
                    string buy = (prices.buyPrices[i] != 0) ? prices.buyPrices[i].ToString() : "n/a";
                    string sell = (prices.sellPrices[i] != 0) ? prices.sellPrices[i].ToString() : "n/a";

                    text += $"buy: {buy}, sell: {sell}";
                    break;
                }
            }*/


            ModConsoleLog.Log(Plugin.instance.Info, $"Prices for {itemName}: {text}");

            //ModConsoleLog.Error(Plugin.instance.Info, "Cannot have a value below 0!");
            
        }

    }
}

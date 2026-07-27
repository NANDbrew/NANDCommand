using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindConsole;
using SailwindConsole.Commands;

namespace NANDCommand.Commands
{
    internal class GetPriceReportCommand : Command
    {
        public override string Name => "getPriceReport";
        public override int MinArgs => 1;
        public override string Usage => "<port>";

        public override string Description => "Get a full price report from a port";

        public override void OnRun(List<string> args)
        {
            PriceReport prices;
            string text = "";

            Port targetPort = GameState.lastVisitedPort;

            string portName = string.Join(" ", args);
            foreach (Port port in Port.ports)
            {
                if (port && port.GetPortName().ToLower() == portName.ToLower())
                {
                    targetPort = port;
                    break;
                }
            }

            if (targetPort == null) 
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
                if (PrefabsDirectory.instance.GetGood(i) is ShipItem item)
                {
                    string buy = (prices.buyPrices[i] != 0) ? prices.buyPrices[i].ToString() : "n/a";
                    string sell = (prices.sellPrices[i] != 0) ? prices.sellPrices[i].ToString() : "n/a";

                    text += $"\n  {item.name} => buy: {buy}, sell: {sell}";
                }
            }


            ModConsoleLog.Log(Plugin.instance.Info, $"Prices at {targetPort.GetPortName()}:{text}");

            //ModConsoleLog.Error(Plugin.instance.Info, "Cannot have a value below 0!");
            
        }

    }
}

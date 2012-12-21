using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JM.Core;

namespace JM.DCJ.Vehicle
{
    internal static class Resources
    {
        public static readonly string READ_CURRENT_TROUBLE_CODE = Database.GetText("Read Current Trouble Code", "System");
        public static readonly string READ_HISTORY_TROUBLE_CODE = Database.GetText("Read History Trouble Code", "System");
        public static readonly string READ_DATA_STREAM = Database.GetText("Read Data Stream", "System");
        public static readonly string READ_ECU_VERSION = Database.GetText("Read ECU Version", "System");
        public static readonly string TPS_IDLE_SETTING = Database.GetText("TPS Idle Setting", "System");
        public static readonly string LONG_TERM_LEARN_VALUE_ZONE_INITIALIZATION = Database.GetText("Long Term Learn Value Zone Initialization", "Mikuni");
        public static readonly string ISC_LEARN_VALUE_INITIALIZATION = Database.GetText("ISC Learn Value Initialize", "Mikuni");
        public static readonly string COMMUNICATING = Database.GetText("Communicating", "System");
        public static readonly string TPS_IDLE_SETTING_SUCCESS = Database.GetText("TPS Idle Setting Success", "Mikuni");
        public static readonly string LONG_TERM_LEARN_VALUE_ZONE_INITIALIZATION_SUCCESS = Database.GetText("Long Term Learn Value Zone Initialization Success", "Mikuni");
        public static readonly string ISC_LEARN_VALUE_INITIALIZATION_SUCCESS = Database.GetText("ISC Learn Value Initialization Success", "Mikuni");
        private static Dictionary<string, string> mikuni16ATroubleCodes;
        private static Dictionary<string, string> mikuni16CTroubleCodes;
        private static Dictionary<string, string> gw250TroubleCodes;

        private static LiveDataVector liveDataVector;

        static Resources()
        {
            mikuni16ATroubleCodes = new Dictionary<string, string>();
            mikuni16ATroubleCodes["0040"] = "00";
            mikuni16ATroubleCodes["0080"] = "00";
            mikuni16ATroubleCodes["0140"] = "01";
            mikuni16ATroubleCodes["0180"] = "01";
            mikuni16ATroubleCodes["0240"] = "02";
            mikuni16ATroubleCodes["0280"] = "02";
            mikuni16ATroubleCodes["0340"] = "03";
            mikuni16ATroubleCodes["0380"] = "03";
            mikuni16ATroubleCodes["0540"] = "05";
            mikuni16ATroubleCodes["0580"] = "05";
            mikuni16ATroubleCodes["0640"] = "06";
            mikuni16ATroubleCodes["0680"] = "06";
            mikuni16ATroubleCodes["0740"] = "07";
            mikuni16ATroubleCodes["0780"] = "07";
            mikuni16ATroubleCodes["0840"] = "08";
            mikuni16ATroubleCodes["0880"] = "08";
            mikuni16ATroubleCodes["0940"] = "09";
            mikuni16ATroubleCodes["0980"] = "09";
            mikuni16ATroubleCodes["2040"] = "20";
            mikuni16ATroubleCodes["2080"] = "20";
            mikuni16ATroubleCodes["2140"] = "21";
            mikuni16ATroubleCodes["2180"] = "21";
            mikuni16ATroubleCodes["2240"] = "22";
            mikuni16ATroubleCodes["2280"] = "22";
            mikuni16ATroubleCodes["2340"] = "23";
            mikuni16ATroubleCodes["2380"] = "23";
            mikuni16ATroubleCodes["2440"] = "24";
            mikuni16ATroubleCodes["2480"] = "24";
            mikuni16ATroubleCodes["4040"] = "40";
            mikuni16ATroubleCodes["4080"] = "40";

            mikuni16CTroubleCodes = new Dictionary<string, string>();
            mikuni16CTroubleCodes["0040"] = "13";
            mikuni16CTroubleCodes["0080"] = "13";
            mikuni16CTroubleCodes["0140"] = "44";
            mikuni16CTroubleCodes["0180"] = "44";
            mikuni16CTroubleCodes["0240"] = "14";
            mikuni16CTroubleCodes["0280"] = "14";
            mikuni16CTroubleCodes["0340"] = "98";
            mikuni16CTroubleCodes["0380"] = "98";
            mikuni16CTroubleCodes["0540"] = "99";
            mikuni16CTroubleCodes["0580"] = "99";
            mikuni16CTroubleCodes["0640"] = "15";
            mikuni16CTroubleCodes["0680"] = "15";
            mikuni16CTroubleCodes["0740"] = "21";
            mikuni16CTroubleCodes["0780"] = "21";
            mikuni16CTroubleCodes["0840"] = "23";
            mikuni16CTroubleCodes["0880"] = "23";
            mikuni16CTroubleCodes["0940"] = "12";
            mikuni16CTroubleCodes["0980"] = "12";
            mikuni16CTroubleCodes["2040"] = "32";
            mikuni16CTroubleCodes["2080"] = "32";
            mikuni16CTroubleCodes["2140"] = "24";
            mikuni16CTroubleCodes["2180"] = "24";
            mikuni16CTroubleCodes["2240"] = "67";
            mikuni16CTroubleCodes["2280"] = "67";
            mikuni16CTroubleCodes["2340"] = "66";
            mikuni16CTroubleCodes["2380"] = "66";
            mikuni16CTroubleCodes["2440"] = "49";
            mikuni16CTroubleCodes["2480"] = "49";

            gw250TroubleCodes = new Dictionary<string, string>();
            gw250TroubleCodes["P0105"] = "C17";
            gw250TroubleCodes["P0110"] = "C21";
            gw250TroubleCodes["P0115"] = "C15";
            gw250TroubleCodes["P0120"] = "C14";
            gw250TroubleCodes["P0130"] = "C44";
            gw250TroubleCodes["P0135"] = "C44";
            gw250TroubleCodes["P1750"] = "C13";
            gw250TroubleCodes["P0201"] = "C32";
            gw250TroubleCodes["P0202"] = "C33";
            gw250TroubleCodes["P0335"] = "C12";
            gw250TroubleCodes["P0351"] = "C24";
            gw250TroubleCodes["P0352"] = "C25";
            gw250TroubleCodes["P0230"] = "C41";
            gw250TroubleCodes["P0480"] = "C60";
            gw250TroubleCodes["P0505"] = "C40";
            gw250TroubleCodes["P0506"] = "C65";
            gw250TroubleCodes["P0507"] = "C65";
            gw250TroubleCodes["P0705"] = "C31";
            gw250TroubleCodes["P1650"] = "C42";
            gw250TroubleCodes["P1651"] = "C23";
            gw250TroubleCodes["P1656"] = "C49";
            gw250TroubleCodes["P0443"] = "C62";
            gw250TroubleCodes["P2505"] = "C41";
        }

        public static JM.Diag.ICommbox Commbox
        {
            get { return JM.Diag.BoxFactory.Instance.Commbox; }
        }

        public static Core.LiveDataVector LiveDataVector
        {
            get { return liveDataVector; }
            set { liveDataVector = value; }
        }

        public static void ChangeTroubleCodeFor16A(List<TroubleCode> tcs)
        {
            foreach (var v in tcs)
            {
                v.Code = mikuni16ATroubleCodes[v.Code];
            }
        }

        public static void ChangeTroubleCodeForNone16A(List<TroubleCode> tcs)
        {
            foreach (var v in tcs)
            {
                v.Code = mikuni16CTroubleCodes[v.Code];
            }
        }

        public static void ChangeTroubleCodeForGW250(List<TroubleCode> tcs)
        {
            foreach (var v in tcs)
            {
                v.Code = gw250TroubleCodes[v.Code];
            }
        }
    }
}

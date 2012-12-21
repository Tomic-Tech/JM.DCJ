using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using JM.Core;
using JM.Vehicles;
using JM.DCJ.Vehicle;

namespace JM.DCJ.WinUI
{
    class HJ125T_16APresenter : MikuniPresenter
    {
        public HJ125T_16APresenter()
            : base()
        {

        }

        protected override void MenuItemInitialize()
        {
            DlgPresenter.ShowStatus(Resources.COMMUNICATING);
            Mikuni.ChineseVersion version = new Mikuni.ChineseVersion();
            
            Task.Factory.StartNew(() => 
            {
                Mikuni protocol = new Mikuni(Resources.Commbox, options);
                string temp = protocol.GetECUVersion();
                version = Mikuni.FormatECUVersionForChina(temp);
            }).ContinueWith((t) => 
            {
                if (t.IsFaulted)
                {
                    DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    Back();
                }
                else
                {
                    DlgPresenter.Hide();
                    Items = new ObservableCollection<MenuItem>();
                    Items.Add(new MenuItem(Resources.READ_CURRENT_TROUBLE_CODE,
                        READ_CURRENT_TROUBLE_CODE_ICON,
                        READ_CURRENT_TROUBLE_CODE_ICON_S));
                    Items.Add(new MenuItem(Resources.READ_HISTORY_TROUBLE_CODE,
                        READ_HISTORY_TROUBLE_CODE_ICON,
                        READ_HISTORY_TROUBLE_CODE_ICON_S));
                    Items.Add(new MenuItem(Resources.READ_DATA_STREAM,
                        READ_DATA_STREAM_ICON,
                        READ_DATA_STREAM_ICON_S));
                    Items.Add(new MenuItem(Resources.READ_ECU_VERSION,
                        READ_ECU_VERSION_ICON,
                        READ_ECU_VERSION_ICON_S));
                    Items.Add(new MenuItem(Resources.TPS_IDLE_SETTING,
                        TPS_IDLE_SETTING_ICON,
                        TPS_IDLE_SETTING_ICON_S));
                    if (version.Hardware.Equals("ECU200-A001D") ||
                        version.Hardware.Equals("ECU200-A002") ||
                        version.Software.Equals("ECU200-A003"))
                    {
                        Items.Add(new MenuItem(Resources.LONG_TERM_LEARN_VALUE_ZONE_INITIALIZATION,
                            LONG_TERM_LEARNING_VALUE_INITIALIZATION_ICON,
                            LONG_TERM_LEARNING_VALUE_INITIALIZATION_ICON_S));
                        Items.Add(new MenuItem(Resources.ISC_LEARN_VALUE_INITIALIZATION,
                            ISC_LEARN_VALUE_INITIALIZATION_ICON,
                            ISC_LEARN_VALUE_INITIALIZATION_ICON_S));
                    }
                }
            });
        }
    }
}

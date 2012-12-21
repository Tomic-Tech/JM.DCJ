using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace JM.DCJ.WinUI
{
    class MenuPresenter : JM.Core.Notifier
    {
        public delegate void ProtocolFunc();
        private Dictionary<string, ProtocolFunc> functionSelected;
        private ObservableCollection<MenuItem> items = null;
        private MenuPresenter preview = null;
        private UserControl view = null;

        public const string READ_CURRENT_TROUBLE_CODE_ICON = "pack://application:,,,/Icons/Read Current Trouble Code.png";
        public const string READ_CURRENT_TROUBLE_CODE_ICON_S = "pack://application:,,,/Icons/Read Current Trouble Code.png";
        public const string READ_HISTORY_TROUBLE_CODE_ICON = "pack://application:,,,/Icons/Read History Trouble Code.png";
        public const string READ_HISTORY_TROUBLE_CODE_ICON_S = "pack://application:,,,/Icons/Read History Trouble Code.png";
        public const string READ_DATA_STREAM_ICON = "pack://application:,,,/Icons/Read Data Stream.png";
        public const string READ_DATA_STREAM_ICON_S = "pack://application:,,,/Icons/Read Data Stream.png";
        public const string READ_ECU_VERSION_ICON = "pack://application:,,,/Icons/Read ECU Version.png";
        public const string READ_ECU_VERSION_ICON_S = "pack://application:,,,/Icons/Read ECU Version.png";
        public const string TPS_IDLE_SETTING_ICON = "pack://application:,,,/Icons/TPS Idle Setting.png";
        public const string TPS_IDLE_SETTING_ICON_S = "pack://application:,,,/Icons/TPS Idle Setting.png";
        public const string LONG_TERM_LEARNING_VALUE_INITIALIZATION_ICON = "pack://application:,,,/Icons/Long Term Learning Value Initialize.png";
        public const string LONG_TERM_LEARNING_VALUE_INITIALIZATION_ICON_S = "pack://application:,,,/Icons/Long Term Learning Value Initialize.png";
        public const string ISC_LEARN_VALUE_INITIALIZATION_ICON = "pack://application:,,,/Icons/Idle Control Learning Value Initialize.png";
        public const string ISC_LEARN_VALUE_INITIALIZATION_ICON_S = "pack://application:,,,/Icons/Idle Control Learning Value Initialize.png";
        public MenuPresenter()
        {
        }

        public ObservableCollection<MenuItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public MenuPresenter Preview
        {
            get { return preview; }
            set
            {
                preview = value;
            }
        }

        public List<MenuItem> ItemList
        {
            set { items = new ObservableCollection<MenuItem>(value); }
        }

        public Dictionary<string, ProtocolFunc> FunctionSelected
        {
            get { return functionSelected; }
            set { functionSelected = value; }
        }

        public UserControl View
        {
            get { return view; }
            set { view = value; }
        }

        public virtual void Enter(MenuItem item)
        {
            if (FunctionSelected != null &&
                FunctionSelected.ContainsKey(item.Text))
                FunctionSelected[item.Text]();
        }

        public void Back()
        {
            View.Dispatcher.BeginInvoke((Action)(() => 
            {
                if (Preview != null)
                    View.DataContext = Preview;
            }));
        }

        protected DialogPresenter DlgPresenter
        {
            get { return AppPresenter.Inst.DlgPresenter; }
        }

        protected TroubleCodePresenter TCPresenter
        {
            get { return AppPresenter.Inst.TCPresenter; }
        }

        protected DataStreamPresenter DSPresenter
        {
            get { return AppPresenter.Inst.DSPresenter; }
        }
    }
}

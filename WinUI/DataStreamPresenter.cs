using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace JM.DCJ.WinUI
{
    class DataStreamPresenter : Core.Notifier
    {
        public delegate void NoArgDelegate();

        private ObservableCollection<Core.LiveData> items = null;
        private Diag.AbstractECU protocol = null;
        private Task task = null;

        public ObservableCollection<Core.LiveData> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public Diag.AbstractECU Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }

        public Task ProtocolTask
        {
            get { return task; }
            set { task = value; }
        }

        public NoArgDelegate BackHandler;
        public NoArgDelegate ShowHandler;

        public void Back()
        {
            AppPresenter.Inst.DlgPresenter.ShowStatus(Vehicle.Resources.COMMUNICATING);
            if (Protocol != null)
                Protocol.StopReadDataStream();
            if (ProtocolTask != null)
                ProtocolTask.Wait();
            if (BackHandler != null)
                BackHandler();
            AppPresenter.Inst.DlgPresenter.Hide();
        }

        public void Show()
        {
            if (ShowHandler != null)
                ShowHandler();
        }

        public string HeaderShortName
        {
            get
            {
                return JM.Core.Database.GetText("ShortName", "System");
            }
        }

        public string HeaderContent
        {
            get
            {
                return JM.Core.Database.GetText("Content", "System");
            }
        }

        public string HeaderValue
        {
            get
            {
                return JM.Core.Database.GetText("Value", "System");
            }
        }

        public string HeaderUnit
        {
            get
            {
                return JM.Core.Database.GetText("Unit", "System");
            }
        }
    }
}

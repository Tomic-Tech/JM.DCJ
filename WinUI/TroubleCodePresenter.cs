using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JM.Core;

namespace JM.DCJ.WinUI
{
    class TroubleCodePresenter : Core.Notifier
    {
        public delegate void NoArgDelegate();
        private ObservableCollection<TroubleCode> troubleCodes;

        public TroubleCodePresenter()
        {

        }

        public ObservableCollection<TroubleCode> TroubleCodes
        {
            get { return troubleCodes; }
            set
            {
                troubleCodes = value;
                OnPropertyChanged("TroubleCodes");
            }
        }

        public List<TroubleCode> TroubleCodeList
        {
            set 
            { 
                troubleCodes = new ObservableCollection<TroubleCode>(value);
                OnPropertyChanged("TroubleCodes");
            }
        }

        public string HeaderCode
        {
            get { return Core.Database.GetText("Code", "System"); }
        }

        public string HeaderContent
        {
            get { return Core.Database.GetText("Content", "System"); }
        }

        public NoArgDelegate BackHandler;
        public NoArgDelegate ShowHandler;

        public void Back()
        {
            if (BackHandler != null)
                BackHandler();
        }

        public void Show()
        {
            if (ShowHandler != null)
                ShowHandler();
        }
    }
}

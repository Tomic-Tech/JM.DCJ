using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JM.DCJ.WinUI
{
    class AppPresenter : Core.Notifier
    {
        static private AppPresenter inst;
        private DialogPresenter dlgPresenter;
        private TroubleCodePresenter tcPresenter;
        private DataStreamPresenter dsPresenter;

        static AppPresenter()
        {
            inst = new AppPresenter();
        }

        private AppPresenter()
        {
        }

        public static AppPresenter Inst
        {
            get { return inst; }
        }

        public DialogPresenter DlgPresenter
        {
            get
            {
                if (dlgPresenter == null)
                    dlgPresenter = new DialogPresenter();
                return dlgPresenter;
            }
        }

        public TroubleCodePresenter TCPresenter
        {
            get
            {
                if (tcPresenter == null)
                    tcPresenter = new TroubleCodePresenter();
                return tcPresenter;
            }
        }

        public DataStreamPresenter DSPresenter
        {
            get
            {
                if (dsPresenter == null)
                    dsPresenter = new DataStreamPresenter();

                return dsPresenter;
            }
        }
    }
}

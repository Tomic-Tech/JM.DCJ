using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JM.DCJ.Vehicle;
using JM.Core;
using JM.Vehicles;

namespace JM.DCJ.WinUI
{
    class GW250Presenter : MenuPresenter
    {
        public GW250Presenter()
        {
            Items = new ObservableCollection<MenuItem>();
            Items.Add(new MenuItem(Core.Database.GetText("Read Current Trouble Code", "System"),
                "pack://application:,,,/Icons/Read Current Trouble Code.png",
                "pack://application:,,,/Icons/Read Current Trouble Code.png"));
            Items.Add(new MenuItem(Core.Database.GetText("Read History Trouble Code", "System"),
                "pack://application:,,,/Icons/Read History Trouble Code.png",
                "pack://application:,,,/Icons/Read History Trouble Code.png"));
            Items.Add(new MenuItem(Core.Database.GetText("Read Data Stream", "System"),
                "pack://application:,,,/Icons/Read Data Stream.png",
                "pack://application:,,,/Icons/Read Data Stream.png"));
            Items.Add(new MenuItem(Core.Database.GetText("Read ECU Version", "System"),
                "pack://application:,,,/Icons/Read ECU Version.png",
                "pack://application:,,,/Icons/Read ECU Version.png"));

            FunctionSelected = new Dictionary<string, ProtocolFunc>();
            FunctionSelected[Resources.READ_CURRENT_TROUBLE_CODE] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                List<TroubleCode> codes = null;

                Task.Factory.StartNew(() => 
                {
                    GW250 protocol = new GW250(Resources.Commbox);
                    codes = protocol.ReadTroubleCode(false);
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        Resources.ChangeTroubleCodeForGW250(codes);
                        TCPresenter.TroubleCodeList = codes;
                        TCPresenter.Show();
                        DlgPresenter.Hide();
                    }
                });
            };

            FunctionSelected[Resources.READ_HISTORY_TROUBLE_CODE] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                List<TroubleCode> codes = null;

                Task.Factory.StartNew(() => 
                {
                    GW250 protocol = new GW250(Resources.Commbox);
                    codes = protocol.ReadTroubleCode(true);
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        Resources.ChangeTroubleCodeForGW250(codes);
                        TCPresenter.TroubleCodeList = codes;
                        TCPresenter.Show();
                        DlgPresenter.Hide();
                    }
                });
            };

            FunctionSelected[Resources.READ_DATA_STREAM] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                Resources.LiveDataVector = Database.GetLiveData("GW250");
                DSPresenter.Items = Resources.LiveDataVector.Items;
                GW250 protocol = null;

                DSPresenter.ProtocolTask = Task.Factory.StartNew(() => 
                {
                    protocol = new GW250(Resources.Commbox);
                    DSPresenter.Protocol = protocol;
                    protocol.ReadDataStream(Resources.LiveDataVector);
                });

                DSPresenter.Show();
                DlgPresenter.Hide();

                DSPresenter.ProtocolTask.ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        if (DSPresenter.Protocol != null)
                        {
                            DSPresenter.Protocol.StopReadDataStream();
                            DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, (sender, e) => 
                            {
                                DSPresenter.Back();
                            });
                        }
                    }
                });
            };

        }
    }
}

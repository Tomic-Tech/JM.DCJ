using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;

namespace JM.DCJ.WinUI
{
    class MainMenuPresenter : MenuPresenter
    {
        public MainMenuPresenter()
        {
            Items = new ObservableCollection<MenuItem>();
            Items.Add(new MenuItem(Core.Database.GetText("HJ125T-16A", "DCJ"),
                "pack://application:,,,/Icons/Vehicle.png",
                "pack://application:,,,/Icons/VehicleSelected.png"));
            Items.Add(new MenuItem(Core.Database.GetText("HJ125T-16C", "DCJ"),
                "pack://application:,,,/Icons/Vehicle.png",
                "pack://application:,,,/Icons/VehicleSelected.png"));
            Items.Add(new MenuItem(Core.Database.GetText("HJ125T-10", "DCJ"),
                "pack://application:,,,/Icons/Vehicle.png",
                "pack://application:,,,/Icons/VehicleSelected.png"));
            Items.Add(new MenuItem(Core.Database.GetText("GW250", "DCJ"),
                "pack://application:,,,/Icons/Vehicle.png",
                "pack://application:,,,/Icons/VehicleSelected.png"));

            FunctionSelected = new Dictionary<string, MenuPresenter.ProtocolFunc>();
            FunctionSelected[Core.Database.GetText("HJ125T-16A", "DCJ")] = () =>
            {
                Execute(() =>
                {
                    ChangePage(new HJ125T_16APresenter());
                });
            };

            FunctionSelected[Core.Database.GetText("HJ125T-16C", "DCJ")] = () =>
            {
                Execute(() =>
                {
                    ChangePage(new HJ125T_16CPresenter());
                });
            };

            FunctionSelected[Core.Database.GetText("HJ125T-10", "DCJ")] = () =>
            {
                Execute(() =>
                {
                    ChangePage(new HJ125T_10Presenter());
                });
            };

            FunctionSelected[Core.Database.GetText("GW250", "DCJ")] = () =>
            {
                Execute(() =>
                {
                    ChangePage(new GW250Presenter());
                });
            };
        }

        private void ChangePage(MenuPresenter current)
        {
            try
            {
                MenuPresenter presenter = View.DataContext as MenuPresenter;
                current.View = View;
                current.Preview = presenter;
                View.DataContext = current;
            }
            catch (Exception ex)
            {
                AppPresenter.Inst.DlgPresenter.ShowFatalBox(ex.Message, null);
                MenuPresenter presenter = (View.DataContext as MenuPresenter).Preview;
                if (presenter != null)
                {
                    View.DataContext = presenter;
                }
            }
        }

        private void Execute(Action act)
        {
            AppPresenter.Inst.DlgPresenter.ShowStatus(Core.Database.GetText("OpenCommbox", "System"));
            Task task = Task.Factory.StartNew(() =>
            {
                if (!Vehicle.Resources.Commbox.Close() ||
                    !Vehicle.Resources.Commbox.Open())
                {
                    throw new IOException(Core.Database.GetText("Open Commbox Fail", "System"));
                }
            });

            task.ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    AppPresenter.Inst.DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                }
                else
                {
                    AppPresenter.Inst.DlgPresenter.Hide();
                    View.Dispatcher.BeginInvoke(act);
                }
            });
        }
    }
}

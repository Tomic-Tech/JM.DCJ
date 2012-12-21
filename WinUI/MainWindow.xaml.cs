using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;

namespace JM.DCJ.WinUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dialog dialog = null;
        private MenuView menuView = null;
        private TroubleCodeView troubleCodeView = null;
        private DataStreamView dataStreamView = null;

        public MainWindow()
        {
            InitializeComponent();
            JM.Core.MustCallFirst.Instance.Init(System.Environment.CurrentDirectory + "/");

            InitializeDialog();
            InitializeVehicleModels();
            InitializeTroubleCode();
            InitializeDataStream();

            Content = menuView;

            Diag.BoxFactory.Instance.Version = Diag.BoxVersion.C168;
            Diag.BoxFactory.Instance.StreamType = Diag.StreamType.SerialPort;

            this.Loaded += (sender, e) => 
            {
                dialog.Owner = this;
            };

            Title = Core.Database.GetText("DCJ Diagnostic System", "DCJ");
        }

        private void InitializeDialog()
        {
            dialog = new Dialog();
            dialog.DataContext = AppPresenter.Inst.DlgPresenter;
            AppPresenter.Inst.DlgPresenter.ShowHandler = dialog.ShowBox;
            AppPresenter.Inst.DlgPresenter.HideHandler = () => 
            {
                Dispatcher.BeginInvoke((Action)(() => 
                {
                    dialog.Hide(); 
                }));
                
            };
            AppPresenter.Inst.DlgPresenter.ResetHandler = dialog.Reset;
            AppPresenter.Inst.DlgPresenter.AddButtonHandler = dialog.AddButton;
        }

        private void InitializeVehicleModels()
        {
            menuView = new MenuView();
            MenuPresenter p = new MainMenuPresenter();
            p.View = menuView;
            menuView.DataContext = p;
        }

        private void InitializeTroubleCode()
        {
            troubleCodeView = new TroubleCodeView();
            troubleCodeView.DataContext = AppPresenter.Inst.TCPresenter;
            AppPresenter.Inst.TCPresenter.BackHandler = () =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    Content = menuView;
                }));
            };

            AppPresenter.Inst.TCPresenter.ShowHandler = () =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    Content = troubleCodeView;
                }));
            };
        }

        private void InitializeDataStream()
        {
            dataStreamView = new DataStreamView();
            dataStreamView.DataContext = AppPresenter.Inst.DSPresenter;
            AppPresenter.Inst.DSPresenter.BackHandler = () =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    Content = menuView;
                }));
            };

            AppPresenter.Inst.DSPresenter.ShowHandler = () =>
            {
                Dispatcher.BeginInvoke((Action)(() => 
                {
                    Content = dataStreamView;
                }));
            };
        }
    }
}

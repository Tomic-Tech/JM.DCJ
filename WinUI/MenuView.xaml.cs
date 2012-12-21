using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace JM.DCJ.WinUI
{
    /// <summary>
    /// VehicleModels.xaml 的交互逻辑
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Content = Core.Database.GetText("Back", "System");
            btn.Click += (sender, e) =>
            {
                MenuPresenter presenter = DataContext as MenuPresenter;
                if (presenter == null)
                    return;
                presenter.Back();
            };
            toolBar.Items.Add(btn);
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = listView.SelectedItem as MenuItem;
            if (item == null)
                return;
            MenuPresenter presenter = DataContext as MenuPresenter;
            if (presenter == null)
                return;
            presenter.Enter(item);
        }
    }
}

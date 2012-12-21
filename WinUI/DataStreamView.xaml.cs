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

namespace JM.DCJ.WinUI
{
    /// <summary>
    /// DataStreamView.xaml 的交互逻辑
    /// </summary>
    public partial class DataStreamView : UserControl
    {
        public DataStreamView()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Content = Core.Database.GetText("Back", "System");
            btn.Click += (sender, e) => 
            {
                DataStreamPresenter presenter = DataContext as DataStreamPresenter;
                if (presenter == null)
                    return;
                presenter.Back();
            };
            toolBar.Items.Add(btn);
        }
    }
}

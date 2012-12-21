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
using System.Windows.Shapes;

namespace JM.DCJ.WinUI
{
    /// <summary>
    /// Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class Dialog : Window
    {
        private MessageBox msgBox;

        public Dialog()
        {
            InitializeComponent();
            InitializeMessageBox();
        }

        private void InitializeMessageBox()
        {
            msgBox = new MessageBox();
        }

        public void ShowBox()
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                switch (AppPresenter.Inst.DlgPresenter.Type)
                {
                    case DialogPresenter.ContentType.MsgBox:
                        dockPanel.Children.Add(msgBox);
                        break;
                    default:
                        break;
                }
                ShowDialog();
            }));
        }

        public void Reset()
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Hide();
                buttonPanel.Children.RemoveRange(0, buttonPanel.Children.Count);
                dockPanel.Children.RemoveRange(0, dockPanel.Children.Count);
            }));
        }

        public void AddButton(string text, RoutedEventHandler handler)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Button btn = new Button();
                btn.Content = text;
                if (handler != null)
                    btn.Click += handler;
                btn.Click += (sender, e) =>
                {
                    Hide();
                };
                btn.Height = 30;
                buttonPanel.Children.Add(btn);

            }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace JM.DCJ.WinUI
{
    class HJ125T_16CPresenter : MenuPresenter
    {
        public HJ125T_16CPresenter()
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
            Items.Add(new MenuItem(Core.Database.GetText("TPS Idle Setting", "System"),
                "pack://application:,,,/Icons/TPS Idle Setting.png",
                "pack://application:,,,/Icons/TPS Idle Setting.png"));
            Items.Add(new MenuItem(Core.Database.GetText("Long Term Learning Value Initialize", "System"),
                "pack://application:,,,/Icons/Long Term Learning Value Initialize.png",
                "pack://application:,,,/Icons/Long Term Learning Value Initialize.png"));
            Items.Add(new MenuItem(Core.Database.GetText("Idle Control Learning Value Initialize", "System"),
                "pack://application:,,,/Icons/Idle Control Learning Value Initialize.png",
                "pack://application:,,,/Icons/Idle Control Learning Value Initialize.png"));

        }
    }
}

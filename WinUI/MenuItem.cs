using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JM.DCJ.WinUI
{
    class MenuItem : JM.Core.Notifier
    {
        private string text;
        private string icon;
        private string iconSelected;

        public MenuItem(string text, string icon, string iconSelected)
        {
            this.text = text;
            this.icon = icon;
            this.iconSelected = iconSelected;
        }

        public string Text
        {
            get { return text; }
            set 
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public string IconSelected
        {
            get { return iconSelected; }
            set
            {
                iconSelected = value;
                OnPropertyChanged("IconSelected");
            }
        }
    }
}

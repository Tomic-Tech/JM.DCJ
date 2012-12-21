using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace JM.DCJ.WinUI
{
    class DialogPresenter : Core.Notifier
    {
        public enum ContentType
        {
            MsgBox,
            ListBox
        }

        public delegate void NoArgDelegate();
        public delegate void StringArgDelegate(string text, RoutedEventHandler handler);

        private ContentType type = ContentType.MsgBox;
        private string msg;


        public DialogPresenter()
        {
        }

        public ContentType Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Message
        {
            get { return msg; }
            set
            {
                msg = value;
                OnPropertyChanged("Message");
            }
        }

        public NoArgDelegate ShowHandler;
        public NoArgDelegate HideHandler;
        public NoArgDelegate ResetHandler;
        public StringArgDelegate AddButtonHandler;

        public void Reset()
        {
            if (ResetHandler != null)
                ResetHandler();
        }

        public void AddButton(string text, RoutedEventHandler handler)
        {
            if (AddButtonHandler != null)
                AddButtonHandler(text, handler);
        }

        public void Show()
        {
            if (ShowHandler != null)
                ShowHandler();
        }

        public void Hide()
        {
            if (HideHandler != null)
                HideHandler();
        }

        public void ShowFatalBox(string text, RoutedEventHandler handler)
        {
            Reset();
            Type = ContentType.MsgBox;
            Message = text;
            AddButtonHandler(Core.Database.GetText("OK", "System"), handler);
            Show();
        }

        public void ShowStatus(string text)
        {
            Reset();
            Type = ContentType.MsgBox;
            Message = text;
            Show();
        }
    }
}

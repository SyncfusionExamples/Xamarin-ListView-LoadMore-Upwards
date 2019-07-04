using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplateSelector
{
    public class Message : INotifyPropertyChanged
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(); }
        }

        private string messageDateTime;

        public string MessagDateTime
        {
            get { return messageDateTime; }
            set { messageDateTime = value; RaisePropertyChanged(); }
        }

        private bool isIncoming;

        public bool IsIncoming
        {
            get { return isIncoming; }
            set { isIncoming = value; RaisePropertyChanged(); }
        }

        private void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

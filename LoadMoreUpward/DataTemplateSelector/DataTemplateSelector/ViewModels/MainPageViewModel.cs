using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DataTemplateSelector
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region Fields
        public SfListView ListView;
        private ObservableCollection<Message> messagesList;
        public Command<object> LoadCommand { get; set; }
        public bool indicatorIsVisible;
        public bool gridIsVisible;
        private string newText;
        private ImageSource sendIcon;
        private string outgoingText;

        public string[] MessageText = new string[]
        {
            "Hi Squirrel! \uD83D\uDE0A",
            "Hi Baboon, How are you? \uD83D\uDE0A",
            "We've a party at Mandrill's. Would you like to join? We would love to have you there! \uD83D\uDE01",
            "You will love it. Don't miss.",
            "Sounds like a plan. \uD83D\uDE0E",
            "\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49"
        };

        #endregion

        #region properties

        public ObservableCollection<Message> Messages
        {
            get { return messagesList; }
            set { messagesList = value; }
        }

        public bool IndicatorIsVisible
        {
            get { return indicatorIsVisible; }
            set
            {
                indicatorIsVisible = value;
                OnPropertyChanged("IndicatorIsVisible");
            }
        }

        public bool GridIsVisible
        {
            get { return gridIsVisible; }
            set
            {
                gridIsVisible = value;
                OnPropertyChanged("GridIsVisible");
            }
        }

        public string NewText
        {
            get { return newText; }
            set
            {
                newText = value;
                OnPropertyChanged("NewText");
            }
        }
        
        public ImageSource SendIcon
        {
            get
            { return sendIcon; }
            set
            { sendIcon = value; }
        }

        public Command<object> SendCommand { get; set; }

        public string OutGoingText
        {
            get { return outgoingText; }
            set { outgoingText = value; }
        }

        #endregion

        #region Interface Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            SendCommand =new Command<object>(InitializeSendCommand);
            LoadCommand = new Command<object>(OnLoadingAsync);
            // Initialize with default values
            var r = new Random();
            Messages = new ObservableCollection<Message>();
            for (int i = 0; i < 5; i++)
            {
                var collection = new Message();
                collection.Text = MessageText[r.Next(0, MessageText.Count() - 1)];
                collection.IsIncoming = i % 2 == 0 ? true : false;
                collection.MessagDateTime = DateTime.Now.ToString();
                Messages.Add(collection);
            }
            GridIsVisible = true;
        }

        #endregion
        #region Private methods
        private async void OnLoadingAsync(object obj)
        {
            ListView = obj as SfListView;
            var firstItem = ListView.DataSource.DisplayItems[0];
            this.GridIsVisible = false;
            this.IndicatorIsVisible = true;
            var r = new Random();
            ListView.DataSource.BeginInit();
            for (int i = 0; i < 5; i++)
            {
                var collection = new Message();
                collection.Text = this.MessageText[r.Next(0, this.MessageText.Count() - 1)];
                collection.IsIncoming = i % 2 == 0 ? true : false;
                collection.MessagDateTime = DateTime.Now.ToString();
                this.Messages.Insert(0, collection);
            }
            ListView.DataSource.EndInit();
            await Task.Delay(500);
            var firstItemIndex = ListView.DataSource.DisplayItems.IndexOf(firstItem);
            var header = (ListView.HeaderTemplate != null && !ListView.IsStickyHeader) ? 1 : 0;
            var totalItems = firstItemIndex + header;
            ListView.LayoutManager.ScrollToRowIndex(totalItems, true);
            this.GridIsVisible = true;
            this.IndicatorIsVisible = false;
        }

        private void InitializeSendCommand(object obj)
        {
            SfListView listView = obj as SfListView;
            if (!string.IsNullOrWhiteSpace(NewText))
            {
                Messages.Add(new Message
                {
                    Text = NewText,
                    IsIncoming = true,
                    MessagDateTime = DateTime.Now.ToString()
                });
                (listView.LayoutManager as LinearLayout).ScrollToRowIndex(Messages.Count - 1, true);
            }
            NewText = null;
        }

        #endregion

    }
}

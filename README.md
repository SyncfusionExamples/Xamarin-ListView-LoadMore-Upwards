# How to load more items in upward direction manually using bottom click

SfListView allows you to load more items in upward direction manually using a button clicked event. Show the busy indicator until the items are added into the 
collection as like below code example.

```
<sync:SfListView x:Name="ListView"  IsBusy="True"
            ItemTemplate="{StaticResource MessageTemplateSelector}" 
            ItemsSource="{Binding Messages}"
            ItemSize="100">
            <sync:SfListView.HeaderTemplate>
                <DataTemplate>
                    <ViewCell>
                        <Grid>
                            <Grid BackgroundColor="#d3d3d3" IsVisible="{Binding GridIsVisible}">
                                <Button Text="Load More" Clicked="Button_Clicked" HorizontalOptions="CenterAndExpand" VerticalOptions="CenterAndExpand"/>
                            </Grid>
                            <sync:LoadMoreIndicator Color="Red" IsRunning="True" IsVisible="{Binding IndicatorIsVisible}"/>
                        </Grid>
                    </ViewCell>
                </DataTemplate>
            </sync:SfListView.HeaderTemplate>
</sync:SfListView>
```
Insert each new item in the zeroth position of the underlying collection which is bound to the SfListView.ItemsSource property. When the collection gets modified,the ScrollViewer scrolls to the top of the list. Using ScrollToRowIndex method scroll back to the previous item index as like below.

```
public partial class MainPage : ContentPage
{
        MainPageViewModel ViewModel;
        VisualContainer visualContainer;
  
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainPageViewModel();
            BindingContext = ViewModel;
            ViewModel.ListView = this.ListView;
            ListView.Loaded += ListView_Loaded;
            visualContainer = ListView.GetVisualContainer();
        }
  
        private void ListView_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            (ListView.LayoutManager as LinearLayout).ScrollToRowIndex(ViewModel.Messages.Count - 1, true);
        }
  
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //To get the current first item which is visible in the View.
            var firstItem = ListView.DataSource.DisplayItems[0];
            ViewModel.GridIsVisible = false;
            ViewModel.IndicatorIsVisible = true;
            await Task.Delay(2000);
            var r = new Random();
  
            //To avoid layout calls for arranging each and every items to be added in the View. 
            ListView.DataSource.BeginInit();
            for (int i = 0; i < 5; i++)
            {
                var collection = new Message();
                collection.Text = ViewModel.MessageText[r.Next(0, ViewModel.MessageText.Count() - 1)];
                collection.IsIncoming = i % 2 == 0 ? true : false;
                collection.MessagDateTime = DateTime.Now.ToString();
                ViewModel.Messages.Insert(0, collection);
            }
            ListView.DataSource.EndInit();
            var firstItemIndex = ListView.DataSource.DisplayItems.IndexOf(firstItem);
            var header = (ListView.HeaderTemplate != null && !ListView.IsStickyHeader) ? 1 : 0;
            var totalItems = firstItemIndex + header;            //Need to scroll back to previous position else the ScrollViewer moves to top of the list.
            ListView.LayoutManager.ScrollToRowIndex(totalItems, true);
            ViewModel.GridIsVisible = true;
            ViewModel.IndicatorIsVisible = false;
        }
}
```
To know more about LoadMore in SfListView, please refer our documentation [here](https://help.syncfusion.com/xamarin/sflistview/loadmore)


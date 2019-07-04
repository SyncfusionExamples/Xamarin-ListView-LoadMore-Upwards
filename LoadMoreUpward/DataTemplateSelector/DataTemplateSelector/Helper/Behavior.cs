using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataTemplateSelector
{
   public class Behavior : Behavior<ContentPage>
    {

        #region Fields
        SfListView ListView;
        VisualContainer visualContainer;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("ListView");
            ListView.Loaded += ListView_Loaded;
            visualContainer = ListView.GetVisualContainer();

            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(ContentPage bindable)
        {

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region CallBacks
        private void ListView_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            (ListView.LayoutManager as LinearLayout).ScrollToRowIndex(ListView.DataSource.DisplayItems.Count()-1, true);
        }
        
        #endregion

    }
}

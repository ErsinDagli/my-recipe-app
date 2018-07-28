using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.ObjectModel;

namespace Recipe_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Categories : ContentPage
	{
        public static string SelectedCategory;
        public static IList<SQLentry> categoryListEntries;
        public static ObservableCollection<SQLentry> CategoryOC;


        public Categories (string selectedcategory)
		{
			InitializeComponent ();
            BindingContext = new SQLentry();
            SelectedCategory = selectedcategory;


            //App.categoryListEntries = App.Database.GetCategory(selectedcategory);
            CategoryOC = new ObservableCollection<SQLentry>();
            categoryListEntries = App.Database.GetCategory(selectedcategory);

            foreach (var item in categoryListEntries)
            {
                if (!CategoryOC.Contains(item))
                {
                    CategoryOC.Add(item);
                }
            }
            //item source of list is the OC where only categories are the same as the selected category provided in constructor
            //categoriesList.ItemsSource =  CategoryOC.Where(x => x.Category == SelectedCategory);
            categoriesList.ItemsSource = CategoryOC;
           // categoriesList.ItemsSource = App.Database.GetCategory(selectedcategory);
        }

        protected override void OnAppearing()
        {
            categoriesList.ItemsSource = CategoryOC;
        }

        private async Task categoriesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //a copy of the navigation technique in the recipessearchpage list

            SQLentry mychoice = e.SelectedItem as SQLentry;


            if (mychoice == null)
            {
                return;
            }
            else
            {
                //first line is making usre that after selecting listview item, and going back to this page, the selection doesnt remain checked
                ((ListView)sender).SelectedItem = null;
                await Navigation.PushAsync(new RecipeDisplayPage(mychoice));
            }
        }
    }
}
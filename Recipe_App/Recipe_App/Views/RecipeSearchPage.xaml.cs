using Recipe_App.ViewModels;
using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace Recipe_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeSearchPage : ContentPage
    {
        public Label txtRecipeName { get; set; }
        public static ObservableCollection<SQLentry> RecipeNameOC { get ; set; }
                                                                
        public static IList<SQLentry> recipeNameList;


      


        public RecipeSearchPage()
        {
            InitializeComponent();
            BindingContext = new SQLentry();

            //we check if theres duplicate values, if so we dont add it
            RecipeNameOC = new ObservableCollection<SQLentry>();
            var entries = App.Database.ListRecipes("");
            foreach (var item in entries)
            {
                if ((!RecipeNameOC.Contains(item)))
                {
                    RecipeNameOC.Add(item);
                }

            }


            // searchitemslistView.ItemsSource = App.Database.ListRecipes("");

            searchitemslistView.ItemsSource = RecipeNameOC;

           // searchitemslistView.ItemsSource = RecipeNameOC;
            //OnPropertyChanged(searchitemslist.SelectedItem.ToString());


        }

        protected override void OnAppearing()
        {
            searchitemslistView.ItemsSource = RecipeNameOC;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {


        }

        private void searchbar_SearchButtonPressed(object sender, EventArgs e)
        {
              
        }

        //search an item as a list in the database, we display all items in the list, the list updates as searched
        private void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {


            //searchitemslistView.ItemsSource = App.Database.ListRecipes(e.NewTextValue);

            //source of list is the RecipeNameOC but with text input of search
            //searchitemslistView.ItemsSource = RecipeSearchPage.RecipeNameOC.Where(x => x.RecipeName.Contains(e.NewTextValue)); 
            searchitemslistView.ItemsSource = App.Database.ListRecipes(e.NewTextValue);


        }


        //we select an item and get the item and explicitly convert the list item to a SQLentry type, pass it to the next page as a parameter

        private async void searchitemslist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           

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
                searchbar.Text = "";
                
            }
        }


        






}
}
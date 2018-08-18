using Recipe_App.ViewModels;
using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            
            searchitemslistView.ItemsSource = RecipeNameOC;
        }



        protected override void OnAppearing()
        {
            searchitemslistView.ItemsSource = RecipeNameOC;
        }


        


       

        //search an item as a list in the database, we display all items in the list, the list updates as searched
        private void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
           
           
            searchitemslistView.ItemsSource = App.Database.ListRecipes(e.NewTextValue.ToUpper());


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
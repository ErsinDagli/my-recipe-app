using Recipe_App.ViewModels;
using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        int page = 1;
      


        public RecipeSearchPage()
        {
            InitializeComponent();



            BindingContext = new SQLentry();





            //we check if theres duplicate values, if so we dont add it
            RecipeNameOC = new ObservableCollection<SQLentry>();
            Task.Run( async () => await Initialise());
               
          

            
            searchitemslistView.ItemsSource = RecipeNameOC;


            if(MainPage.TurkishClicked == false)
            {
                Title = Language.QuickSearchEN;
                searchbar.Placeholder = "Name";

            }
            else
            {
                Title = Language.QuickSearchTR;
                searchbar.Placeholder = "Tarif";
            }

        }

        async Task Initialise()
        {

            var entries = await App.Database.ListRecipes("", page);


            foreach (var item in entries)
            {
                if ((!RecipeNameOC.Contains(item)))
                {
                    if (string.IsNullOrWhiteSpace(item.ImageFilePath))
                        item.ImageFilePath = "recipeplaceholder.png";


                    RecipeNameOC.Add(item);
                }

            }
        }

        protected override void OnAppearing()
        {
            searchitemslistView.ItemsSource = RecipeNameOC;
            page = 1;

        }







        //search an item as a list in the database, we display all items in the list, the list updates as searched
        private async void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var recipes = await App.Database.ListRecipes(e.NewTextValue.ToUpper(), 1);

                foreach (var item in recipes)
                {
                    if (string.IsNullOrWhiteSpace(item.ImageFilePath))
                    {
                        item.ImageFilePath = "recipeplaceholder.pnhg";

                    }
                }


                searchitemslistView.ItemsSource = recipes;
            }
            catch(Exception f)
            {

            }
          


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

        private async void SearchitemslistView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                

                if (RecipeNameOC.Count < 10)
                    return;
                var recipeList = sender as ListView;
                var recipeListLast = RecipeNameOC.LastOrDefault();

                if (e.Item == null)
                {
                    return;
                }


                if (((SQLentry)e.Item).RecipeName == recipeListLast.RecipeName)
                {
                    page++;

                    loader.IsRunning = true;
                   loader.IsVisible = true;
                   var recipes = await App.Database.ListRecipes(searchbar.Text?.ToUpper() ?? "", page);

                    foreach(var rec in recipes)
                    {
                        if (string.IsNullOrWhiteSpace(rec.ImageFilePath))
                        {
                            rec.ImageFilePath = "recipeplaceholder.pnhg";

                        }
                        if(RecipeNameOC.Where(x=> x.RecipeName == rec.RecipeName).FirstOrDefault() == null)
                            RecipeNameOC.Add(rec);
                    }
                    searchitemslistView.ItemsSource = RecipeNameOC;


                 



                }
            }
            catch(Exception f)
            {

            }
            finally{
                loader.IsRunning = false;
                loader.IsVisible = false;
            }
        }
    }
}
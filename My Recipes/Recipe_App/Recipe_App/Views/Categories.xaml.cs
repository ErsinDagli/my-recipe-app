using Recipe_App.ViewModels;
using System.Collections.Generic;
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
           
            categoriesList.ItemsSource = CategoryOC;

            if(MainPage.TurkishClicked == false)
            {
                CategoryLabel.Text = Language.RecipeEnglish;
            }
            else
            {
                CategoryLabel.Text = Language.RecipeTurkish;
            }



            Title = SelectedCategory;

        }

       // protected override void OnAppearing()
       // {
           // categoriesList.ItemsSource = CategoryOC;
       // }

        private async void categoriesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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

        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            bool answer;
            if (MainPage.TurkishClicked == false)
            {
                answer = await DisplayAlert("Delete Buttons", "Are you sure?", "Yes", "No");
            }
            else
            {
                answer = await DisplayAlert("Tarifi Sil", "Emin Misiniz?", "Evet", "Hayır");
            }

            if (answer == true)
            {
                //delete category
                var cat = App.Database.GetCategoryByName(SelectedCategory);
                if (cat != null)
                {
                    App.Database.DeleteCategory(cat.Id);


                    await Navigation.PopAsync();
                }


            }
        }

     
    }
}
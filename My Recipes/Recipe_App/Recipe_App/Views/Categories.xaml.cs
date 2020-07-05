using Recipe_App.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;

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

            SetUpNavBar();

            SelectedCategory = selectedcategory;


          
            CategoryOC = new ObservableCollection<SQLentry>();
            categoryListEntries = App.Database.GetCategory(selectedcategory);

            foreach (var item in categoryListEntries)
            {
                if (!CategoryOC.Contains(item))
                {
                    if (string.IsNullOrWhiteSpace(item.ImageFilePath))
                        item.ImageFilePath = "recipeplaceholder.png";

                    CategoryOC.Add(item);
                }
            }
            //item source of list is the OC where only categories are the same as the selected category provided in constructor
           
            categoriesList.ItemsSource = CategoryOC;

            if(MainPage.TurkishClicked == false)
            {
                CategoryLabel.Text = SelectedCategory;
            }
            else
            {
                CategoryLabel.Text = SelectedCategory;
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

        //delete category event


        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            bool answer;
            if (MainPage.TurkishClicked == false)
            {
                answer = await DisplayAlert("Delete this category", "Are you sure?", "Yes", "No");
            }
            else
            {
                answer = await DisplayAlert("Kategoriyi sil", "Emin Misiniz?", "Evet", "Hayır");
            }

            if (answer == true)
            {
                //delete category
                var cat = App.Database.GetCategoryByName(SelectedCategory);
                if (cat != null)
                {
                    App.Database.DeleteCategory(cat.Id);

                    //send deleted event
                    MessagingCenter.Send<Categories, string>(this, "Deleted", SelectedCategory);


                    await Navigation.PopAsync();
                }


            }
        }


        void SetUpNavBar()
        {
            try
            {
                var frame = new Frame()
                {
                    CornerRadius = 10,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    IsClippedToBounds = true,
                    Padding = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.LightBlue

                };

                SearchBar sb = new SearchBar();
                sb.HorizontalOptions = LayoutOptions.FillAndExpand;
                sb.VerticalOptions = LayoutOptions.FillAndExpand;
                sb.BackgroundColor = Color.Transparent;
                sb.TextChanged += searchbar_TextChanged;
                sb.HeightRequest = 40;
                sb.Behaviors.Add(new TextChangedBehavior());
                sb.Placeholder = "Recipe name";
                 frame.Content = sb;

                StackLayout chatNavBarLayout = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
                 chatNavBarLayout.Children.Add(frame);
               // chatNavBarLayout.Children.Add(new Label() { Text = "test" });

                NavigationPage.SetTitleView(this, chatNavBarLayout);
            }
            catch (Exception e)
            {

            }


           

        }
        private void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {


            categoriesList.ItemsSource = App.Database.SearchRecipeInCategory(e.NewTextValue.ToUpper(), SelectedCategory);


        }
    }
}
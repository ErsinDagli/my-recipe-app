using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Recipe_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryButtonsPage : ContentPage
	{
        Color redColor = Color.FromRgba(190, 104, 100, 190);

        void ReloadButtons()
        {
            //get categories
            var categories = App.Database.GetCategories();

            int col = 1;
            foreach (var category in categories)
            {
                Frame f = new Frame() {
                    HeightRequest = 150, WidthRequest = 150 ,CornerRadius = 20, BackgroundColor = redColor, HasShadow = false,
                };


                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                tapGestureRecognizer.CommandParameter = category.CategoryName;
                f.GestureRecognizers.Add(tapGestureRecognizer);

               StackLayout sl = new StackLayout();
                sl.Margin = new Thickness(-10, -15, -10, -15);
                sl.Children.Add(new Label() {
                    Text = category.CategoryName,
                    VerticalTextAlignment = TextAlignment.Center, FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White
                });

                sl.Children.Add(new Image() { HeightRequest = 100, WidthRequest = 100, Source = category.ImageFilePath});

                f.Content = sl;

                Button button = new Button();

                button.HorizontalOptions = LayoutOptions.Center;
                button.VerticalOptions = LayoutOptions.Center;
                // add a navigation to each button, navigate to a category page that contains a list of each item in the category
                button.Text = category.CategoryName +
                    Environment.NewLine + 
                    "Total Recipes " + 
                    App.Database.GetCountRecipesInCategory(category.CategoryName);

                if(col % 2 == 0)
                    stack2.Children.Add(f);
                else
                    stack1.Children.Add(f);


                //button.HeightRequest = 150;
                //button.WidthRequest = 150;
                //button.CornerRadius = 20;
                //button.Margin = new Thickness(5);
                ////give the button a random colour
                //button.BackgroundColor = redColor;
                //button.TextColor = Color.Black;



                //button.Clicked += async (sender, e) => await Navigation.PushAsync(new Categories(category.CategoryName));


                col++;
            }

            col = 1;
        }

		public CategoryButtonsPage ()
		{
			InitializeComponent ();



            BreakfastFrame.BackgroundColor = redColor;
            LunchFrame.BackgroundColor = redColor;
            DinnerFrame.BackgroundColor = redColor;
            DessertFrame.BackgroundColor = redColor;
            QuickBitesFrame.BackgroundColor = redColor;
            SaladsFrame.BackgroundColor = redColor;


            ReloadButtons();





            if (MainPage.TurkishClicked == false)
            {
                BreakfastButton.Text = Language.CategoryBreakfastEnglish;
                Lunch.Text = Language.CategoryLunchEnglish;
                Dinner.Text = Language.CategoryDinnerEnglish;
                QuickBites.Text = Language.CategoryQuickBitesEN;
                Desserts.Text = Language.CategoryDessertsEN;
                Salads.Text = Language.CategorySaladsEN;
                SaveCategoryButton.Text = Language.SaveButtonEnglish;


            }
            else
            {
                BreakfastButton.Text = Language.CategoryBreakfastTurkish;
                Lunch.Text = Language.CategoryLunchTurkish;
                Dinner.Text = Language.CategoryDinnerTurkish;
                QuickBites.Text = Language.CategoryQuickBitesTR;
                Desserts.Text = Language.CategoryDessertsTR;
                Salads.Text = Language.CategorySaladsTR;
                SaveCategoryButton.Text = Language.SaveButtonTurkish;
            }


            //setup count category recipes
            CountRecipesPerCategory();



            try
            {
                MessagingCenter.Subscribe<Categories, string>(this, "Deleted", (sender, deletedCat) => {


                    var buttonToDelete = stack1.Children.Where(x => ((Button)x).Text.Contains(deletedCat)).FirstOrDefault();
                    if(buttonToDelete != null)
                        stack1.Children.Remove(buttonToDelete);
                    else
                    {
                        buttonToDelete = stack2.Children.Where(x => ((Button)x).Text.Contains(deletedCat)).FirstOrDefault();
                        stack2.Children.Remove(buttonToDelete);

                    }


                   
                  

                });

            }
            catch (Exception e)
            {

            }
          
        }

        void CountRecipesPerCategory()
        {
            //BreakfastButton.Text += 
            //       Environment.NewLine +
            //       "Total Recipes " +
            //       App.Database.GetCountRecipesInCategory("Breakfast");

            //Lunch.Text +=
            //   Environment.NewLine +
            //   "Total Recipes " +
            //   App.Database.GetCountRecipesInCategory("Lunch");

            //Dinner.Text +=
            //  Environment.NewLine +
            //  "Total Recipes " +
            //  App.Database.GetCountRecipesInCategory("Dinner");

         

            //QuickBites.Text +=
            //  Environment.NewLine +
            //  "Total Recipes " +
            //  App.Database.GetCountRecipesInCategory("Quick Bites");

            //Desserts.Text +=
            //  Environment.NewLine +
            //  "Total Recipes " +
            //  App.Database.GetCountRecipesInCategory("Desserts");

            //Salads.Text +=
            //  Environment.NewLine +
            //  "Total Recipes " +
            //  App.Database.GetCountRecipesInCategory("Salads");

        }


        protected override void OnAppearing()
        {
            BackgroundImageSource = "pictwoinone.png";


          
        }

        protected override void OnDisappearing()
        {
            BackgroundImageSource = null;
        }
       



        static Random rand = new Random();
        
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            var color = Color.FromHsla(rand.NextDouble(),rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            return color;
            
        }



   

        private async void AddCategoryButton_Clicked(object sender, EventArgs e)
        {
            await scroll.ScrollToAsync(0, 0, false);
            NewCategoryFrame.IsVisible = true;

            categoryButtonPageStack.Unfocus();
            categoryButtonPageStack.Opacity = 0.25;
            
            

        }

        private void SaveCategoryButton_Clicked(object sender, EventArgs e)
        {
            //dont create a button if no text was supplied to new category
            if (((AddCategoryEntry.Text != null) && (AddCategoryEntry.Text != "")))
            {
                Button button = new Button();
                button.Text = AddCategoryEntry.Text;
               
                button.CornerRadius = 20;
                button.WidthRequest = 150;
                button.HeightRequest = 150;
                button.Margin = new Thickness(5);
                button.Clicked += async (s, f) => await Navigation.PushAsync(new Categories(AddCategoryEntry.Text));
               button.BackgroundColor = redColor;
                if ((button.Text != "") || (button.Text != (null)))
                {
                    categoryButtonPageStack.Children.Add(button);

                    //save to properties
                    //Application.Current.Properties[$"{AddCategoryEntry.Text}"] = AddCategoryEntry.Text;



                    //save to SQL!

                    Category c = new Category();
                    c.CategoryName = AddCategoryEntry.Text;

                    App.Database.SaveCategory(c);
                }




                NewCategoryFrame.IsVisible = false;

                categoryButtonPageStack.Focus();
                categoryButtonPageStack.Opacity = 1;


                
         



            } else
            {
                NewCategoryFrame.IsVisible = false;

                categoryButtonPageStack.Focus();
                categoryButtonPageStack.Opacity = 1;
               
            }
            

          


        }

        private void CancelCategoryButton_Clicked(object sender, EventArgs e)
        {
            NewCategoryFrame.IsVisible = false;

            categoryButtonPageStack.Focus();
            categoryButtonPageStack.Opacity = 1;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Categories(((TappedEventArgs)e).Parameter.ToString()));

            }
            catch
            {

            }

        }
    }
}
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
                    HeightRequest = 200, WidthRequest = 150 ,CornerRadius = 20, BackgroundColor = redColor, HasShadow = true,
                };


                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                tapGestureRecognizer.CommandParameter = category.CategoryName;
                f.GestureRecognizers.Add(tapGestureRecognizer);

               StackLayout sl = new StackLayout() { VerticalOptions= LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand};
                //sl.Margin = new Thickness(-10, -15, -10, -15);
                sl.Margin = new Thickness(0, 5, 0, 5);

                sl.Children.Add(new Label() {
                    Text = category.CategoryName,
                    VerticalTextAlignment = TextAlignment.Center, FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White
                    ,VerticalOptions  = LayoutOptions.Start
                });

                Frame imageFrame = new Frame()
                {
                    Margin = new Thickness(0),
                    Padding = new Thickness(0),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    CornerRadius = 100,
                    IsClippedToBounds = true,
                    BackgroundColor = Color.Transparent,
                    Content = new Image() {Aspect = Aspect.Fill, HeightRequest = 100, WidthRequest = 100, Source = category.ImageFilePath, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                    
                 };

                sl.Children.Add(imageFrame);
                sl.Children.Add(new Label()
                {
                    Margin = new Thickness(0, 5, 0, 0),
                    Text = App.Database.GetCountRecipesInCategory(category.CategoryName) == 1 ? App.Database.GetCountRecipesInCategory(category.CategoryName).ToString() + " recipe" : App.Database.GetCountRecipesInCategory(category.CategoryName).ToString()  + " recipes",
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White ,
                    VerticalOptions = LayoutOptions.Center
                });

                f.Content = sl;



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
            breaklbl.Text = App.Database.GetCountRecipesInCategory("Breakfast") == 1 ? App.Database.GetCountRecipesInCategory("Breakfast").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Breakfast").ToString() + " recipes";


            lunchlbl.Text = App.Database.GetCountRecipesInCategory("Lunch") == 1 ? App.Database.GetCountRecipesInCategory("Lunch").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Lunch").ToString() + " recipes";


            dinnerlbl.Text = App.Database.GetCountRecipesInCategory("Dinner") == 1 ? App.Database.GetCountRecipesInCategory("Dinner").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Dinner").ToString() + " recipes";




            biteslbl.Text = App.Database.GetCountRecipesInCategory("Quick Bites") == 1 ? App.Database.GetCountRecipesInCategory("Quick Bites").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Quick Bites").ToString() + " recipes";


            dessertslbl.Text = App.Database.GetCountRecipesInCategory("Desserts") == 1 ? App.Database.GetCountRecipesInCategory("Desserts").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Desserts").ToString() + " recipes";


            saladslbl.Text = App.Database.GetCountRecipesInCategory("Salads") == 1 ? App.Database.GetCountRecipesInCategory("Salads").ToString() + " recipe" : App.Database.GetCountRecipesInCategory("Salads").ToString() + " recipes";


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
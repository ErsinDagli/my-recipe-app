using Newtonsoft.Json;
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
        int addColumn = 0;

        void SetButtonFrameColors()
        {

            //loop through all frames in stack layout
            foreach(Frame frame in stack1.Children)
            {
                try
                {
                    var categoryName = ((frame.Content as StackLayout).Children[0] as Label).Text;
                    if (Application.Current.Properties.ContainsKey(categoryName + "-color"))
                    {
                        var colorrgba = Application.Current.Properties[categoryName + "-color"].ToString();

                        var RGBA = colorrgba.Split(',');
                        var R = Convert.ToDouble(RGBA[0]);
                        var G = Convert.ToDouble(RGBA[1]);
                        var B = Convert.ToDouble(RGBA[2]);
                        var A = Convert.ToDouble(RGBA[3]);

                        frame.BackgroundColor = new Color(R, G, B, A);
                    }
                }
                catch
                {

                }
              
            }

            foreach (Frame frame in stack2.Children)
            {
                try
                {
                    var categoryName = ((frame.Content as StackLayout).Children[0] as Label).Text;
                    if (Application.Current.Properties.ContainsKey(categoryName + "-color"))
                    {
                        var colorrgba = Application.Current.Properties[categoryName + "-color"].ToString();

                        var RGBA = colorrgba.Split(',');
                        var R = Convert.ToDouble(RGBA[0]);
                        var G = Convert.ToDouble(RGBA[1]);
                        var B = Convert.ToDouble(RGBA[2]);
                        var A = Convert.ToDouble(RGBA[3]);

                        frame.BackgroundColor = new Color(R, G, B ,A );
                    }
                }
                catch
                {

                }

            }

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
                CancelCategoryButton.Text = Language.CancelButtonEnglish;
                AddCategoryEntry.Placeholder = Language.AddCategoryPlaceholderEn;

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
                CancelCategoryButton.Text = Language.CancelButtonTurkish;
                AddCategoryEntry.Placeholder = Language.AddCategoryPlaceholderTR;

            }


            //setup count category recipes
            CountRecipesPerCategory();

            //try
            //{
            //    MessagingCenter.Subscribe<Categories, string>(this, "PicUpdated", (sender, editedCat) => {

            //        var buttonTUpdate = stack1.Children.Where(x => (((StackLayout)((Frame)x).Content).Children.FirstOrDefault() as Label)?.Text.Contains(editedCat) == true).FirstOrDefault();
            //        if (buttonTUpdate != null)
            //        {
            //            stack1.
            //        }
            //            stack1.Children.Remove(buttonToDelete);




            //    });
            //}
            //catch
            //{

            //}

            try
            {
                MessagingCenter.Subscribe<Categories, string>(this, "Deleted", (sender, deletedCat) => {


                    var buttonToDelete = stack1.Children.Where(x => (((StackLayout)((Frame)x).Content).Children.FirstOrDefault() as Label)?.Text.Contains(deletedCat) == true).FirstOrDefault();
                    if (buttonToDelete != null)
                        stack1.Children.Remove(buttonToDelete);
                    else
                    {
                        buttonToDelete = stack2.Children.Where(x => (((StackLayout)((Frame)x).Content).Children.FirstOrDefault() as Label)?.Text.Contains(deletedCat) == true).FirstOrDefault();
                        stack2.Children.Remove(buttonToDelete);

                    }





                });

            }
            catch (Exception e)
            {

            }


         
          
        }




        async Task ReloadButtons()
        {
            //get categories
            var categories = await App.Database.GetCategories();

            int col = 1;
            foreach (var category in categories)
            {
                Frame f = new Frame()
                {
                    HeightRequest = 200,
                    WidthRequest = 150,
                    CornerRadius = 20,
                    BackgroundColor = redColor,
                    HasShadow = true,
                };


                //var categoryName = ((f.Content as StackLayout).Children[0] as Label).Text;
                var categoryName = category.CategoryName;

                if (Application.Current.Properties.ContainsKey(categoryName + "-color"))
                {
                    var colorrgba = Application.Current.Properties[categoryName + "-color"].ToString();

                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);

                    f.BackgroundColor = new Color(R, G, B, A);
                }

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                tapGestureRecognizer.CommandParameter = category.CategoryName;
                f.GestureRecognizers.Add(tapGestureRecognizer);

                StackLayout sl = new StackLayout() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                //sl.Margin = new Thickness(-10, -15, -10, -15);
                sl.Margin = new Thickness(0, 5, 0, 5);

                sl.Children.Add(new Label()
                {
                    Text = category.CategoryName,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White
                    ,
                    VerticalOptions = LayoutOptions.Start
                });

               // if (!string.IsNullOrWhiteSpace(category.ImageFilePath))
                //{
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
                        BackgroundColor = Color.Transparent
                    };

                if (!string.IsNullOrWhiteSpace(category.ImageFilePath))
                {
                    imageFrame.Content = new Image() { Aspect = Aspect.AspectFill, HeightRequest = 100, WidthRequest = 100, Source = category.ImageFilePath, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

                }
                else
                {
                    imageFrame.Content = new Image() { Aspect = Aspect.AspectFill, HeightRequest = 100, WidthRequest = 100, Source = "recipeplaceholder.png", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

                }

                sl.Children.Add(imageFrame);
           
             



                sl.Children.Add(new Label()
                {
                    Margin = new Thickness(0, 5, 0, 0),
                    Text = await App.Database.GetCountRecipesInCategory(category.CategoryName) == 1 ? (await App.Database.GetCountRecipesInCategory(category.CategoryName)).ToString() + " recipe" : (await App.Database.GetCountRecipesInCategory(category.CategoryName)).ToString() + " recipes",
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.EndAndExpand
                });

                f.Content = sl;



                if (col % 2 == 0)
                    stack2.Children.Add(f);
                else
                    stack1.Children.Add(f);




                col++;
            }

            col = 1;
        }

        async Task CountRecipesPerCategory()
        {
            breaklbl.Text = await App.Database.GetCountRecipesInCategory("Breakfast") == 1 ?  "1 recipe" : (await App.Database.GetCountRecipesInCategory("Breakfast")).ToString() + " recipes";


            lunchlbl.Text = await App.Database.GetCountRecipesInCategory("Lunch") == 1 ? "1 recipe" : (await App.Database.GetCountRecipesInCategory("Lunch")).ToString() + " recipes";


            dinnerlbl.Text = await App.Database.GetCountRecipesInCategory("Dinner") == 1 ? "1 recipe" : (await App.Database.GetCountRecipesInCategory("Dinner")).ToString() + " recipes";




            biteslbl.Text = await App.Database.GetCountRecipesInCategory("Quick Bites") == 1 ? "1 recipe" : (await App.Database.GetCountRecipesInCategory("Quick Bites")).ToString() + " recipes";


            dessertslbl.Text = await App.Database.GetCountRecipesInCategory("Desserts") == 1 ? "1 recipe" : (await App.Database.GetCountRecipesInCategory("Desserts")).ToString() + " recipes";


            saladslbl.Text = await App.Database.GetCountRecipesInCategory("Salads") == 1 ? "1 recipe" : (await App.Database.GetCountRecipesInCategory("Salads")).ToString() + " recipes";


        }


        protected override void OnAppearing()
        {
            BackgroundImageSource = "pictwoinone.png";

            try
            {

               


                if (Application.Current.Properties.ContainsKey("Breakfast-color"))
                {
                    var colorrgba = Application.Current.Properties["Breakfast-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    BreakfastFrame.BackgroundColor = new Color(R, G, B, A);
                }
                if (Application.Current.Properties.ContainsKey("Lunch-color"))
                {
                    var colorrgba = Application.Current.Properties["Lunch-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    LunchFrame.BackgroundColor = new Color(R, G, B, A);
                }
                if (Application.Current.Properties.ContainsKey("Dinner-color"))
                {
                    var colorrgba = Application.Current.Properties["Dinner-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    DinnerFrame.BackgroundColor = new Color(R, G, B, A);
                }
                if (Application.Current.Properties.ContainsKey("Desserts-color"))
                {
                    var colorrgba = Application.Current.Properties["Desserts-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    DessertFrame.BackgroundColor = new Color(R, G, B, A);
                }
                if (Application.Current.Properties.ContainsKey("Quick Bites-color"))
                {
                    var colorrgba = Application.Current.Properties["Quick Bites-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    QuickBitesFrame.BackgroundColor = new Color(R, G, B, A);
                }
                if (Application.Current.Properties.ContainsKey("Salads-color"))
                {
                    var colorrgba = Application.Current.Properties["Salads-color"].ToString();
                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);
                    SaladsFrame.BackgroundColor = new Color(R, G, B, A);
                }

                SetButtonFrameColors();


             
            }
            catch
            {

            }
          



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

        private async void SaveCategoryButton_Clicked(object sender, EventArgs e)
        {
            //dont create a button if no text was supplied to new category
            if (((AddCategoryEntry.Text != null) && (AddCategoryEntry.Text != "")))
            {
                Category c = new Category();
                c.CategoryName = AddCategoryEntry.Text;

                var result = await App.Database.SaveCategory(c);
                if (result == 0)
                {
                    await DisplayAlert("That category already exists", "", "cancel");
                    return;

                }

      

                Frame f = new Frame()
                {
                    HeightRequest = 200,
                    WidthRequest = 150,
                    CornerRadius = 20,
                    BackgroundColor = redColor,
                    HasShadow = true,
                };

      
            

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                tapGestureRecognizer.CommandParameter = AddCategoryEntry.Text;
                f.GestureRecognizers.Add(tapGestureRecognizer);

                StackLayout sl = new StackLayout() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                //sl.Margin = new Thickness(-10, -15, -10, -15);
                sl.Margin = new Thickness(0, 5, 0, 5);

                sl.Children.Add(new Label()
                {
                    Text = AddCategoryEntry.Text,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White
                    ,
                    VerticalOptions = LayoutOptions.Start
                });

                //if (!string.IsNullOrWhiteSpace(category.ImageFilePath))
                //{
                //    Frame imageFrame = new Frame()
                //    {
                //        Margin = new Thickness(0),
                //        Padding = new Thickness(0),
                //        HorizontalOptions = LayoutOptions.Center,
                //        VerticalOptions = LayoutOptions.Center,
                //        HeightRequest = 100,
                //        WidthRequest = 100,
                //        CornerRadius = 100,
                //        IsClippedToBounds = true,
                //        BackgroundColor = Color.Transparent,
                //        Content = new Image() { Aspect = Aspect.Fill, HeightRequest = 100, WidthRequest = 100, Source = category.ImageFilePath, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                //    };

                //    sl.Children.Add(imageFrame);
                //}



                sl.Children.Add(new Label()
                {
                    Margin = new Thickness(0, 5, 0, 0),
                    Text = await App.Database.GetCountRecipesInCategory(AddCategoryEntry.Text) == 1 ? (await App.Database.GetCountRecipesInCategory(AddCategoryEntry.Text)).ToString() + " recipe" : (await App.Database.GetCountRecipesInCategory(AddCategoryEntry.Text)).ToString() + " recipes",
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.EndAndExpand
                });

                f.Content = sl;



                if(addColumn % 2 == 0)
                    stack2.Children.Add(f);
              else
                    stack1.Children.Add(f);


                addColumn++;




                NewCategoryFrame.IsVisible = false;

                categoryButtonPageStack.Focus();
                categoryButtonPageStack.Opacity = 1;


                //scroll to bottom
                try
                {
                    var lastChild = stack2.Children.LastOrDefault();
                    if (lastChild != null)
                       await scroll.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, true);
                }
                catch
                {

                }




            }
            else
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
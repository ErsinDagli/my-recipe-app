﻿using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Recipe_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryButtonsPage : ContentPage
	{

        

		public CategoryButtonsPage ()
		{
			InitializeComponent ();
            


            BreakfastButton.BackgroundColor = GetRandomColor();
            Lunch.BackgroundColor = GetRandomColor();
            Dinner.BackgroundColor = GetRandomColor();
            Desserts.BackgroundColor = GetRandomColor();
            QuickBites.BackgroundColor = GetRandomColor();
            Salads.BackgroundColor = GetRandomColor();


           

            //ADDING BUTTONS BASED ON PROPERTIES DICTIONARY

            //foreach (KeyValuePair<string, object> category in Application.Current.Properties)
            //{
            //    //creating a new button for each category, giving its text as the category, use properties to store the category


            //    Button button = new Button();

            //    button.HorizontalOptions = LayoutOptions.Center;
            //    button.VerticalOptions = LayoutOptions.Center;
            //    // add a navigation to each button, navigate to a category page that contains a list of each item in the category
            //    button.Text = category.Value.ToString();
            //    categoryButtonPageStack.Children.Add(button);

            //    button.HeightRequest = 150;
            //    button.WidthRequest = 150;
            //    button.CornerRadius = 20;

            //    //give the button a random colour
            //    button.BackgroundColor = GetRandomColor();
            //    button.TextColor = Color.Black;



            //    button.Clicked += async (sender, e) => await Navigation.PushAsync(new Categories(category.Value.ToString()));


            //}



            if(MainPage.TurkishClicked == false)
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


        }


        protected override void OnAppearing()
        {
            BackgroundImage = "pictwoinone.png";


            //if (App.CategoryDeleted)
            //{

            //}
        }

        protected override void OnDisappearing()
        {
            BackgroundImage = null;
        }
       



        static Random rand = new Random();
        
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            var color = Color.FromHsla(rand.NextDouble(),rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            return color;
            
        }



   



        private async void BreakfastButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Breakfast"));
        }

        private async void Lunch_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Lunch"));
        }

        private async void Dinner_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Dinner"));
        }

        private async void Desserts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Desserts"));
        }

        private async void QuickBites_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Quick Bites"));
        }

        private async void Salads_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Categories("Salads"));
        }



        private void AddCategoryButton_Clicked(object sender, EventArgs e)
        {
            AddCategoryEntry.IsVisible = true;
            SaveCategoryButton.IsVisible = true;

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

               
               
            

                    AddCategoryEntry.IsVisible = false;
                SaveCategoryButton.IsVisible = false;
                categoryButtonPageStack.Opacity = 1;
                
               
            } else
            {
                AddCategoryEntry.IsVisible = false;
                SaveCategoryButton.IsVisible = false;
                categoryButtonPageStack.Opacity = 1;
               
            }
            

          


        }
    }
}
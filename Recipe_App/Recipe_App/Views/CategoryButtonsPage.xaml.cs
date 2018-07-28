using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BindingContext = new SQLentry();

            BreakfastButton.BackgroundColor = GetRandomColor();
            Lunch.BackgroundColor = GetRandomColor();
            Dinner.BackgroundColor = GetRandomColor();
            Desserts.BackgroundColor = GetRandomColor();
            QuickBites.BackgroundColor = GetRandomColor();
            Salads.BackgroundColor = GetRandomColor();






            //TapGestureRecognizer tgr = new TapGestureRecognizer();
            //tgr.NumberOfTapsRequired = 2;
            //tgr.Tapped += (object sender, EventArgs e) => {
            //    categoryButtonPageStack.Children.Remove((Button)sender);
            //};



            //ADDING BUTTONS BASED ON PROPERTIES DICTIONARY

            foreach (KeyValuePair<string, object> category in Application.Current.Properties)
            {
                //creating a new button for each category, giving its text as the category, use properties to store the category


                Button button = new Button();

                button.HorizontalOptions = LayoutOptions.Center;
                button.VerticalOptions = LayoutOptions.Center;
                // add a navigation to each button, navigate to a category page that contains a list of each item in the category
                button.Text = category.Value.ToString();
                categoryButtonPageStack.Children.Add(button);

                button.HeightRequest = 100;
                button.WidthRequest = 100;
                button.CornerRadius = 20;

                //give the button a random colour
                button.BackgroundColor = GetRandomColor();

                //button.GestureRecognizers.Add(tgr);



                button.Clicked += async (sender, e) => await Navigation.PushAsync(new Categories(category.Value.ToString()));


            }








        }


        static Random rand = new Random();
        
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            var color = Color.FromHsla(rand.NextDouble(),rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            return color;
            
        }



        private void DeleteButtons_Clicked(object sender, EventArgs e)
        {
            List<Button> buttonList = new List<Button>();
            foreach (var children in categoryButtonPageStack.Children)
            {
                if ((children.GetType() == typeof(Button)))
                {
                    buttonList.Add(children as Button);
                }
            }
            foreach (var ch in buttonList)
            {
                categoryButtonPageStack.Children.Remove(ch as Button);
            }

            Application.Current.Properties.Clear();
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
            
            
            //Categories.categoryListEntries.Add(entry.Text);


            
            

        }

        private void SaveCategoryButton_Clicked(object sender, EventArgs e)
        {
            //dont create a button if no text was supplied to new category
            if (((AddCategoryEntry.Text != null) && (AddCategoryEntry.Text != "")))
            {
                Button button = new Button();
                button.Text = AddCategoryEntry.Text;
               
                button.CornerRadius = 20;
                button.WidthRequest = 100;
                button.HeightRequest = 100;
                button.Clicked += async (s, f) => await Navigation.PushAsync(new Categories(AddCategoryEntry.Text));
               
                if ((button.Text != "") || (button.Text != (null)))
                {
                    categoryButtonPageStack.Children.Add(button);
                    Application.Current.Properties[$"{AddCategoryEntry.Text}"] = AddCategoryEntry.Text;
                }

               
               
            

                    AddCategoryEntry.IsVisible = false;
                SaveCategoryButton.IsVisible = false;
                categoryButtonPageStack.Opacity = 1;
                
                //AddCategoryEntry.Text
                //AddCategoryEntry.Text = "";
            } else
            {
                AddCategoryEntry.IsVisible = false;
                SaveCategoryButton.IsVisible = false;
                categoryButtonPageStack.Opacity = 1;
               
                //AddCategoryEntry.Text
                // AddCategoryEntry.Text = "";
            }
            

          


        }
    }
}
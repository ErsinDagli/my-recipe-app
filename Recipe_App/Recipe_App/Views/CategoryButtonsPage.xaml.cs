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



            Application.Current.Properties.Remove("");

            TapGestureRecognizer tgr = new TapGestureRecognizer();
            tgr.NumberOfTapsRequired = 2;
            tgr.Tapped += (object sender, EventArgs e) => {
                categoryButtonPageStack.Children.Remove((Button)sender);
            };


            foreach (KeyValuePair<string, object> category in Application.Current.Properties)
                    {
                        //creating a new button for each category, giving its text as the category, use properties to store the category

                        
                        Button button = new Button();
                        
                        button.HorizontalOptions = LayoutOptions.Center;
                        button.VerticalOptions = LayoutOptions.Center;
                        // add a navigation to each button, navigate to a category page that contains a list of each item in the category
                        button.Text = category.Value.ToString();
                        categoryButtonPageStack.Children.Add(button);


                        button.GestureRecognizers.Add(tgr);



                button.Clicked += async (sender, e) => await Navigation.PushAsync(new Categories(category.Value.ToString()));
                        

                    }


           





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


        
    }
}
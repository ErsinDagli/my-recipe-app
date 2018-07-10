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
	public partial class NavigateToRecipesPage : ContentPage
	{
		public NavigateToRecipesPage ()
		{
			InitializeComponent ();
            BindingContext = new NavigateToRecipesPageViewModel();
            




		}

        private async void  Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecipesPage());

        }
    }
}
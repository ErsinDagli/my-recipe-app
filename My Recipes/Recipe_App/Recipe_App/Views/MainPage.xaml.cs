using Recipe_App.ViewModels;
using Xamarin.Forms;


namespace Recipe_App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            
            InitializeComponent();
            
            BindingContext = new MainPageViewModel();


        

        }


        protected override void OnAppearing()
        {
            BackgroundImage = "MainPage.png";
        }

        protected override void OnDisappearing()
        {
            BackgroundImage = null;
        }












    }
}

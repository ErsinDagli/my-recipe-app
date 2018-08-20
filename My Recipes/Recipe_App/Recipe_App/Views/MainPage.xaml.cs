using Recipe_App.Model;
using Recipe_App.ViewModels;
using Xamarin.Forms;


namespace Recipe_App
{
	public partial class MainPage : ContentPage
	{

        public static bool TurkishClicked = false;

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

        private void TurkishButton_Clicked(object sender, System.EventArgs e)
        {
            TurkishClicked = true;

        }

        private void EnglishButton_Clicked(object sender, System.EventArgs e)
        {
            TurkishClicked = false;
        }
    }
}

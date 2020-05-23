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

            if(TurkishClicked == false)
            {
                AddRecipebutton.Text = Language.MainPageAddRecipeButtonEN;
                SearchRecipeButton.Text = Language.MainPageSearchRecipesbuttonEN;


            }
            else
            {
                AddRecipebutton.Text = Language.MainPageAddRecipeButtonTR;
                SearchRecipeButton.Text = Language.MainPageSearchRecipeButtonTR;

            }
        

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
          


        }

        private void EnglishButton_Clicked(object sender, System.EventArgs e)
        {
          
        }

        private void LanguagePicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                var language = ((Picker)sender).SelectedItem?.ToString();

                if(language == "English")
                {
                    TurkishClicked = false;
                    AddRecipebutton.Text = Language.MainPageAddRecipeButtonEN;
                    SearchRecipeButton.Text = Language.MainPageSearchRecipesbuttonEN;

                }
                else if(language == "Turkish")
                {
                    TurkishClicked = true;
                    AddRecipebutton.Text = Language.MainPageAddRecipeButtonTR;
                    SearchRecipeButton.Text = Language.MainPageSearchRecipeButtonTR;

                }
            }
            catch
            {

            }
        }
    }
}

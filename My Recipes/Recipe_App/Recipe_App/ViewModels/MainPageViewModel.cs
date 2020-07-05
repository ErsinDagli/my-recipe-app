using Recipe_App.Views;
using Xamarin.Forms;

namespace Recipe_App.ViewModels
{
    class MainPageViewModel
    {

        public Command NavigateToAddRecipesPage => new Command(async () =>

        await Application.Current.MainPage.Navigation.PushAsync(new AddRecipesPage()));



        public Command NavigateToRecipeSearchPage => new Command(async () =>

          await Application.Current.MainPage.Navigation.PushAsync(new TabbedPage1() {  BarBackgroundColor = Color.White, BarTextColor = Color.Black }));





        

        
    }
    
}

using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Recipe_App.ViewModels
{
    class MainPageViewModel
    {

        public Command NavigateToAddRecipesPage => new Command(async () =>

        await Application.Current.MainPage.Navigation.PushAsync(new AddRecipesPage()));



        public Command NavigateToRecipeSearchPage => new Command(async () =>

          await Application.Current.MainPage.Navigation.PushAsync(new TabbedPage1() {  BarBackgroundColor = Color.Black }));

       // new RecipeSearchPage(AddRecipesPage.categoryList))



        
    }
    
}

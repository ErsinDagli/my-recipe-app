using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Recipe_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDisplayPage : ContentPage
    {
        private SQLentry sqlentry;
        public SQLentry SQLentry { get => sqlentry; set => sqlentry = value;}
        public static MediaFile PicTakenFile { get; set; }


        public RecipeDisplayPage (SQLentry sqlentryselected)
		{
			InitializeComponent ();
             BindingContext = new SQLentry();

            //Populate labels with parameter sqlentry
            SQLentry = sqlentryselected;


            imgRecipe.Source = sqlentryselected.ImageFilePath;
            
                
            editorRecipeName.Text = sqlentryselected.RecipeName;
            editorCategory.Text  = sqlentryselected.Category;
            editorRecipe.Text = sqlentryselected.Recipe;
            editorIngredients.Text = sqlentryselected.Ingredients;
            editorNotes.Text = sqlentryselected.Notes;

           

        }



        private async void Delete_Clicked(object sender, EventArgs e)
        {

            var answer = await DisplayAlert("Delete Recipe", "Are you sure?", "Yes", "No");
          if(answer == true)
            {
                //App.CategoryOC.Remove(SQLentry);
                App.Database.DeleteItem(SQLentry.RecipeID);

                //we need to also delete from the observablecollections
                if (RecipeSearchPage.RecipeNameOC.Contains(SQLentry))
                {
                    RecipeSearchPage.RecipeNameOC.Remove(SQLentry);
                }
               
                

                if (Categories.CategoryOC != null && Categories.CategoryOC.Contains(SQLentry))
                {
                    Categories.CategoryOC.Remove(SQLentry);
                }
                
                


                imgRecipe.Source = "";
                editorRecipeName.Text = "";
                editorCategory.Text = "";
                editorRecipe.Text = "";
                editorIngredients.Text = "";
                editorNotes.Text = "";

                await Navigation.PopToRootAsync();
                
            }
            
            


        }
        //make the editors editable when Edit button clicked
        private async void EditRecipe_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecipesPage(SQLentry));

        

            ///TAP GESTURE STUFF FOR IMAGE
            ///
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += async (s, f) =>
            //{
            //    await TakePhotoAsync();
                
            //    //save the new image to the sqlentry
            //    

            //};
            //imgRecipe.GestureRecognizers.Add(tapGestureRecognizer);
        }

       

        

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
           
        }


        

       
    }
}
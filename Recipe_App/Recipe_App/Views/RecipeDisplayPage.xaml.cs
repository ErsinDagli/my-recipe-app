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
           
        

		public RecipeDisplayPage (SQLentry sqlentryselected)
		{
			InitializeComponent ();
             BindingContext = new SQLentry();

            //Populate labels with parameter sqlentry
            SQLentry = sqlentryselected;


            imgRecipe.Source = sqlentryselected.ImageFilePath;
            
                
            editorRecipeName.Text = sqlentryselected.RecipeName;
            editorCategory.Text = sqlentryselected.Category;
            editorRecipe.Text = sqlentryselected.Recipe;
            editorIngredients.Text = sqlentryselected.Ingredients;
            editorNotes.Text = sqlentryselected.Notes;

		}


        



        private void Delete_Clicked(object sender, EventArgs e)
        {
            
            //App.CategoryOC.Remove(SQLentry);
            App.Database.DeleteItem(SQLentry.RecipeID);

            //we need to also delete from the observablecollection
            RecipeSearchPage.RecipeNameOC.Remove(SQLentry);
           

            imgRecipe.Source = "";
            editorRecipeName.Text = "";
            editorCategory.Text = "";
            editorRecipe.Text = "";
            editorIngredients.Text = "";
            editorNotes.Text = "";
            


        }
        //make the editors editable when Edit button clicked
        private void EditRecipe_Clicked(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = true;
            editorCategory.IsEnabled = true;
            editorIngredients.IsEnabled = true;
            editorRecipe.IsEnabled = true;
            editorNotes.IsEnabled = true;
            //App.Database.SaveItem(SQLentry);
        }

       
        //when focus is shifted from editors they save, and update the SQLentry properties
        private void editorRecipeName_Completed(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = false;
            editorCategory.IsEnabled = false;
            editorIngredients.IsEnabled = false;
            editorRecipe.IsEnabled = false;
            editorNotes.IsEnabled = false;
            SQLentry.RecipeName = editorRecipeName.Text;

        }

        private void editorCategory_Completed(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = false;
            editorCategory.IsEnabled = false;
            editorIngredients.IsEnabled = false;
            editorRecipe.IsEnabled = false;
            editorNotes.IsEnabled = false;
            SQLentry.Category = editorCategory.Text;
        }

        private void editorIngredients_Completed(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = false;
            editorCategory.IsEnabled = false;
            editorIngredients.IsEnabled = false;
            editorRecipe.IsEnabled = false;
            editorNotes.IsEnabled = false;
            SQLentry.Ingredients = editorIngredients.Text;
        }

        private void editorRecipe_Completed(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = false;
            editorCategory.IsEnabled = false;
            editorIngredients.IsEnabled = false;
            editorRecipe.IsEnabled = false;
            editorNotes.IsEnabled = false;
            SQLentry.Recipe = editorRecipe.Text;
        }

        private void editorNotes_Completed(object sender, EventArgs e)
        {
            editorRecipeName.IsEnabled = false;
            editorCategory.IsEnabled = false;
            editorIngredients.IsEnabled = false;
            editorRecipe.IsEnabled = false;
            editorNotes.IsEnabled = false;
            SQLentry.Notes = editorNotes.Text;
        }

        private async Task SaveButton_Clicked(object sender, EventArgs e)
        {
            SQLentry entry = new SQLentry();
            entry.RecipeID = SQLentry.RecipeID;
            entry.RecipeName = editorRecipeName.Text;
            if (editorCategory.Text == null)
            {
                entry.Category = "Uncategorized";
            }
            else { entry.Category = editorCategory.Text; }



            //here we are saving in the properties dictionary all the categories the user inputs
            Application.Current.Properties[$"{editorCategory.Text}"] = editorCategory.Text;

            entry.Recipe = editorRecipe.Text;
            entry.Notes = editorNotes.Text;
            entry.Ingredients = editorIngredients.Text;
            //if (PicTakenFile != null)
            //{
            //    entry.ImageFilePath = PicTakenFile.Path.ToString();
            //}
            //else
            //{
            //    entry.ImageFilePath = "";
            //}





            //entry.ImageFilePath = PicTakenFile.ToString();
            int i = App.Database.SaveItem(entry);

            if (i > 0)
            {
                App.Database.SaveItem(entry);
                await DisplayAlert("Recipe Edit", "Save successful", "OK");

            }
            else
            {
                await DisplayAlert("Oops!", "Try Again", "OK");
            }
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
           
        }

       
    }
}
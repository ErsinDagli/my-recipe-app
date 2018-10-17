using Plugin.Media.Abstractions;
using Recipe_App.ViewModels;
using System;
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
            
             //This Changes first letter of each word to Upper case   
            editorRecipeName.Text = char.ToUpper(sqlentryselected.RecipeName[0]) + sqlentryselected.RecipeName.Substring(1).ToLower();

            //changing display of category based on turkish or english selected
            if(MainPage.TurkishClicked == true)
            {
                if(sqlentryselected.Category == "Breakfast")
                {
                    editorCategory.Text = "Kahvaltı";

                }
                else if (sqlentryselected.Category == "Lunch")
                {
                    editorCategory.Text = "Öğlen Yemeği";
                }
                else if (sqlentryselected.Category == "Dinner")
                {
                    editorCategory.Text = "Akşam yemeği";
                }
                else if (sqlentryselected.Category == "Desserts")
                {
                    editorCategory.Text = "Tatlılar";
                }
                else if (sqlentryselected.Category == "Quick Bites")
                {
                    editorCategory.Text = "Aperatifler";
                }else
                {
                    editorCategory.Text = sqlentryselected.Category;
                }
            }
            else
            {
                editorCategory.Text = sqlentryselected.Category;
            }

            
            editorRecipe.Text = sqlentryselected.Recipe;
            editorIngredients.Text = sqlentryselected.Ingredients;
            editorNotes.Text = sqlentryselected.Notes;



            if (MainPage.TurkishClicked == false)
            {
               
                recipeNameLabel.Text = Language.RecipeNameEnglish;
                categoryLabel.Text = Language.CategoryEnglish;
                ingredientsLabel.Text = Language.IngredientsEnglish;
                recipeLabel.Text = Language.RecipeEnglish;
                notesLabel.Text = Language.NotesEnglish;
                EditRecipe.Text = Language.EditRecipeButtonEN;
                Delete.Text = Language.DeleteButtonEN;


            }
            else
            {
                
                recipeNameLabel.Text = Language.RecipeNameTurkish;
                categoryLabel.Text = Language.CategoryTurkish;
                ingredientsLabel.Text = Language.IngredientsTurkish;
                recipeLabel.Text = Language.RecipeTurkish;
                notesLabel.Text = Language.NotesTurkish;
                EditRecipe.Text = Language.EditRecipeButtonTR;
                Delete.Text = Language.DeleteButtonTR;

            }


        }



        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer;
            if(MainPage.TurkishClicked == false)
            {
                answer = await DisplayAlert("Delete Recipe", "Are you sure?", "Yes", "No");
            }
            else
            {
                answer = await DisplayAlert("Tarifi Sil", "Emin Misiniz?", "Evet", "Hayır");
            }
           
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

        
        }

       

        



        

       
    }
}
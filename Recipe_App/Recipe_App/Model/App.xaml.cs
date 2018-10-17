using Recipe_App.ViewModels;
using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Recipe_App
{
	public partial class App : Application
	{
       
        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Black
                
                
            };
            NavigationPage.SetHasNavigationBar(MainPage, false);
           


            SQLentry fileexist = App.Database.GetItem("SALMON WITH HERB SAUCE RAVIGOTE");
            if (fileexist == null)
            {
                SQLentry stockRecipe1 = new SQLentry();
                stockRecipe1.RecipeName = "SALMON WITH HERB SAUCE RAVIGOTE";
                stockRecipe1.Category = "Dinner";
                stockRecipe1.Ingredients = "250g salmon" + Environment.NewLine + "Handful of thyme" + Environment.NewLine + "Handful of rosemary and thyme"
                    + Environment.NewLine + "2 lemons" + Environment.NewLine + "Vinegar" + Environment.NewLine + "250g prepared chips or potatoes"
                    + Environment.NewLine + "Olive Oil/Butter";
                stockRecipe1.Recipe = "1) Pour some oil/melt butter in a non stick frying pan " + Environment.NewLine +
                                      "2) Cook the salmon the pan on low heatt until the skin is slightly hardened" + Environment.NewLine +
                                      "3) In a mortar and pestle crush the rosemary and thyme with some lemon juice and vinegar for the sauce ravigote " + Environment.NewLine +
                                      "4) Cook the chips in the olive oil" + Environment.NewLine +
                                      "5) Serve the chips and salmon with sauce ravigote poured to taste"

                   ;

                //this is how we use an IMAGE from the resources drawable. just quote the name of file with extension
                //BE CAREFUL its best to use PNG extension. If you file is jpg, dont change it directly, better to open via paint and savea as png, otherwise becomes
                //unusable currupt file
                stockRecipe1.ImageFilePath = "salmon.png";


                Database.SaveItem(stockRecipe1);
            }

            SQLentry fileexist2 = App.Database.GetItem("FRENCH TOAST");
            if (fileexist2 == null)
            {
                SQLentry stockRecipe1 = new SQLentry();
                stockRecipe1.RecipeName = "FRENCH TOAST";
                stockRecipe1.Category = "Breakfast";
                stockRecipe1.Ingredients = "2 pieces of toast" + Environment.NewLine + "Honey" + Environment.NewLine + "Half a Banana"
                    + Environment.NewLine + "Handful of Strawberries" + Environment.NewLine + "1 Egg" + Environment.NewLine + "Butter";
                stockRecipe1.Recipe = "1) Crack and mix the eggs in a flat plate" + Environment.NewLine +
                                       "2) Dab both sides of the toast onto the eggs and place on a low heat flat frying pan" + Environment.NewLine +
                                       "3) When the toast is as desired pour honey and place your chopped fruits ontop";

                //this is how we use an IMAGE from the resources drawable. just quote the name of file with extension
                //BE CAREFUL its best to use PNG extension. If you file is jpg, dont change it directly, better to open via paint and savea as png, otherwise becomes
                //unusable currupt file
                stockRecipe1.ImageFilePath = "FrenchToast.png";


                Database.SaveItem(stockRecipe1);
            }




        }


        

        public static SQLHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLHelper();
                } 
                return database;
            }
        }

        public object Glide { get; }

        private static SQLHelper database;



        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}



       


    }

}

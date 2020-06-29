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
                BarBackgroundColor = Color.LightBlue,
                BarTextColor= Color.White,
                
                
                
                
            };
            NavigationPage.SetHasNavigationBar(MainPage, false);
           


            SQLentry fileexist = App.Database.GetItem("SALMON WITH HERB SAUCE RAVIGOTE");
            if (fileexist == null)
            {
                SQLentry stockRecipe1 = new SQLentry();
                stockRecipe1.RecipeName = "SALMON WITH HERB SAUCE RAVIGOTE";
                stockRecipe1.Category = "Dinner";
                stockRecipe1.Ingredients = "250g salmon" + Environment.NewLine + "Handful of thyme" + Environment.NewLine + "Handful of rosemary"
                    + Environment.NewLine + "2 lemons" + Environment.NewLine + "Vinegar" + Environment.NewLine + "250g prepared chips or potatoes";
                stockRecipe1.Recipe = "Cook the salmon etc etc";

                //this is how we use an IMAGE from the resources drawable. just quote the name of file with extension
                //BE CAREFUL its best to use PNG extension. If you file is jpg, dont change it directly, better to open via paint and savea as png, otherwise becomes
                //unusable currupt file
                stockRecipe1.ImageFilePath = "salmon.png";


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
        public static bool CategoryDeleted { get; internal set; }

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

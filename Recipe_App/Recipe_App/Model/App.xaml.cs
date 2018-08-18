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
       // public static  ObservableCollection<SQLentry> RecipeNameOC { get; set; }
       // public static IList<SQLentry> recipeNameList;
        //public static List<SQLentry> categoryListEntries;
       // public static ObservableCollection<SQLentry> CategoryOC;


        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Black
                
                
            };
            NavigationPage.SetHasNavigationBar(MainPage, false);



            //we create a list first form SQLhelper, then popuplate our OC with this list
            // recipeNameList = App.Database.ListRecipes("");
            // RecipeNameOC = new ObservableCollection<SQLentry>(recipeNameList);



            //we create a list first form SQLhelper, then popuplate our OC with this list
            // categoryListEntries = App.Database.GetCategory(Categories.SelectedCategory);
            //CategoryOC = new ObservableCollection<SQLentry>(categoryListEntries);

            //var newListOfCategories = App.Database.GetCategory(Categories.SelectedCategory);
            //foreach (var item in newListOfCategories)
            //{
            //    App.CategoryOC.Add(item);

            //}




          


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

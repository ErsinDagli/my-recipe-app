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

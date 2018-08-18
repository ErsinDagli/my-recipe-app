using Android.Graphics;
using Recipe_App.ViewModels;
using Recipe_App.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Recipe_App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            
            InitializeComponent();
            
            BindingContext = new MainPageViewModel();


           // newMethod();






        }


        public async void newMethod()
        {
            ImageResizer ir = new ImageResizer();

            Bitmap x =  await ir.GetImageForDisplay("recipeBackground.png", 500, 500);
            MainContentPage.BackgroundImage = x.ToString();


            
         

        }
       




       

      
        



    }
}

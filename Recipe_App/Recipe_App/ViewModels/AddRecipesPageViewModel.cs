using Plugin.SavableObject.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Plugin.Media;
using System.IO;




namespace Recipe_App
{
    class AddRecipesPageViewModel : SavableObject, INotifyPropertyChanged
    {


        public AddRecipesPageViewModel()
        {
            
            Load();
            SaveCommand = new Command(() => Save());
        }
        //Image image;
        string recipeName = "defaultName";
        string ingredients;
        string recipe;
        string notes;
        string category;
        Image pic;
       // public ImageSource img

        public Image Pic
        {
            get => pic;
            set { pic = value; OnPropertyChanged(); }
        }
       

        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }

        }


        public string RecipeName
        {
            get => recipeName;
            set { recipeName = value; OnPropertyChanged(); }
        }

        public string Ingredients
        {
            get => ingredients;
            set { ingredients = value; OnPropertyChanged(); }
        }

        public string Recipe
        {
            get => recipe;
            set { recipe = value; OnPropertyChanged(); }
        }

        public string Notes
        {
            get => notes;
            set { notes = value; OnPropertyChanged(); }
        }
        [IgnoreSave]
        public Command SaveCommand { get; set; }
        public object CrossMedia { get; private set; }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion




        //public List<string> FoodCategories = new List<string>() { "Desserts", "Main Meal", "Pastries", "Quick Bites"};
      


        

    }
}

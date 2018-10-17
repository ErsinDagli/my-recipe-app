using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Recipe_App.Model
{
    class Language : INotifyPropertyChanged
    {

        private string addRecipeHeading;
        public string AddRecipeHeading
        {
            get
            {
                return addRecipeHeading;
            }
            set
            {
                addRecipeHeading = value;
                OnPropertyChanged(addRecipeHeading);
            }
        }

        private string takeImageButton;
        public string TakeImageButton
        {
            get
            {
                return takeImageButton;
            }
            set
            {
                takeImageButton = value;
                OnPropertyChanged(takeImageButton);
            }
        }

        private string chooseImageButton;
        public string ChooseImageButton
        {
            get
            {
                return chooseImageButton;
            }
            set
            {
                chooseImageButton = value;
                OnPropertyChanged(chooseImageButton);
            }
        }



        private string recipeName;
        public string RecipeName
        {
            get
            {
                return recipeName;
            }
            set
            {
                recipeName = value;
                OnPropertyChanged(recipeName);
            }
        }

        private string category;
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged(category);
            }
        }
        private string ingredients;
        public string Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                ingredients = value;
                OnPropertyChanged(ingredients);
            }
        }
        private string recipe;
        public string Recipe
        {
            get
            {
                return recipe;
            }
            set
            {
                recipe = value;
                OnPropertyChanged(recipe);
            }
        }

        private string notes;
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
                OnPropertyChanged(notes);
            }
        }


        private string save;
        public string Save
        {
            get
            {
                return save;
            }
            set
            {
                save = value;
                OnPropertyChanged(save);
            }
        }




        private string breakfast;
        public string Breakfast
        {
            get
            {
                return breakfast;
            }
            set
            {
                breakfast = value;
                OnPropertyChanged(breakfast);
            }
        }


        private string lunch;
        public string Lunch
        {
            get
            {
                return lunch;
            }
            set
            {
                lunch = value;
                OnPropertyChanged(lunch);
            }
        }


        private string dinner;
        public string  Dinner
        {
            get
            {
                return dinner;
            }
            set
            {
                dinner = value;
                OnPropertyChanged(dinner);
            }
        }


        private string desserts;
        public string Desserts
        {
            get
            {
                return desserts;
            }
            set
            {
                desserts = value;
                OnPropertyChanged(desserts);
            }
        }


        private string quickbites;
        public string Quickbites
        {
            get
            {
                return quickbites;
            }
            set
            {
                quickbites = value;
                OnPropertyChanged(quickbites);
            }
        }


        private string salads;
        public string Salads
        {
            get
            {
                return salads;
            }
            set
            {
                salads = value;
                OnPropertyChanged(salads);
            }
        }







        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

       

        

        public Language()
        {
            if(MainPage.TurkishClicked == true)
            {
               TakeImageButton = "Resime Cek";
               ChooseImageButton = "Resim Sec";
               RecipeName = "Tarife:";
               Category = "Kategori:";
               Ingredients = "Icerikler:";
               Recipe = "Tarife:";
               Notes = "Notlar:";
               Save = "Kaydet";
               Breakfast = "Kahvalti";
                Lunch = "Ogle yemegi";
                Dinner = "Aksam Yemegi";
                Desserts = "Tatlilar";
                Quickbites = "Cabuk yiyecekler";
                Salads = "Salatalar";


            } else
            {
                TakeImageButton = "Take Photo";
                ChooseImageButton = "Choose Image";
                RecipeName = "Recipe Name:";
                Category = "Category:";
                Ingredients = "Ingredients:";
                Recipe = "Recipe:";
                Notes = "Notes:";
                Save = "Save";
                Breakfast = "Breakfast";
                Lunch = "Lunch";
                Dinner = "Dinner";
                Desserts = "Desserts";
                Quickbites = "Quick Bites";
                Salads = "Salads";
            }
          
        }


    }
}

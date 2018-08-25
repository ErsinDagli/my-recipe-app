using Recipe_App.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipe_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1 ()
        {
            InitializeComponent();

            if(MainPage.TurkishClicked == false)
            {
                RecipeSearchTab.Title = Language.SearchTabEN;
                CategoryButtonsTab.Title = Language.CategoriesTabEn;
            }
            else
            {
                RecipeSearchTab.Title = Language.SearchTabTR;
                CategoryButtonsTab.Title = Language.CategoriesTabTR;
            }
        }
        
    }
}
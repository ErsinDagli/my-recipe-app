using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recipe_App.ViewModels;
using Recipe_App.Model;
using System.Collections.ObjectModel;
using Recipe_App.Views;

namespace Recipe_App
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipesPage : ContentPage
    {
        
       public static MediaFile PicTakenFile { get; set; }
        
        

        public AddRecipesPage()
        {
            InitializeComponent();
            //BindingContext = new AddRecipesPageViewModel();
             
            
        }





        async void Click_Save(object sender, EventArgs e)
        {
            try
            {


                if (txtRecipeName.Text != null)
                {
                    SQLentry fileexist = App.Database.GetItem(txtRecipeName.Text);
                    if (fileexist == null)
                    {
                        if (txtRecipeName.Text != null)

                        {



                            SQLentry entry = new SQLentry();
                            entry.RecipeName = txtRecipeName.Text;
                            if (txtcategory.Text == null)
                            {
                                //here we are saving in the properties dictionary all the categories the user inputs
                                entry.Category = "Uncategorized";
                                Application.Current.Properties["Uncategorized"] = "Uncategorized";
                            }
                            else
                            {

                                entry.Category = txtcategory.Text;
                                Application.Current.Properties[$"{txtcategory.Text}"] = txtcategory.Text;

                            }



                          

                            entry.Recipe = txtRecipe.Text;
                            entry.Notes = txtNotes.Text;
                            entry.Ingredients = txtIngredients.Text;
                            if (PicTakenFile != null)
                            {
                                entry.ImageFilePath = PicTakenFile.Path.ToString();
                            }
                            else
                            {
                                entry.ImageFilePath = "";
                            }





                            //entry.ImageFilePath = PicTakenFile.ToString();
                            int i = App.Database.SaveItem(entry);
                            //adding the recipename to its OC and the category name to its own OC after save so it can display in the respective lists
                            //App.RecipeNameOC.Add(entry);
                           // App.CategoryOC.Add(entry);

                            if (i > 0)
                            {
                               // App.Database.SaveItem(entry);
                                await DisplayAlert("New Recipe", "Save successful", "OK");
                                await Navigation.PopAsync();

                            }
                            else
                            {
                                await DisplayAlert("Oops!", "Try Again", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("", "Please fill out fields", "");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Save Failed", "Recipe Already Exists ", "OK");
                        txtRecipeName.Text = "";


                    }
                }
                else
                {
                    await DisplayAlert("", "Please Enter name", "");
                }
            }
            catch
            {
                e.ToString();
            }

        }
        // async void Click_Login(object sender, EventArgs e)  
        // {  
        //     await Navigation.PushModalAsync(new MainPage());  
        // }  








        //CAMERA STUFF AND IMAGE CHOOSE STUFF

        public async void CameraButtonClicked(object sender, EventArgs e)
        {


            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "MyRecipes",
                Name = "test.jpg",
                AllowCropping = true,
              
            });

            PicTakenFile = file;

            ////Get the public album path
            //var aPpath = file.AlbumPath;

            ////Get private path
            //var path = file.Path;

            if (file == null)
                return;




            RecipeImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                //file.Dispose();
                return stream;


            });



        }

        private async void ChooseImageClicked(object sender, EventArgs e)
        {
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }

                Stream stream = null;
                var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);


                if (file == null)
                    return;

                stream = file.GetStream();
                //file.Dispose();

                RecipeImage.Source = ImageSource.FromStream(() => stream);

                PicTakenFile = file;

            };



        }
    }
    
}

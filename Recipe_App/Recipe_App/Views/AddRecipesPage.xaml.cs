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

using System.Collections.ObjectModel;
using Recipe_App.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Recipe_App
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipesPage : ContentPage
    {
        
       public static MediaFile PicTakenFile { get; set; }
       public string SelectedCategory { get; set; }
       public SQLentry passedSQLentry { get; set; }


        public AddRecipesPage()
        {
            InitializeComponent();
            //BindingContext = new AddRecipesPageViewModel();

            //update picker values with colletion
            foreach (var item in Application.Current.Properties)
            {
                txtcategory.Items.Add(item.Value.ToString());
            }
            


        }

        public AddRecipesPage(SQLentry sqlentry)
        {
            InitializeComponent();

            passedSQLentry = sqlentry;

            RecipeImage.Source = sqlentry.ImageFilePath;
            txtRecipeName.Text = sqlentry.RecipeName;
            txtcategory.SelectedItem = sqlentry.Category;
            txtIngredients.Text = sqlentry.Ingredients;
            txtRecipe.Text = sqlentry.Recipe;
            txtNotes.Text = sqlentry.Notes;

            //update picker values with colletion, first check if category exists already
            
            //if (!txtcategory.Items.Contains(sqlentry.Category) ||
            //    (!txtcategory.Items.Contains("Breakfast")) ||
            //    (!txtcategory.Items.Contains("Lunch")) ||
            //   ( !txtcategory.Items.Contains("Dinner")) ||
            //   ( !txtcategory.Items.Contains("Quick Bites")) ||
            //    (!txtcategory.Items.Contains("Salads")))
            //{
            //    txtcategory.Items.Add(sqlentry.Category);


            //}

            foreach (var item in Application.Current.Properties)
            {
                txtcategory.Items.Add(item.Value.ToString());
            }



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
                            //if (txtcategory.Text == null)
                            //{
                            //    //here we are saving in the properties dictionary all the categories the user inputs
                            //    entry.Category = "Uncategorized";
                            //    Application.Current.Properties["Uncategorized"] = "Uncategorized";
                            //}
                            //else
                            //{

                            //    entry.Category = txtcategory.Text;
                            //    Application.Current.Properties[$"{txtcategory.Text}"] = txtcategory.Text;

                            //}
                            entry.Category = txtcategory.SelectedItem.ToString();



                          

                            entry.Recipe = txtRecipe.Text;
                            entry.Notes = txtNotes.Text;
                            entry.Ingredients = txtIngredients.Text;

                            
                                if (PicTakenFile != null)
                                {
                                    entry.ImageFilePath = PicTakenFile.Path.ToString();
                                    PicTakenFile = null;
                                }
                                else 
                                {
                                    entry.ImageFilePath = "";
                                }

                            
                                   
                               




                            //entry.ImageFilePath = PicTakenFile.ToString();
                            int i = App.Database.SaveItem(entry);
                          

                            if (i > 0)
                            {
                              
                                await DisplayAlert("New Recipe", "Save successful", "OK");
                                await Navigation.PopToRootAsync();

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
                    else if (fileexist != null && passedSQLentry != null)
                    {
                        if (txtRecipeName.Text != null)
                        {
                            SQLentry entry2 = new SQLentry();
                            entry2.RecipeID = passedSQLentry.RecipeID;
                            if (PicTakenFile == null)
                            {
                                entry2.ImageFilePath = passedSQLentry.ImageFilePath;
                                passedSQLentry.ImageFilePath = null;
                            }
                            else
                            {
                                entry2.ImageFilePath = PicTakenFile.Path.ToString();
                                PicTakenFile = null;
                            }
                            entry2.Category = txtcategory.SelectedItem.ToString();
                            entry2.RecipeName = txtRecipeName.Text;
                            entry2.Ingredients = txtIngredients.Text;
                            entry2.Recipe = txtRecipe.Text;
                            entry2.Notes = txtNotes.Text;

                            int i = App.Database.SaveItem(entry2);


                            if (i > 0)
                            {

                                await DisplayAlert("Edit Recipe", "Save successful", "OK");
                                await Navigation.PopToRootAsync();

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



                    } else if (fileexist != null && passedSQLentry == null)
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
        








        //CAMERA STUFF AND IMAGE CHOOSE STUFF

        public async void CameraButtonClicked(object sender, EventArgs e)
        {
           

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            

            if (cameraStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera});
                cameraStatus = results[Permission.Camera];
                
            }

            
               
            
            


            if (cameraStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "MyRecipes",
                    Name = "test.jpg",
                    AllowCropping = true,
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 92,


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
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }





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
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                if (storageStatus != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] {Permission.Storage });
                    
                    storageStatus = results[Permission.Storage];
                }

                if (storageStatus == PermissionStatus.Granted)
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 92,

                    });

                    //.ConfigureAwait(true);


                    if (file == null)
                        return;

                    stream = file.GetStream();
                    

                    RecipeImage.Source = ImageSource.FromStream(() => stream);

                    PicTakenFile = file;
                    //file.Dispose();

                };
            }
                    



        }
    }
    
}

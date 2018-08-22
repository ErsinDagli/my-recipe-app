using System;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recipe_App.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Recipe_App.Model;
using System.Resources;

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
            //BindingContext = new Language();
            

            //update picker values with colletion
            foreach (var item in Application.Current.Properties)
            {
                txtcategory.Items.Add(item.Value.ToString());
            }
            
            

            if(MainPage.TurkishClicked == true)
            {
                addRecipeLabel.Text = Turkish.AddRecipe;
                recipeNameLabel.Text = Turkish.RecipeName;
                categoryLabel.Text = Turkish.Category;
                ingredientsLabel.Text = Turkish.Ingredients;
                recipeLabel.Text = Turkish.Recipe;
                notesLabel.Text = Turkish.Notes;
                SaveButton.Text = Turkish.SaveButton;



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

          

            foreach (var item in Application.Current.Properties)
            {
                txtcategory.Items.Add(item.Value.ToString());
            }



        }


        protected override void OnAppearing()
        {
            BackgroundImage = "AddRecipesPageBackground.png";
        }


        protected override void OnDisappearing()
        {
            BackgroundImage = null;
        }





        async void Click_Save(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Save Recipe", "Are you sure?", "Yes", "No");
            if (answer == true)
            {
                try
                {


                    if (txtRecipeName.Text != null)
                    {
                        SQLentry fileexist = App.Database.GetItem(txtRecipeName.Text.ToUpper());
                        if (fileexist == null)
                        {
                            if (txtRecipeName.Text != null)

                            {



                                SQLentry entry = new SQLentry();



                                entry.RecipeName = txtRecipeName.Text.ToUpper();

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
                                entry2.RecipeName = txtRecipeName.Text.ToUpper();
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



                        }
                        else if (fileexist != null && passedSQLentry == null)
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

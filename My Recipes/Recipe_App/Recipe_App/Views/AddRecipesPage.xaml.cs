using System;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recipe_App.ViewModels;
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
       public bool saved;


        public AddRecipesPage()
        {
            InitializeComponent();
            //BindingContext = new Language();
            

            //update picker values with colletion
            foreach (var item in Application.Current.Properties)
            {
                txtcategory.Items.Add(item.Value.ToString());
            }
            
            

            if(MainPage.TurkishClicked == false)
            {
                
                recipeNameLabel.Text = Language.RecipeNameEnglish;
                categoryLabel.Text = Language.CategoryEnglish;
                ingredientsLabel.Text = Language.IngredientsEnglish;
                recipeLabel.Text = Language.RecipeEnglish;
                notesLabel.Text = Language.NotesEnglish;
                SaveButton.Text = Language.SaveButtonEnglish;
                CameraButton.Text = Language.TakePhotoButtonEnglish;
                ChooseImage.Text = Language.ChooseImageButtonEnglish;


                txtcategory.Items.Add("Breakfast");
                txtcategory.Items.Add("Lunch");
                txtcategory.Items.Add("Dinner");
                txtcategory.Items.Add("Desserts");
                txtcategory.Items.Add("Quick Bites");

            }
            else
            {
                
                recipeNameLabel.Text = Language.RecipeNameTurkish;
                categoryLabel.Text = Language.CategoryTurkish;
                ingredientsLabel.Text = Language.IngredientsTurkish;
                recipeLabel.Text = Language.RecipeTurkish;
                notesLabel.Text = Language.NotesTurkish;
                SaveButton.Text = Language.SaveButtonTurkish;
                CameraButton.Text = Language.TakePhotoButtonTurkish;
                ChooseImage.Text = Language.ChooseImageButtonTurkish;

                txtcategory.Items.Add("Kahvaltı");
                txtcategory.Items.Add("Öğlen Yemeği");
                txtcategory.Items.Add("Akşam Yemegi");
                txtcategory.Items.Add("Tatlılar");
                txtcategory.Items.Add("Aperatifler");
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

            if (MainPage.TurkishClicked == false)
            {

                recipeNameLabel.Text = Language.RecipeNameEnglish;
                categoryLabel.Text = Language.CategoryEnglish;
                ingredientsLabel.Text = Language.IngredientsEnglish;
                recipeLabel.Text = Language.RecipeEnglish;
                notesLabel.Text = Language.NotesEnglish;
                SaveButton.Text = Language.SaveButtonEnglish;
                CameraButton.Text = Language.TakePhotoButtonEnglish;
                ChooseImage.Text = Language.ChooseImageButtonEnglish;

                txtcategory.Items.Add("Breakfast");
                txtcategory.Items.Add("Lunch");
                txtcategory.Items.Add("Dinner");
                txtcategory.Items.Add("Desserts");
                txtcategory.Items.Add("Quick Bites");
               




            }
            else
            {

                recipeNameLabel.Text = Language.RecipeNameTurkish;
                categoryLabel.Text = Language.CategoryTurkish;
                ingredientsLabel.Text = Language.IngredientsTurkish;
                recipeLabel.Text = Language.RecipeTurkish;
                notesLabel.Text = Language.NotesTurkish;
                SaveButton.Text = Language.SaveButtonTurkish;
                CameraButton.Text = Language.TakePhotoButtonTurkish;
                ChooseImage.Text = Language.ChooseImageButtonTurkish;

                txtcategory.Items.Add("Kahvaltı");
                txtcategory.Items.Add("Öğlen Yemeği");
                txtcategory.Items.Add("Akşam Yemegi");
                txtcategory.Items.Add("Tatlılar");
                txtcategory.Items.Add("Aperatifler");
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


        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            if (saved == false)
            {
                var answer = DisplayAlert("Oops!", "Exit without saving?", "Yes", "No");
                if (answer.Equals(true))
                {

                    Navigation.PopToRootAsync();
                    return false;

                }
                else
                {
                    return true;
                }
            }
            else
            {

                Navigation.PopToRootAsync();
                saved = false;
                return false;

            }
          

        }





        async void Click_Save(object sender, EventArgs e)
        {
            bool saved = true;

            bool answer;
            if (txtRecipeName.Text != null)
            {
                
                if (MainPage.TurkishClicked == false)
                {
                    answer = await DisplayAlert("Save Recipe", "Are you sure?", "Yes", "No");
                }
                else
                {
                    answer = await DisplayAlert("Yeni Tarif", "Kaydetmek ister misiniz?", "Evet", "Hayır");
                }
            }
            else
            {
                if (MainPage.TurkishClicked == false)
                {
                   await DisplayAlert("", "Recipe name and Category are required fields", "OK");
                    answer = false;
                }
                else
                {
                    await DisplayAlert("", "Lütfen Tarif ismini ve Kategoriyi doldurunuz", "OK");
                    answer = false;
                }
            }

    
            if (answer == true)
            {
                try
                {


                   
                        SQLentry fileexist = App.Database.GetItem(txtRecipeName.Text.ToUpper());
                        if (fileexist == null)
                        {
                            if (txtRecipeName.Text != null)

                            {



                                SQLentry entry = new SQLentry();



                                entry.RecipeName = txtRecipeName.Text.ToUpper();
                                
                    //we want to save each category only in english, even if user app is in turkish mode.
                                if (txtcategory.SelectedItem.ToString() == "Kahvaltı")
                                {
                                    entry.Category = "Breakfast";
                                }
                                else if (txtcategory.SelectedItem.ToString() == "Öğlen Yemeği")
                                {
                                    entry.Category = "Lunch";
                                }
                                else if (txtcategory.SelectedItem.ToString() == "Akşam Yemegi")
                                {
                                    entry.Category = "Dinner";
                                }
                                else if (txtcategory.SelectedItem.ToString() == "Aperatifler")
                                {
                                    entry.Category = "Quick Bites";
                                }
                                else if (txtcategory.SelectedItem.ToString() == "Tatlılar")
                                {
                                    entry.Category = "Desserts";
                                }
                                else 
                                {
                                    entry.Category = txtcategory.SelectedItem.ToString();
                                }


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
                                    if(MainPage.TurkishClicked == false)
                                    {
                                        await DisplayAlert("New Recipe", "Save successful!", "OK");
                                        await Navigation.PopToRootAsync();
                                    }
                                    else
                                    {
                                        await DisplayAlert("Yeni Tarif", "Kayıt Başarılı!", "OK");
                                        await Navigation.PopToRootAsync();
                                    }
                                    

                                }
                                else
                                {
                                    if(MainPage.TurkishClicked == false)
                                    {
                                        await DisplayAlert("Oops!", "Try Again", "OK");
                                    }
                                    else
                                    {
                                        await DisplayAlert("Oops!", "Tekrar Deneyiniz", "OK");
                                    }
                                    
                                }
                            }
                            else
                            {
                                if(MainPage.TurkishClicked == false)
                                {
                                    await DisplayAlert("", "Recipe name and Category are required fields", "OK");
                                }
                                else
                                {
                                    await DisplayAlert("", "Lütfen tarif ismini ve kategoriyi doldurunuz", "OK");
                                }
                               
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
                                    if(MainPage.TurkishClicked == false)
                                    {
                                        await DisplayAlert("Edit Recipe", "Save successful!", "OK");
                                        await Navigation.PopToRootAsync();
                                    }
                                    else
                                    {
                                        await DisplayAlert("Tarif düzenleme", "Başarılı!", "OK");
                                        await Navigation.PopToRootAsync();
                                    }
                                    

                                }
                                else
                                {
                                    if (MainPage.TurkishClicked == false)
                                    {
                                        await DisplayAlert("Oops!", "Try Again", "OK");
                                    }
                                    else
                                    {
                                        await DisplayAlert("Oops!", "Tekrar Deneyiniz", "OK");
                                    }
                                }
                            }
                            else
                            {
                                if (MainPage.TurkishClicked == false)
                                {
                                    await DisplayAlert("", "Recipe name and Category are required fields", "OK");
                                }
                                else
                                {
                                    await DisplayAlert("", "Lütfen tarif ismini ve kategoriyi doldurunuz", "OK");
                                }
                            }



                        }
                        else if (fileexist != null && passedSQLentry == null)
                        {
                            if(MainPage.TurkishClicked == false)
                            {
                                await DisplayAlert("Save Failed", "Recipe Already Exists ", "OK");
                                txtRecipeName.Text = "";
                            }
                            else
                            {
                                await DisplayAlert("Tarif Ekleme Başarısız oldu", "Lütfen Yeni Tarif Ekle", "OK");
                                txtRecipeName.Text = "";
                            }

                            


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
                    Name = "recipe.jpg",
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
                if(MainPage.TurkishClicked == false)
                {
                    await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                }
                else
                {
                    await DisplayAlert("Izin Verilmedi", "Lütfen izin veriniz", "OK");
                }
               
               
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

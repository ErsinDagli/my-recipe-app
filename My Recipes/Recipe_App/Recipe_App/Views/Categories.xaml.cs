using Recipe_App.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System.IO;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
using Newtonsoft.Json;
using System.Linq;

namespace Recipe_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Categories : ContentPage
	{
        public static string SelectedCategory;
        public static IList<SQLentry> categoryListEntries;
        public static ObservableCollection<SQLentry> CategoryOC;
        public MediaFile PicTakenFile { get; set; }
        int page = 1;
        async Task Initialise(string selectedcategory)
        {
            var category = await App.Database.GetCategoryByName(selectedcategory);
            if (string.IsNullOrEmpty(category.ImageFilePath))
            {
                CatImage.Source = "recipeplaceholder.png";

            }
            else
                CatImage.Source = category.ImageFilePath;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            page = 1;

        }

        async Task InitialiseTwo(string selectedcategory)
        {
            CategoryOC = new ObservableCollection<SQLentry>();
            categoryListEntries = await App.Database.GetCategory(selectedcategory, 1);

            foreach (var item in categoryListEntries)
            {
                if (!CategoryOC.Contains(item))
                {
                    if (string.IsNullOrWhiteSpace(item.ImageFilePath))
                        item.ImageFilePath = "recipeplaceholder.png";

                    CategoryOC.Add(item);
                }
            }


            categoriesList.ItemsSource = CategoryOC;
        }

        public Categories (string selectedcategory)
		{
			InitializeComponent ();

            if (selectedcategory == "Breakfast" ||
                selectedcategory == "Lunch" ||
                selectedcategory == "Dinner" ||
                selectedcategory == "Salads" ||
                selectedcategory == "Desserts" ||
                selectedcategory == "Quick Bites")
            {
                DeleteButton.IsEnabled = false;
                DeleteButton.IconImageSource = "";
            }

            if (selectedcategory == "Breakfast")
                CatImage.Source = "catBrekky.png";
            else if(selectedcategory == "Lunch")
                CatImage.Source = "catLunch.png";
            else if (selectedcategory == "Dinner")
                CatImage.Source = "catDinner.png";
            else if (selectedcategory == "Salads")
                CatImage.Source = "catSalad.png";
            else if (selectedcategory == "Quick Bites")
                CatImage.Source = "catBrekky.png";
            else if (selectedcategory == "Desserts")
                CatImage.Source = "catDessert.png";



           


            BindingContext = new SQLentry();

             SetUpNavBar();

            SelectedCategory = selectedcategory;

            //get category image
            try
            {
                Task.Run(async () => await Initialise(selectedcategory));

               
            }
            catch
            {

            }

            try
            {
                if (Application.Current.Properties.ContainsKey(SelectedCategory + "-color"))
                {
                    var colorrgba = Application.Current.Properties[SelectedCategory + "-color"].ToString();

                    var RGBA = colorrgba.Split(',');
                    var R = Convert.ToDouble(RGBA[0]);
                    var G = Convert.ToDouble(RGBA[1]);
                    var B = Convert.ToDouble(RGBA[2]);
                    var A = Convert.ToDouble(RGBA[3]);

                    catFrame.BackgroundColor = new Color(R, G, B, A);
                }
            }
            catch
            {

            }

           


            try
            {

                Task.Run(async () => await InitialiseTwo(selectedcategory));

            }
            catch
            {

            }
           
            //item source of list is the OC where only categories are the same as the selected category provided in constructor
           

            if(MainPage.TurkishClicked == false)
            {
                CategoryLabel.Text = SelectedCategory;
                if (selectedcategory == "Breakfast")
                    CategoryLabel.Text = Language.CategoryBreakfastEnglish;
                else if (selectedcategory == "Lunch")
                    CategoryLabel.Text = Language.CategoryLunchEnglish;
                else if (selectedcategory == "Dinner")
                    CategoryLabel.Text = Language.CategoryDinnerEnglish;
                else if (selectedcategory == "Salads")
                    CategoryLabel.Text = Language.CategorySaladsEN;
                else if (selectedcategory == "Quick Bites")
                    CategoryLabel.Text = Language.CategoryQuickBitesEN;
                else if (selectedcategory == "Desserts")
                    CategoryLabel.Text = Language.CategoryDessertsEN;
            }
            else
            {
                CategoryLabel.Text = SelectedCategory;

                if (selectedcategory == "Breakfast")
                    CategoryLabel.Text = Language.CategoryBreakfastTurkish;
                else if (selectedcategory == "Lunch")
                    CategoryLabel.Text = Language.CategoryLunchTurkish;
                else if (selectedcategory == "Dinner")
                    CategoryLabel.Text = Language.CategoryDinnerTurkish;
                else if (selectedcategory == "Salads")
                    CategoryLabel.Text = Language.CategorySaladsTR;
                else if (selectedcategory == "Quick Bites")
                    CategoryLabel.Text = Language.CategoryQuickBitesTR;
                else if (selectedcategory == "Desserts")
                    CategoryLabel.Text = Language.CategoryDessertsTR;

            }



            Title = SelectedCategory;


     

            try
            {
                if (Application.Current.Properties.ContainsKey($"{SelectedCategory}-color"))
                     catFrame.BackgroundColor = (Color)Application.Current.Properties[$"{SelectedCategory}-color"];
            }
            catch
            {

            }
          


        }

       // protected override void OnAppearing()
       // {
           // categoriesList.ItemsSource = CategoryOC;
       // }

        private async void categoriesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //a copy of the navigation technique in the recipessearchpage list

            SQLentry mychoice = e.SelectedItem as SQLentry;


            if (mychoice == null)
            {
                return;
            }
            else
            {
                //first line is making usre that after selecting listview item, and going back to this page, the selection doesnt remain checked
                ((ListView)sender).SelectedItem = null;
                await Navigation.PushAsync(new RecipeDisplayPage(mychoice));
            }
        }

        //delete category event


        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            bool answer;
            if (MainPage.TurkishClicked == false)
            {
                answer = await DisplayAlert("Delete this category", "Are you sure?", "Yes", "No");
            }
            else
            {
                answer = await DisplayAlert("Kategoriyi sil", "Emin Misiniz?", "Evet", "Hayır");
            }

            if (answer == true)
            {
                //delete category
                var cat = App.Database.GetCategoryByName(SelectedCategory);
                if (cat != null)
                {
                    await App.Database.DeleteCategory(cat.Id);

                    //send deleted event
                    MessagingCenter.Send<Categories, string>(this, "Deleted", SelectedCategory);


                    await Navigation.PopAsync();
                }


            }
        }


         void SetUpNavBar()
        {
            try
            {
                var frame = new Frame()
                {
                    CornerRadius = 10,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    IsClippedToBounds = true,
                    Padding = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.LightBlue

                };

                SearchBar sb = new SearchBar();
                sb.HorizontalOptions = LayoutOptions.FillAndExpand;
                sb.VerticalOptions = LayoutOptions.FillAndExpand;
                sb.BackgroundColor = Color.Transparent;
                sb.TextChanged +=  searchbar_TextChanged;
                sb.HeightRequest = 40;
                sb.Behaviors.Add(new TextChangedBehavior());
                sb.Placeholder = "Recipe name";
                 frame.Content = sb;

                StackLayout chatNavBarLayout = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
                 chatNavBarLayout.Children.Add(frame);
               // chatNavBarLayout.Children.Add(new Label() { Text = "test" });

                NavigationPage.SetTitleView(this, chatNavBarLayout);
            }
            catch (Exception e)
            {

            }


           

        }
        private async void searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {


            categoriesList.ItemsSource = await App.Database.SearchRecipeInCategory(e.NewTextValue.ToUpper(), SelectedCategory);


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
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
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




                CatImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;


                });


                //save image for category
                var currentCategory = await App.Database.GetCategoryByName(SelectedCategory);
                if(currentCategory != null)
                {
                    currentCategory.ImageFilePath = file.Path.ToString();
                }
               

                await App.Database.EditCategory(currentCategory);

               // MessagingCenter.Send<Categories, string>(this, "PicUpdated", SelectedCategory);


                file.Dispose();

            }
            else
            {
                if (MainPage.TurkishClicked == false)
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
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });

                    storageStatus = results[Permission.Storage];
                }

                if (storageStatus == PermissionStatus.Granted)
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 92,

                    });

                   // .ConfigureAwait(true);


                    if (file == null)
                        return;

                    stream = file.GetStream();


                    CatImage.Source = ImageSource.FromStream(() => stream);

                    //save image for category
                    var currentCategory = await App.Database.GetCategoryByName(SelectedCategory);
                    if (currentCategory != null)
                    {
                        currentCategory.ImageFilePath = file.Path.ToString();
                    }

                    await App.Database.EditCategory(currentCategory);


                    PicTakenFile = file;

                   // MessagingCenter.Send<Categories, string>(this, "PicUpdated", SelectedCategory);


                    file.Dispose();

                };
            }




        }


        public static Color GetRandomColor()
        {
            Random rand = new Random();
            var color = Color.FromHsla(rand.NextDouble(), rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            return color;

        }


        private async void Colorbutton_Clicked(object sender, EventArgs e)
        {
            catFrame.BackgroundColor = GetRandomColor();

            var buttonHexColor = catFrame.BackgroundColor.ToHex();

            //  App.Database.UpdateCategoryColor(SelectedCategory, buttonHexColor);
            Application.Current.Properties[$"{SelectedCategory}-color"] = catFrame.BackgroundColor.R + "," +
                                                                          catFrame.BackgroundColor.G + "," +
                                                                           catFrame.BackgroundColor.B + "," +
                                                                            catFrame.BackgroundColor.A;

           await Application.Current.SavePropertiesAsync();

        }

        private async void CategoriesList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {


                if (CategoryOC.Count < 10)
                    return;
                var recipeList = sender as ListView;
                var recipeListLast = CategoryOC.LastOrDefault();

                if (e.Item == null)
                {
                    return;
                }


                if (((SQLentry)e.Item).RecipeName == recipeListLast.RecipeName)
                {
                    page++;

                    loadercat.IsRunning = true;
                    loadercat.IsVisible = true;
                    var recipes = await App.Database.GetCategory(SelectedCategory, page);

                    foreach (var rec in recipes)
                    {
                        if (string.IsNullOrWhiteSpace(rec.ImageFilePath))
                        {
                            rec.ImageFilePath = "recipeplaceholder.pnhg";

                        }
                        if (CategoryOC.Where(x => x.RecipeName == rec.RecipeName).FirstOrDefault() == null)
                            CategoryOC.Add(rec);
                    }
                    categoriesList.ItemsSource = CategoryOC;


                   


                }
            }
            catch (Exception f)
            {

            }
            finally
            {
                loadercat.IsRunning = false;
                loadercat.IsVisible = false;
            }
        }
    }
}
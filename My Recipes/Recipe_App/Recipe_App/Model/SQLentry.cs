using SQLite;


namespace Recipe_App.ViewModels
{
    public class SQLentry 
    {
        private object database;

        [PrimaryKey, AutoIncrement]
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string Recipe { get; set; }
        public string Notes { get; set; }
        public string ImageFilePath { get; set; }
        
 
    }


    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageFilePath { get; set;  }
    }
}

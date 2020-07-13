using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;

namespace Recipe_App.ViewModels
{
    public class SQLHelper
    {
        static object locker = new object();
        SQLiteConnection database;
        

        //CONSTRUCTOR
        public SQLHelper()
        {
            database = GetConnection();
            // create the tables  
            database.CreateTable<SQLentry>();

            database.CreateTable<Category>();

        }

        /// <summary>
        /// CATEGORY methods
        /// </summary>
        /// <returns></returns>
        /// 

        public Category GetCategoryByName(string CategoryName)
        {
            lock (locker)
            {
                return database.Table<Category>().FirstOrDefault(x => x.CategoryName == CategoryName);
            }
        }

        public int EditCategory(Category cat)
        {
            lock (locker)
            {

                //Update Item  
                
                return database.Update(cat);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            lock (locker)
            {
                return (from i in database.Table<Category>() select i).OrderBy(x => x.CategoryName).ToList();
            }
        }

        public int GetCountRecipesInCategory(string categoryname)
        {
            lock (locker)
            {
                return (from i in database.Table<SQLentry>().Where(x=> x.Category.ToLower() == categoryname.ToLower()) select i).Count();
            }
        }
        public int SaveCategory(Category item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    //Update Item  
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    //Insert item  
                    //check if category exists before insert
                    if (GetCategoryByName(item.CategoryName) == null)
                        return database.Insert(item);
                    else
                        return 0;
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            lock (locker)
            {
                database.Delete<Category>(categoryId);
            }
        }


        /// <summary>
        /// RECIPE METHODS
        /// </summary>
        /// <returns></returns>

        public IEnumerable<SQLentry> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<SQLentry>() select i).ToList();
            }
        }
        public SQLentry GetItem(string RecipeName)
        {
            lock (locker)
            {
                return database.Table<SQLentry>().FirstOrDefault(x => x.RecipeName == RecipeName);
            }
        }

        public List<SQLentry> ListRecipes(string key)
        {

            return database.Table<SQLentry>().Where(e => e.RecipeName.Contains(key)).OrderBy(c => c.RecipeName).ToList();
            

        }
        public List<SQLentry> SearchRecipeInCategory(string recipeName, string category)
        {

            return database.Table<SQLentry>().Where(x=> x.Category == category).Where(e => e.RecipeName.Contains(recipeName)).OrderBy(c => c.RecipeName).ToList();


        }
        

        public List<SQLentry> GetCategory(string category)
        {
            return database.Table<SQLentry>().Where(c => c.Category == category).ToList();
        }
        
        public int UpdateCategoryColor(string categoryName, string ColorHex)
        {
            lock (locker)
            {
                var category = database.Table<Category>().Where(x => x.CategoryName == categoryName).FirstOrDefault();

                if (category != null)
                {
                    category.ButtonColorHex = ColorHex;
                    //Update Item  

                    return database.Update(category);
                }

                return 0;
               
            }
        }


        public int SaveItem(SQLentry item)
        {
            lock (locker)
            {
                if (item.RecipeID != 0)
                {
                    //Update Item  
                    database.Update(item);
                    return item.RecipeID;
                }
                else
                {
                    //Insert item  
                    if (GetItem(item.RecipeName) == null)
                        return database.Insert(item);
                    else
                        return 0;
                   
                }
            }
        }

        public void DeleteItem(int RecipeID)
        {
            lock (locker)
            {

                 database.Delete<SQLentry>(RecipeID);
            }
        }



        public SQLite.SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlitConnection;
            var sqliteFilename = "Recipes.db3";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLite.SQLiteConnection(path);
            return sqlitConnection;
        }




      
       


    }
}

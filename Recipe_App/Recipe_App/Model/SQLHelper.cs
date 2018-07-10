using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;
using Recipe_App.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
           
            
        }

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

        public List<SQLentry> GetCategory(string category)
        {
            return database.Table<SQLentry>().Where(c => c.Category.Contains(category)).ToList();
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
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int RecipeID)
        {
            lock (locker)
            {
                return database.Delete<SQLentry>(RecipeID);
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

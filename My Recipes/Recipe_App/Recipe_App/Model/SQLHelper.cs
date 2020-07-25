using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;
using System.Threading.Tasks;

namespace Recipe_App.ViewModels
{
    public class SQLHelper
    {
        int pageSize = 10;
           
        static object locker = new object();
        SQLiteAsyncConnection database;
        

        //CONSTRUCTOR
        public SQLHelper()
        {
            database = GetConnection();
            // create the tables  
            database.CreateTableAsync<SQLentry>();

            database.CreateTableAsync<Category>();

        }

        

        /// <summary>
        /// CATEGORY methods
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<Category> GetCategoryByName(string CategoryName)
        {
            
            return await database.Table<Category>().FirstOrDefaultAsync(x => x.CategoryName == CategoryName);
            
        }

        public async Task<int> EditCategory(Category cat)
        {
           
                
                return await database.UpdateAsync(cat);
           
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
           
               

                return  await database.Table<Category>().OrderBy(x => x.CategoryName).ToListAsync();
         
        }

        public async Task<int> GetCountRecipesInCategory(string categoryname)
        {
          
                return await database.Table<SQLentry>().Where(x=> x.Category.ToLower() == categoryname.ToLower()).CountAsync();
          
        }
        public async Task<int> SaveCategory(Category item)
        {
           
                if (item.Id != 0)
                {
                    //Update Item  
                    await database.UpdateAsync(item);
                    return item.Id;
                }
                else
                {
                    //Insert item  
                    //check if category exists before insert
                    if (await GetCategoryByName(item.CategoryName) == null)
                        return await database.InsertAsync(item);
                    else
                        return 0;
                }
           
        }

        public async Task DeleteCategory(int categoryId)
        {
           
                await database.DeleteAsync<Category>(categoryId);
         
        }


        /// <summary>
        /// RECIPE METHODS
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<SQLentry>> GetItems(int page)
        {
          
                var total = await database.Table<SQLentry>().CountAsync();
                var skip = pageSize * (page - 1);
                var canPage = skip < total;
                if (!canPage) // do what you wish if you can page no further
                    return null;

                return await  database.Table<SQLentry>().Skip(skip).Take(pageSize).ToListAsync();
            
        }
        public async Task<SQLentry> GetItem(string RecipeName)
        {
            
                return await database.Table<SQLentry>().FirstOrDefaultAsync(x => x.RecipeName == RecipeName);
           
        }

        public async Task<List<SQLentry>> ListRecipes(string key, int page)
        {
            var total = await database.Table<SQLentry>().CountAsync();
            var skip = pageSize * (page - 1);
            var canPage = skip < total;
            if (!canPage) // do what you wish if you can page no further
                return null;

            return await database.Table<SQLentry>().Where(e => e.RecipeName.Contains(key)).OrderBy(c => c.RecipeName).Skip(skip).Take(pageSize).ToListAsync();
            

        }
        public async Task<List<SQLentry>> SearchRecipeInCategory(string recipeName, string category)
        {

            return await database.Table<SQLentry>().Where(x=> x.Category == category).Where(e => e.RecipeName.Contains(recipeName)).OrderBy(c => c.RecipeName).ToListAsync();


        }
        

        public async Task<List<SQLentry>> GetCategory(string category, int page)
        {
            var total = await database.Table<SQLentry>().CountAsync();
            var skip = pageSize * (page - 1);
            var canPage = skip < total;
            if (!canPage) // do what you wish if you can page no further
                return null;

            return await database.Table<SQLentry>().Where(c => c.Category == category).OrderBy(x=> x.RecipeName).Skip(skip).Take(pageSize).ToListAsync();
        }
        
        public async Task<int> UpdateCategoryColor(string categoryName, string ColorHex)
        {
           
                var category = await database.Table<Category>().Where(x => x.CategoryName == categoryName).FirstOrDefaultAsync();

                if (category != null)
                {
                    category.ButtonColorHex = ColorHex;
                    //Update Item  

                    return await database.UpdateAsync(category);
                }

                return  0;
               
           
        }


        public async Task<int> SaveItem(SQLentry item)
        {
           
                if (item.RecipeID != 0)
                {
                    //Update Item  
                    await database.UpdateAsync(item);
                    return item.RecipeID;
                }
                else
                {
                    //Insert item  
                    if (await GetItem(item.RecipeName) == null)
                        return await database.InsertAsync(item);
                    else
                        return 0;
                   
                }
           
        }

        public async Task DeleteItem(int RecipeID)
        {
           

               await  database.DeleteAsync<SQLentry>(RecipeID);
          
        }



        public SQLiteAsyncConnection GetConnection()
        {
            SQLiteAsyncConnection sqlitConnection;
            var sqliteFilename = "Recipes.db3";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLiteAsyncConnection(path);
            return sqlitConnection;
        }




      
       


    }
}

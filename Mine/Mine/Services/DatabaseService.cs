using System;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Mine.Models;
using System.Collections.Generic;

namespace Mine.Services 
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Add Item to the database
        /// </summary>
        /// <param name="item">Item to be inserted</param>
        /// <returns>True on successful insertion</returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            if (item == null)
            {
                return false;
            }

            var result = await Database.InsertAsync(item);
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Task<bool> UpdateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve an Item from the database
        /// </summary>
        /// <param name="id">ID of item to be retrieved</param>
        /// <returns>ItemModel</returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            // use Linq to find the record that matches Database ID with ItemModel Table
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));
            return result;
        }

        /// <summary>
        /// Retrieve a list of Items from the database 
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}

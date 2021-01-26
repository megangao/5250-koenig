using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Fishing Bait", Description="Used to attract fish.", Value=3 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Breakfast", Description="Increases HP by 1.", Value=1 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Crystal Ball", Description="Reveals the entire map.", Value=8 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Yum Heart", Description="Restore a full heart.", Value=1 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Guppy's Head", Description="Spawns 4 blue flies.", Value=4 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "A Dime", Description="Gives you 10 coins.", Value=10 }
            };
        }

        public async Task<bool> CreateAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
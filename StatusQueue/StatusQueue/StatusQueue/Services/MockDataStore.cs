using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StatusQueue.Models;

namespace StatusQueue.Services
{
	public class MockDataStore : IDataStore<PostOffice>
	{
		bool isInitialized;
		List<PostOffice> items;

		public async Task<bool> AddItemAsync(PostOffice item)
		{
			await InitializeAsync();

			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(PostOffice item)
		{
			await InitializeAsync();

			var _item = items.Where((PostOffice arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(PostOffice item)
		{
			await InitializeAsync();

			var _item = items.Where((PostOffice arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);

			return await Task.FromResult(true);
		}

		public async Task<PostOffice> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<PostOffice>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(items);
		}

		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}


		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			items = new List<PostOffice>();
            var _items = new List<PostOffice>();
			//{
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
			//	new PostOffice { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
			//};

			foreach (PostOffice item in _items)
			{
				items.Add(item);
			}

			isInitialized = true;
		}
	}
}

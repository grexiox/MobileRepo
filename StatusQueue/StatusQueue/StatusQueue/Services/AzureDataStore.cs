﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using StatusQueue.Helpers;
using StatusQueue.Models;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace StatusQueue.Services
{
	public class AzureDataStore : AzureDataStoreBase, IDataStore<PostOffice>
	{

        public MobileServiceAuthenticationProvider AuthProvider => MobileServiceAuthenticationProvider.Facebook;


        IMobileServiceSyncTable<PostOffice> itemsTable;



		public async Task<IEnumerable<PostOffice>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeAsync();
			if (forceRefresh)
				await PullLatestAsync();

			return await itemsTable.ToEnumerableAsync();
		}

		public async Task<PostOffice> GetItemAsync(string id)
		{
			await InitializeAsync();
			await PullLatestAsync();
			var items = await itemsTable.Where(s => s.Id == id).ToListAsync();

			if (items == null || items.Count == 0)
				return null;

			return items[0];
		}

		public async Task<bool> AddItemAsync(PostOffice item)
		{
			await InitializeAsync();
			await PullLatestAsync();
			await itemsTable.InsertAsync(item);
			await SyncAsync();

			return true;
		}

		public async Task<bool> UpdateItemAsync(PostOffice item)
		{
			await InitializeAsync();
			await itemsTable.UpdateAsync(item);
			await SyncAsync();

			return true;
		}

		public async Task<bool> DeleteItemAsync(PostOffice item)
		{
			await InitializeAsync();
			await PullLatestAsync();
			await itemsTable.DeleteAsync(item);
			await SyncAsync();

			return true;
		}

		public async Task InitializeAsync()
		{
            if (isInitialized)
                return;

            Initialize();
            var store = new MobileServiceSQLiteStore("app.db");
			store.DefineTable<PostOffice>();
			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
			itemsTable = MobileService.GetSyncTable<PostOffice>();

			isInitialized = true;
		}

		public async Task<bool> PullLatestAsync()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				Debug.WriteLine("Unable to pull items, we are offline");
				return false;
			}
			try
			{
                await itemsTable.PullAsync($"all{typeof(PostOffice).Name}", itemsTable.CreateQuery());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to pull items, that is alright as we have offline capabilities: " + ex);
				return false;
			}
			return true;
		}

		public async Task<bool> SyncAsync()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				Debug.WriteLine("Unable to sync items, we are offline");
				return false;
			}
			try
			{
				await MobileService.SyncContext.PushAsync();
				if (!(await PullLatestAsync().ConfigureAwait(false)))
					return false;
			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult == null)
				{
					Debug.WriteLine("Unable to sync, that is alright as we have offline capabilities: " + exc);
					return false;
				}
				foreach (var error in exc.PushResult.Errors)
				{
					if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
					{
						//Update failed, reverting to server's copy.
						await error.CancelAndUpdateItemAsync(error.Result);
					}
					else
					{
						// Discard local change.
						await error.CancelAndDiscardItemAsync();
					}

					Debug.WriteLine(@"Error executing sync operation. PostOffice: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
				return false;
			}

			return true;
		}
	}
}
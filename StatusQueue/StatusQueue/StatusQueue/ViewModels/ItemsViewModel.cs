﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

using StatusQueue.Helpers;
using StatusQueue.Models;
using StatusQueue.Views;

using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<PostOffice> Items { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Title = "Browse";
			Items = new ObservableRangeCollection<PostOffice>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, PostOffice>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as PostOffice;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				Items.ReplaceRange(items);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
using System;

using StatusQueue.Models;

using Xamarin.Forms;

namespace StatusQueue.Views
{
	public partial class NewItemPage : ContentPage
	{
		public PostOffice Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

			Item = new PostOffice
			{
				City = "PostOffice name",
				State = "This is a nice description"
			};

			BindingContext = this;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddItem", Item);
			await Navigation.PopToRootAsync();
		}
	}
}
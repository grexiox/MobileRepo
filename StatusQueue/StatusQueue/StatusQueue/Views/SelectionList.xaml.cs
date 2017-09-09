using StatusQueue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatusQueue.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectionList : ContentPage
	{
		public SelectionList ()
		{
			InitializeComponent ();
		}

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new MainScreen();
            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as SelectionListViewModel;
            viewModel.LoadPostOfficeItemsCommand.Execute(null);
        }

        void OnSearchTextChanged(object sender, EventArgs args)
        {
            var viewModel = BindingContext as SelectionListViewModel;
            viewModel.SearchCommand.Execute(null);
        }
    }
}
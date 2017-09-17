﻿using StatusQueue.ViewModels;
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
	public partial class MainScreen : ContentPage
	{
		public MainScreen ()
		{
			InitializeComponent ();
		}
        void OnPostSelectButton(object sender, EventArgs args)
        {
            Application.Current.MainPage = new SelectionList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as MainScreenViewModel;
            Task.Run(async () =>
            {
                 await viewModel.LoadData();
            }
            );
            
        }
    }
}
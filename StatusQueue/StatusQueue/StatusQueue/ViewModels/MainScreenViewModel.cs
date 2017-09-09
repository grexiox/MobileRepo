using StatusQueue.Models;
using StatusQueue.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        public ICommand SelectedPost { get; private set; }
        public MainScreenViewModel()
        {
            SelectedPostOffices = new List<PostOffice>();
            SelectedPostOffices.Add(new PostOffice { PostalCode = "pp", OpeningHours = "tt", Street = "str" });
            SelectedPostOffices.Add(new PostOffice { PostalCode = "pp", OpeningHours = "tt", Street = "str" });
            //SelectedPost = new Command(ButtonClicked);
        }

        public IList<PostOffice> SelectedPostOffices { get; set; }

      

    }
}

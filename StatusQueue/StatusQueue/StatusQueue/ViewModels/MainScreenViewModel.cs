using StatusQueue.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusQueue.ViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        public MainScreenViewModel()
        {
            SelectedPostOffices = new List<PostOffice>();
            SelectedPostOffices.Add(new PostOffice { PostalCode = "pp", OpeningHours = "tt", Street = "str" });
        }
        public IList<PostOffice> SelectedPostOffices { get; set; }
    }
}

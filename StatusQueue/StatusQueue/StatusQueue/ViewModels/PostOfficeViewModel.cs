using StatusQueue.Helpers;
using StatusQueue.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
    public class PostOfficeViewModel
    {
        public Command SelectPostCommand { get; set; }
        public PostOfficeViewModel()
        {
            SelectPostCommand = new Command(async () => ExecuteSelectPostCommand());
        }

        private void ExecuteSelectPostCommand()
        {
            DataKeeper.SaveSelectedPost(Id);
            Application.Current.MainPage = new MainScreen();
        }

        public string Id { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string OpeningHours { get; set; }
        public string City { get; set; }
        public int IsAvailable { get; set; }
    }
}

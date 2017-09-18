using AutoMapper;
using StatusQueue.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
    public class SelectedPostOfficeViewModel:BaseViewModel
    {
        public static IJsonService JsonService => DependencyService.Get<IJsonService>();
        public Command RefreshPostCommand { get; set; }
        static MapperConfiguration config;
        public SelectedPostOfficeViewModel()
        {
            RefreshPostCommand = new Command(async () => await RefreshData());
        }
        public string Id { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string OpeningHours { get; set; }
        public string City { get; set; }
        public int StatusQueue { get; set; }
        public int ExpectedTime { get; set; }
        public bool Tendency { get; set; }
        public bool Result { get; set; }
        public string Info { get; set; }

        public async Task RefreshData()
        {
            var data = await JsonService.GetDataForAPost(Id);
            if(!data.Result)
            {
                //Refresh Error
            }
            StatusQueue = data.StatusQueue;
            OnPropertyChanged(nameof(StatusQueue));
            ExpectedTime = data.ExpectedTime;
            OnPropertyChanged(nameof(ExpectedTime));
            Tendency = data.Tendency;
            OnPropertyChanged(nameof(Tendency));

        }
    }
}

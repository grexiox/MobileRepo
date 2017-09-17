using AutoMapper;
using StatusQueue.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
    public class SelectedPostOfficeViewModel:BaseViewModel
    {
        public static IJsonService JsonService => DependencyService.Get<IJsonService>();

        static MapperConfiguration config;
        public SelectedPostOfficeViewModel()
        {

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

        public async void RefreshData()
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

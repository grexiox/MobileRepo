using AutoMapper;
using StatusQueue.Helpers;
using StatusQueue.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatusQueue.ViewModels
{
    public class SelectionListViewModel : BaseViewModel
    {
        static MapperConfiguration config;
        bool searchedMode = false;
        public Command LoadPostOfficeItemsCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command TextChangedCommand { get; set; }
        public string SearchedText { get; set; }
        public ObservableRangeCollection<PostOfficeViewModel> PostOffices { get; set; }
        public SelectionListViewModel()
        {
            PostOffices = new ObservableRangeCollection<PostOfficeViewModel>();
            if (config == null)
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<PostOffice, PostOfficeViewModel>());
            }
            LoadPostOfficeItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SearchCommand = new Command(() =>ExecuteSearchCommand());
            TextChangedCommand = new Command(() => ExecuteSearchCommand());
        }

        private void ExecuteSearchCommand()
        {
            if(!string.IsNullOrEmpty(SearchedText))
            {
                var temp = _orginalList.Where(p => p.City.Contains(SearchedText, StringComparison.OrdinalIgnoreCase)
                || p.PostalCode.Contains(SearchedText, StringComparison.OrdinalIgnoreCase)
                || p.Street.Contains(SearchedText, StringComparison.OrdinalIgnoreCase));
                searchedMode = true;
                PostOffices.ReplaceRange(temp);
            }
            else
            {
                if(searchedMode)
                {
                    searchedMode = false;
                    PostOffices.ReplaceRange(_orginalList);
                }
            }
        }

        IEnumerable<PostOfficeViewModel> _orginalList;
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                PostOffices.Clear();
                var listOffice = await DataStore.GetItemsAsync(true);
                var mapper = config.CreateMapper();
                _orginalList = listOffice.Select(p => mapper.Map<PostOfficeViewModel>(p)).ToList().OrderByDescending(p => p.IsAvailable);
                _orginalList = TestPurpose(_orginalList);
                PostOffices.ReplaceRange(_orginalList);
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

        private IEnumerable<PostOfficeViewModel> TestPurpose(IEnumerable<PostOfficeViewModel> orginalList)
        {
            _orginalList.FirstOrDefault(p => p.PostalCode == "34-332").IsAvailable = 1;
            _orginalList.FirstOrDefault(p => p.PostalCode == "30-091").IsAvailable = 1;
            return _orginalList.OrderByDescending(p => p.IsAvailable);
        }
    }
}

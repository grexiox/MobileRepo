using AutoMapper;
using StatusQueue.Helpers;
using StatusQueue.Models;
using StatusQueue.Services;
using StatusQueue.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace StatusQueue.ViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        static MapperConfiguration _config;
        MapperConfiguration config
        {
            get
            {
                if(_config==null)
                {
                    _config = new MapperConfiguration(cfg => cfg.CreateMap<PostOffice, SelectedPostOfficeViewModel>());
                }
                return _config;
            }
        }
        IMapper Mapper
        {
            get
            {
                return config.CreateMapper();
            }
        }
        public ICommand SelectedPost { get; private set; }
        public MainScreenViewModel()
        {
            SelectedPostOffices = new ObservableRangeCollection<SelectedPostOfficeViewModel>();
            
        }

        public ObservableRangeCollection<SelectedPostOfficeViewModel> SelectedPostOffices { get; set; }

      public async Task LoadData()
        {
            var selectedPostId = DataKeeper.LoadSelectedPost();
            if (!string.IsNullOrWhiteSpace(selectedPostId))
            {
                var selected = await DataStore.GetItemAsync(selectedPostId);
                var item = Mapper.Map<SelectedPostOfficeViewModel>(selected);
                SelectedPostOffices.Add(item);
                OnPropertyChanged(nameof(SelectedPostOffices));
                SelectedPostOffices.ToList().ForEach(i => i.RefreshData());
            }
        }
    }
}

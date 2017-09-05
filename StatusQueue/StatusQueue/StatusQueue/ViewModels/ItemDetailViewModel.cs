using StatusQueue.Models;

namespace StatusQueue.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public PostOffice Item { get; set; }
		public ItemDetailViewModel(PostOffice item = null)
		{
			Title = item.City;
			Item = item;
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}
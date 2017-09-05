namespace StatusQueue.Models
{
    public class PostOffice : BaseDataObject
	{
        string state = string.Empty;
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        string postalCode = string.Empty;
        public string PostalCode
        {
            get { return postalCode; }
            set { SetProperty(ref postalCode, value); }
        }
        string street = string.Empty;
        public string Street
        {
            get { return street; }
            set { SetProperty(ref street, value); }
        }
        string openingHours = string.Empty;
        public string OpeningHours
        {
            get { return openingHours; }
            set { SetProperty(ref openingHours, value); }
        }
        string city = string.Empty;
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }
        string url = string.Empty;
        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }
    }
}

using System.Collections.Generic;

using StatusQueue.Helpers;
using StatusQueue.Services;
using StatusQueue.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StatusQueue
{
	public partial class App : Application
	{
        //MUST use HTTPS, neglecting to do so will result in runtime errors on iOS
		public static bool AzureNeedsSetup => AzureMobileAppUrl == "https://CONFIGURE-THIS-URL.azurewebsites.net";
		public static string AzureMobileAppUrl = "http://backendhill.azurewebsites.net";
        public static IDictionary<string, string> LoginParameters => null;

        public App()
		{
			InitializeComponent();
            //needed do not execute login operation
            Settings.UserId = "not empty";
			if (AzureNeedsSetup)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<AzureDataStore>();

			SetMainPage();
		}

		public static void SetMainPage()
		{
            if (!AzureNeedsSetup && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
		}

        public static void GoToMainPage()
        {
            Current.MainPage = new MainScreen();
            /*
            Current.MainPage = new TabbedPage
			{
				Children =
				{
					new NavigationPage(new ItemsPage())
					{
						Title = "Browse",
						Icon = Device.OnPlatform("tab_feed.png",null,null)
					},
					new NavigationPage(new AboutPage())
					{
						Title = "About",
						Icon = Device.OnPlatform("tab_about.png",null,null)
					},
				}
			};*/
        }
	}
}

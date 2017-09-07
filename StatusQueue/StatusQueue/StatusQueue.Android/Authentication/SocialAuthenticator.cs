using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StatusQueue.Services;

using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

[assembly: Dependency(typeof(StatusQueue.Droid.Authentication.SocialAuthenticator))]
namespace StatusQueue.Droid.Authentication
{
    public class SocialAuthenticator : BaseSocialAuthenticator
    {
        public override async Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {

                return await client.LoginAsync(provider, new JObject());

            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
            }

            return null;
        }

        public override void ClearCookies()
        {
            try
            {
                if ((int)global::Android.OS.Build.VERSION.SdkInt >= 21)
                    global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
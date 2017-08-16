using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StatusQueue.Services;

using Microsoft.WindowsAzure.MobileServices;

[assembly: Xamarin.Forms.Dependency(typeof(StatusQueue.UWP.Authentication.SocialAuthenticator))]
namespace StatusQueue.UWP.Authentication
{
    public class SocialAuthenticator : BaseSocialAuthenticator
    {
        public override async Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return await client.LoginAsync(provider, parameters);
            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
            }

            return null;
        }
    }
}
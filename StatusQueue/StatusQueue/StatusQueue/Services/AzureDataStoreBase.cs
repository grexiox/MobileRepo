using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using StatusQueue.Helpers;
using StatusQueue.Models;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace StatusQueue.Services
{
    public abstract class AzureDataStoreBase
    {
        protected bool isInitialized;
        public bool UseAuthentication => true;
        public MobileServiceClient MobileService { get; set; }
        public void Initialize()
        {
           
            DelegatingHandler handler = null;

            if (UseAuthentication)
                handler = new CustomApiAuthenticator();

            MobileService = new MobileServiceClient(App.AzureMobileAppUrl, handler)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings
                {
                    CamelCasePropertyNames = true
                }
            };

            if (UseAuthentication && !string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }
        }
    }
}
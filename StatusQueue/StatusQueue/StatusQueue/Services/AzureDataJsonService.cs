using System.Collections.Generic;
using System.Threading.Tasks;
using StatusQueue.Models.JsonObject;
using System.Net.Http;

namespace StatusQueue.Services
{
    class AzureDataJsonService : AzureDataStoreBase ,IJsonService
    {
        public async Task<PostValue> GetDataForAPost(string postId)
        {
            Initial();
            var parameters = new Dictionary<string, string>();
            parameters.Add("PostId", postId);
            var retvalue = await MobileService.InvokeApiAsync<PostValue>("Post", HttpMethod.Get, parameters);
            return retvalue;
        }

        private void Initial()
        {
            if (!isInitialized)
            {
                Initialize();
                isInitialized = true;
            }
        }
    }
}

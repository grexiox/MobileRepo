using StatusQueue.Models.JsonObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StatusQueue.Services
{
    public interface IJsonService
    {
         Task<PostValue> GetDataForAPost(string id);
    }
}

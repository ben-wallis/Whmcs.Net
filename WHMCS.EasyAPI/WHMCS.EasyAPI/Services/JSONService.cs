using Newtonsoft.Json;

namespace Whmcs.Services
{
    public class JSONService : IJSONService
    {
        public T DeserialiseJSON<T>(string inputJSON)
        {
            return JsonConvert.DeserializeObject<T>(inputJSON);
        }
    }
}

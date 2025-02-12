using Newtonsoft.Json;

namespace CVSTS_FE
{
    public class ODataResponse<T>
    {
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}

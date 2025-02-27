using Newtonsoft.Json;

namespace HomeDoctor.Models
{
    public class Wards
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("codename")]
        public string Codename { get; set; }
        [JsonProperty("division_type")]
        public string DivisionType { get; set; }
        [JsonProperty("district_code")]
        public string DistrictCode { get; set; }
    }
}

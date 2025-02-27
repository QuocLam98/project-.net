using Newtonsoft.Json;

namespace HomeDoctor.Models
{
    public class Districts
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("codename")]
        public string Codename { get; set; }
        [JsonProperty("division_type")]
        public string DivisionType { get; set; }
        [JsonProperty("province_code")]
        public int ProvinceCode { get; set; }
        [JsonProperty("wards")]
        public List<Wards> Wards { get; set; }
    }
}

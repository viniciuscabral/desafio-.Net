using Newtonsoft.Json;

namespace WsTestes.Model
{
    public class Phone
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Number { get; set; }
        public int Area_code { get; set; }
        public string country_code { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
       

    }
}

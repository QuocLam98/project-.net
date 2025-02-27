namespace HomeDoctorSolution.Models
{
    public class ChatGptDTO
    {
        public string @object  { get; set; }
        public Data[] data { get; set; }
        public string frist_id { get; set; }
        public string last_id { get; set; }
        public bool has_more { get; set; }
    }
    public class Data
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int create_at { get; set; }
        public string thread_id { get; set; }
        public string role { get; set; }
        public Content[] content { get; set; }
    }
    public class Content
    {
        public string type { get; set; }
        public Text text { get; set; }
        public Array[] file_ids { get; set; }
        public string assistant_id { get; set; }
        public  Object metaData { get; set; }
    }
    public class Text
    {
        public string value { get; set; }
        public Array[] annotations { get; set; }
    }
}

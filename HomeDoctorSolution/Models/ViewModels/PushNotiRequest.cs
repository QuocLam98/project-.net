namespace HomeDoctor.Models.ViewModels
{
    public class PushNotiRequest
    {
        public int accountId { get; set; }
        public string title { get; set; }   
        public string message { get; set; }
        public int? id { get; set; }
        public string? key { get; set; }    
    }
}

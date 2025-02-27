namespace HomeDoctor.Models.ViewModels;

public class MessageContactViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }

    public string? Photo { get; set; }
    public string RoomName { get; set; }
}
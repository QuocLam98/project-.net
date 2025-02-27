
namespace HomeDoctorSolution.Models.ViewModels
{
    public class MessageViewModel : Message
    {

        public string MessageTypeName { get; set; }

        public string MessageStatusName { get; set; }

        public string RoomName { get; set; }

        public string AccountName { get; set; }

        public string AccountPhoto { get; set; }

        public int CountTotalUnread { get; set; }

        public int RoleId { get; set; }   
    }
}

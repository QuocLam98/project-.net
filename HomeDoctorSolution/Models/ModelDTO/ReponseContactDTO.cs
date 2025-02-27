using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Models.ViewModels;

namespace HomeDoctorSolution.Models.ModelDTO
{
    public class ReponseContactDTO
    {
        public List<Account> listDoctor { get; set; }
        public List<Account> listUser { get; set; }
        public int CountDoctor { get; set; }
        public int CountUser { get; set; }
    }
}

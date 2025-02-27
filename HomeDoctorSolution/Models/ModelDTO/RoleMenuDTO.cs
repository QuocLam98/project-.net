namespace HomeDoctorSolution.Models.ModelDTO
{
    public class RoleMenuDTO : RoleMenu
    {
        public List<int> Menus { get; set; } = new List<int>();
    }
}

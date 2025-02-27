using AutoMapper;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Models.ViewModels;

namespace HomeDoctor.Util.Extensions
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<OrdersViewModel, Order>();
            CreateMap<ChangePasswordDTO, Account>();
            CreateMap<UpdateAdminAccountDTO, Account>();
            CreateMap<OrdersViewModel, Product>();
            CreateMap<MessageViewModel, Message>();
        }
    }
}

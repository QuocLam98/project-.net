
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using HomeDoctorSolution.Models.ViewModels;

        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface IBaseService<TModel> where TModel : class
            {
                Task<List<TModel>> List();
                Task<List<TModel>> Search(string keyword);
                Task<List<TModel>> ListPaging(int pageIndex,int pageSize);
                Task<TModel> Detail(int? id);
                Task Add(TModel obj);
                Task Update(TModel obj);
                Task Delete(TModel obj);
                Task<int> DeletePermanently(int? id);
                int Count();
            }
        }
    
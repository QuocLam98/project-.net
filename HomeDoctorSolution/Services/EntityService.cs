
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Repository;
        using HomeDoctorSolution.Services.Interfaces;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services
        {
            public class EntityService : IEntityService
            {
                IEntityRepository entityRepository;
                public EntityService(
                    IEntityRepository _entityRepository
                    )
                {
                    entityRepository = _entityRepository;
                }
                public async Task Add(Entity obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await entityRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = entityRepository.Count();
                    return result;
                }
        
                public async Task Delete(Entity obj)
                {
                    obj.Active = 0;
                    await entityRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await entityRepository.DeletePermanently(id);
                }
        
                public async Task<Entity> Detail(int? id)
                {
                    return await entityRepository.Detail(id);
                }
        
                public async Task<List<Entity>> List()
                {
                    return await entityRepository.List();
                }
        
                public async Task<List<Entity>> ListPaging(int pageIndex, int pageSize)
                {
                    return await entityRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Entity>> ListServerSide(EntityDTParameters parameters)
                {
                    return await entityRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Entity>> Search(string keyword)
                {
                    return await entityRepository.Search(keyword);
                }
        
                public async Task Update(Entity obj)
                {
                    await entityRepository.Update(obj);
                }
            }
        }
    
    
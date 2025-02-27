using Microsoft.EntityFrameworkCore;

namespace HomeDoctorSolution.Repository.UploadFile.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        Task<int> CommitAsync();
    }
}

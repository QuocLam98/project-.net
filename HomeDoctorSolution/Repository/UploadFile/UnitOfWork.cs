using HomeDoctorSolution.Repository.UploadFile.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeDoctorSolution.Repository.UploadFile
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        public UnitOfWork(TContext _context)
        {
            context = _context;

        }
        public async Task<int> CommitAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();

    }
}

using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.UploadFileModel;
using HomeDoctorSolution.Repository.Interfaces;
using HomeDoctorSolution.Repository.UploadFile.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeDoctorSolution.Repository.UploadFile
{
    public class UploadFileRepository : RepositoryBaseAsync<UploadFiles, int, HomeDoctorContext>, IUploadFileRepository
    {
        private readonly HomeDoctorContext _db;
        private readonly IUnitOfWork<HomeDoctorContext> _unitOfWork;
        public UploadFileRepository(HomeDoctorContext db, IUnitOfWork<HomeDoctorContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        public async Task<PagingData<List<UploadFiles>>> ListPaging(PagingUploadFileParameter parameters)
        {
            string keyword = parameters.Keyword.Trim();
            string contentType = parameters.ContentType.Trim();
            var query = from fi in _db.UploadFiles
                        join fld in _db.FolderUploads on fi.FolderUploadId equals fld.Id
                        where fi.Active == 1 && fld.Active == 1 && fld.Id == parameters.FodlderUploadId
                        orderby fi.CreatedTime descending
                        select fi;
            if (!String.IsNullOrEmpty(contentType))
            {
                string stringMime = "";
                var mimeAll = contentType.Split(",").Where(c => c.Contains("/*")).Select(c => c.Trim().Replace("*", ""));
                if (mimeAll != null && mimeAll.Count() > 0)
                {
                    stringMime = String.Join(",", UploadFileConst._mappings.Where(c => mimeAll.Any(g => c.Value.Contains(g))).Select(c => c.Value));
                }
                query = query.Where(c => parameters.ContentType.Contains(c.MimeType) || parameters.ContentType.Contains(c.Extension) || stringMime.Contains(c.MimeType));
            }
            int totalRecord = await query.CountAsync();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => c.Name.ToLower().Contains(keyword) || c.Extension.ToLower().Contains(keyword));
            }
            int totalFiltered = await query.CountAsync();
            var data = await query.Skip(parameters.PageStart).Take(parameters.PageSize).ToListAsync();
            var result = new PagingData<List<UploadFiles>>()
            {
                DataSource = data,
                PageSize = parameters.PageSize,
                CurrentPage = parameters.PageIndex + 1,
                Total = totalRecord,
                TotalFiltered = totalFiltered,
            };
            return result;
        }
        public async Task<PagingData<List<UploadFiles>>> ListPagingByAccountId(PagingUploadFileParameter parameters, int accountId)
        {
            string keyword = parameters.Keyword.Trim();
            string contentType = parameters.ContentType.Trim();
            var query = from fi in _db.UploadFiles
                        join fld in _db.FolderUploads on fi.FolderUploadId equals fld.Id
                        where fi.Active == 1 && fld.Active == 1 && fld.Id == parameters.FodlderUploadId && (fi.AccountId == accountId || fld.Id == 1000019)
                        select fi;
            if (!String.IsNullOrEmpty(contentType))
            {
                string stringMime = "";
                var mimeAll = contentType.Split(",").Where(c => c.Contains("/*")).Select(c => c.Trim().Replace("*", ""));
                if (mimeAll != null && mimeAll.Count() > 0)
                {
                    stringMime = String.Join(",", UploadFileConst._mappings.Where(c => mimeAll.Any(g => c.Value.Contains(g))).Select(c => c.Value));
                }
                query = query.Where(c => parameters.ContentType.Contains(c.MimeType) || parameters.ContentType.Contains(c.Extension) || stringMime.Contains(c.MimeType));
            }
            int totalRecord = await query.CountAsync();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => c.Name.ToLower().Contains(keyword) || c.Extension.ToLower().Contains(keyword));
            }
            int totalFiltered = await query.CountAsync();
            var data = await query.Skip(parameters.PageStart).Take(parameters.PageSize).ToListAsync();
            var result = new PagingData<List<UploadFiles>>()
            {
                DataSource = data,
                PageSize = parameters.PageSize,
                CurrentPage = parameters.PageIndex + 1,
                Total = totalRecord,
                TotalFiltered = totalFiltered,
            };
            return result;
        }
    }
}

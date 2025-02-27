

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Helper;
using HomeDoctor.Util.DTParameters;
using HomeDoctor.Models.ViewModels.Post;
using HomeDoctorSolution.Services;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        IPostService service;
        public PostController(IPostService _service, ICacheHelper cacheHelper) : base(cacheHelper)
        {
            service = _service;
        }
        [HttpGet]
        [Route("admin/List")]
        public async Task<IActionResult> AdminListServerSide()
        {
            return View();
        }
        [HttpGet]
        [Route("api/List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var dataList = await service.List();
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Detail/{Id}")]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var dataList = await service.Detail(Id);
                if (dataList == null)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            try
            {
                var dataList = await service.Search(keyword);
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/ListPaging")]
        public async Task<IActionResult> ListPaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 0 || pageSize < 0) return BadRequest();
            try
            {
                var dataList = await service.ListPaging(pageIndex, pageSize);

                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }

                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/Add")]
        public async Task<IActionResult> Add([FromBody] Post model)
        {
            if (ModelState.IsValid)
            {
                //1. business logic

                //data validation
                if (model.Name.Length == 0)
                {
                    return BadRequest();
                }
                //2. add new object
                try
                {
                    await service.Add(model);
                    var homedoctorsolutionResponse = HomeDoctorResponse.CREATED(model);
                    return Created("", homedoctorsolutionResponse);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("api/Update")]
        public async Task<IActionResult> Update([FromBody] Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic 
                    //2. update object
                    model.PostTypeId = SystemConstant.POST_TYPE_TEXT;
                    model.PostLayoutId = SystemConstant.POST_LAYOUT_DEFAULT;
                    await service.Update(model);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(homedoctorsolutionResponse);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/Delete")]
        public async Task<IActionResult> Delete([FromBody] Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic
                    await service.Delete(model);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(homedoctorsolutionResponse);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/DeletePermanently")]
        public async Task<IActionResult> DeletePermanently([FromBody] Post model)
        {
            var result = 0;
            if (!(model.Id > 0))
            {
                return BadRequest();
            }
            try
            {
                //physically delete object
                result = await service.DeletePermanently(model.Id);
                if (result == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/Count")]
        public int CountPost()
        {
            int result = service.Count();
            return result;
        }
        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] PostDTParameters parameters)
        {
            try
            {
                var data = await service.ListServerSide(parameters);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("api/AddPost")]
        public async Task<IActionResult> CreateAsync([FromBody] InsertPostDTO model)
        {
            try
            {
                model.PostAccountId = this.GetLoggedInUserId();
                await service.CreateAsync(model);
                var happysmilesolutionResponse = HomeDoctorResponse.CREATED(model);
                return Created("", happysmilesolutionResponse);
            }
            catch (Exception e)
            {

                return BadRequest();
            }

        }
        [HttpGet]
        [Route("api/DetailbyId/{Id}")]
        public async Task<IActionResult> DetailById(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var dataList = await service.DetailById(Id);
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("api/list-post")]
        public async Task<IActionResult> ListPostOnHomePage([FromBody] PostParameters parameters)
        {
            try
            {
                var dataList = await service.ListPost(parameters);
                if (dataList == null)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("api/list-post-by-category")]
        public async Task<IActionResult> ListPostByPostCategory([FromBody] PostParameters parameters)
        {
            try
            {
                var dataList = await service.ListPostByPostCategory(parameters);
                if (dataList == null)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost("api/list-post-by-tag")]
        public async Task<IActionResult> ListPostByPostTag([FromBody] PostParameters parameters)
        {
            try
            {
                var dataList = await service.ListPostByPostTag(parameters);
                if (dataList == null)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("api/search")]
        public async Task<IActionResult> ListPostByPostName([FromBody] PostParameters parameters)
        {
            try
            {
                var dataList = await service.ListPostByPostName(parameters);
                if (dataList == null)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/filter-post")]
        public async Task<IActionResult> filterPost()
        {
            try
            {
                var dataList = await service.LoadDataFilterPostHomePageAsync();
                if (dataList == null)
                {
                    return NotFound();
                }
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/LatestPost")]
        public async Task<IActionResult> LatestPostsByTime()
        {
            try
            {
                var data = await service.LatestPostsByTime();
                var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(data);
                return Ok(happysmilesolutionResponse);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/list-four-post")]
        public async Task<IActionResult> ListFourPostByTimeDesc()
        {
            try
            {
                var listPost = await service.ListFourPostByTimeDesc();
                if(listPost == null || listPost.Count == 0)
                {
                    return NotFound();
                }
                var homeDoctorSolutionResponse = HomeDoctorResponse.SUCCESS(listPost.Cast<object>().ToList());
                return Ok(homeDoctorSolutionResponse);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost("api/searching-post")]
        public async Task<IActionResult> SearchingPost([FromBody] SearchingPostParameters parameters)
        {
            try
            {
                var data = await service.ListPostHomePageAsync(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }

}

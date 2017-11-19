using System.Web.Http;
using Admin.DataService.Interface;
using Admin.DataService.DTO;
using System.Collections.Generic;
using Admin.DataService.Filters;
using Admin.Common;

namespace Admin.WebApp.Controllers
{

    public class UserController : BaseApiController
    {
        private readonly IWorkerUserService userService;
        
        public UserController(IWorkerUserService userService)
        {
            this.userService = userService;
            
        }

        [Route("api/{lang}/User/Get")]
        [HttpGet]
        public IHttpActionResult Get(long id = 0)
        {
            return execute(delegate ()
            {
                WorkerUserDTO dto = userService.GetDTO(id);
                result = Json(new { success = true, data = dto });
            });
        }   

        [Route("api/{lang}/User/GetList")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri]UserFilter filter, [FromUri]OrderInfo orderInfo, [FromUri]PageInfo pageInfo)
        {
            return execute(delegate ()
            {
                IEnumerable<WorkerUserDTO> list = userService.GetList(filter, orderInfo, ref pageInfo);
                result = Json(new { success = true, data = list, totalCount = pageInfo.TotalCount, count = pageInfo.Count });
            });
        }

        
        [Route("api/{lang}/User/GetListUserType")]
        [HttpGet]
        public IHttpActionResult GetListUserType()
        {
            return execute(delegate ()
            {
                IEnumerable<SimpleDTO> list = userService.GetListUserType();
                result = Json(new { success = true, data = list });
            });
        }

        [Route("api/{lang}/User/Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            return execute(delegate ()
            {
                userService.Delete(id);
                result = Json(new { success = true });
            });
        }
        

        [Route("api/{lang}/User/Save")]
        [HttpPost]
        public IHttpActionResult Save(WorkerUserDTO dto)
        {
            return execute(delegate ()
            {
                userService.Save(dto);
                result = Json(new { success = true });
            });
        }
        
    }
}
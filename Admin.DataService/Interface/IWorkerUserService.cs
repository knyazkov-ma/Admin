using Admin.Entity;
using Admin.DataService.DTO;
using System.Collections.Generic;
using Admin.DataService.Filters;
using Admin.Common;

namespace Admin.DataService.Interface
{
    public interface IWorkerUserService
    {
        WorkerUser Get(long id);
        WorkerUserDTO GetDTO(long id);
        WorkerUserDTO GetDTO(string userName);
        IEnumerable<WorkerUserDTO> GetList(UserFilter filter, OrderInfo orderInfo, ref PageInfo pageInfo);
        void Delete(long id);
        void Save(WorkerUserDTO dto);
        IEnumerable<SimpleDTO> GetListUserType();
    }
}

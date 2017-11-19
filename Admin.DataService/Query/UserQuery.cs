using Admin.Common;
using Admin.Data.Query;
using Admin.DataService.Filters;
using Admin.DataService.DTO;
using Admin.Entity;
using System.Collections.Generic;
using System.Linq;
using Admin.Common.Helpers;
using System;
using System.Linq.Expressions;

namespace Admin.DataService.Query
{
    /// <summary>
    /// Запрос: пользователи
    /// </summary>
    public class UserQuery : IQuery<IEnumerable<WorkerUserDTO>, WorkerUser>
        
    {
        private readonly OrderInfo orderInfo;
        private readonly PageInfo pageInfo;
        private readonly UserFilter filter;

        public UserQuery(UserFilter filter, OrderInfo orderInfo, ref PageInfo pageInfo)
        {
            this.orderInfo = orderInfo;
            this.pageInfo = pageInfo;
            this.filter = filter;
        }

        public IEnumerable<WorkerUserDTO> Run(IQueryable<WorkerUser> users)
        {
            Expression<Func<WorkerUser, bool>> where = t => true;

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.Name))
                    where = where.AndAlso(t => t.Name.ToUpper().Contains(filter.Name.ToUpper()));

                if (!String.IsNullOrWhiteSpace(filter.Email))
                    where = where.AndAlso(t => t.Email.ToUpper().Contains(filter.Email.ToUpper()));

                

                if (filter.UserTypeIds != null && filter.UserTypeIds.Any())
                    where = where.AndAlso(t => filter.UserTypeIds.Contains(t.UserType.Id));

            }
                            
            var filteredUsers = users.Where(where);
            
            

            if (orderInfo != null)
            {
                filteredUsers = filteredUsers.OrderBy(orderInfo.PropertyName, orderInfo.Asc);
            }

            var q = filteredUsers.Select(t => new WorkerUserDTO
            {
                Id = t.Id,
                Email = t.Email,
                Name = t.Name,
                Password = t.Password,
                UserType = t.UserType,
                Disabled = t.Disabled
            });


            if (pageInfo != null)
            {
                pageInfo.TotalCount = users.Count();
                pageInfo.Count = filteredUsers.Count();

                if (pageInfo.PageSize > 0)
                    q = q
                        .Skip(pageInfo.PageSize * pageInfo.CurrentPage)
                        .Take(pageInfo.PageSize);
            }

            return q.ToList();
        }
    }
}

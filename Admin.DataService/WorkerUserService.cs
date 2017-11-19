using Admin.DataService.Interface;
using Admin.Data.Repository;
using Admin.Entity;
using System;
using Admin.DataService.DTO;
using Admin.DataService.Resources;
using Admin.DataService.Common;
using System.Linq.Expressions;
using Admin.Common.Aspects;
using System.Collections.Generic;
using System.Linq;
using Admin.DataService.Filters;
using Admin.Common;
using Admin.Data.Query;
using Admin.DataService.Query;

namespace Admin.DataService
{
    /// <summary>
    /// Для работы с пользователями исполнителями/диспетчерами
    /// </summary>
    public class WorkerUserService : BaseService, IWorkerUserService
    {
        private readonly IQueryRunner queryRunner;
        private readonly IBaseRepository<WorkerUser>    userRepository;
        private readonly IBaseRepository<TypeWorkerUser> typeWorkerUserRepository;
        private readonly IRepository                    repository;

        public WorkerUserService(IQueryRunner queryRunner,
            IBaseRepository<WorkerUser> userRepository,
            IBaseRepository<TypeWorkerUser> typeWorkerUserRepository,
            IRepository repository)
        {
            this.queryRunner = queryRunner;
            this.userRepository = userRepository;
            this.typeWorkerUserRepository = typeWorkerUserRepository;
            this.repository = repository;
        }



        private WorkerUserDTO getUserDTO(Expression<Func<WorkerUser, bool>> predicate)
        {

            WorkerUser user = userRepository.Get(predicate);
            if (user == null)
                return null;

            WorkerUserDTO dto = new WorkerUserDTO()
            {
                Email = user.Email,
                Password = user.Password,
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType,
                Disabled =  user.Disabled
            };
            return dto;
        }

        public WorkerUser Get(long id)
        {
            return userRepository.Get(id);
        }

        public WorkerUserDTO GetDTO(long id)
        {
            if(id == 0)
                return  new WorkerUserDTO()
                {
                    UserType = new TypeWorkerUser()
                };
            return getUserDTO(u => u.Id == id);
        }

        public WorkerUserDTO GetDTO(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
                return null;

            if (userName.IndexOf("@") > 0)
                return getUserDTO(u => u.Email.ToUpper() == userName.ToUpper());

            return getUserDTO(u => u.Email.ToUpper().StartsWith(userName.ToUpper() + "@"));
        }

        
        public IEnumerable<WorkerUserDTO> GetList(UserFilter filter, OrderInfo orderInfo, ref PageInfo pageInfo)
        {
            return queryRunner.Run(new UserQuery(filter, orderInfo, ref pageInfo));
        }

        public void Delete(long id)
        {
            if(id == 1)
                throw new DataServiceException(Resource.SuperUserDeleteMsg);

            userRepository.Delete(id);
            repository.SaveChanges();
        }

        [Transaction]
        public void Save(WorkerUserDTO dto)
        {
            if (dto.Id == 1)
                throw new DataServiceException(Resource.SuperUserUpdateMsg);

            checkStringConstraint("Name", dto.Name, true, 100, 5);
            checkStringConstraint("Email", dto.Email, true, 100, 5);
            checkStringConstraint("Password", dto.Password, true, 100, 5);

            if (!String.IsNullOrWhiteSpace(dto.Email))
            {
                WorkerUserDTO existsUser = getUserDTO(u => u.Email.ToUpper() == dto.Email.ToUpper() && u.Id != dto.Id);
                if (existsUser != null)
                    setErrorMsg("Email", Resource.UniqueEmailConstraintMsg);

                if (!FormatConfirm.IsEmail(dto.Email))
                    setErrorMsg("Email", Resource.EmailConstraintMsg);
            }

            if (dto.UserType == null || dto.UserType.Id == 0)
                setErrorMsg("UserType", Resource.EmptyConstraintMsg);
            
            if (errorMessages.Count > 0)
                throw new DataServiceException(Resource.GeneralConstraintMsg, errorMessages);

            WorkerUser user = null;
            if (dto.Id == 0)
                user = new WorkerUser();
            else
                user = userRepository.Get(dto.Id);

            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Name = dto.Name;
            user.UserType = typeWorkerUserRepository.Get(dto.UserType.Id);
            user.Disabled = dto.Disabled;
        
            userRepository.Save(user);
            repository.SaveChanges();
        }

        public IEnumerable<SimpleDTO> GetListUserType()
        {
            return typeWorkerUserRepository.GetList()
                .OrderBy(t => t.Name)
                .Select(t => new SimpleDTO()
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList();
        }
    }
}

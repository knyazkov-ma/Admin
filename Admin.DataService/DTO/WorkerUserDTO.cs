using Admin.DataService.DTO.Resources;
using Admin.Entity;
using System;

namespace Admin.DataService.DTO
{
    /// <summary>
    /// Пользователь исполнитель/диспетчер
    /// </summary>
    public class WorkerUserDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string UserName
        {
            get { return String.Format("{0} ({1})", Name, UserType.Name); }
        }

        /// <summary>
        /// E-mail (в качестве логина используется, может также только имя почтового ящика без домена)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public TypeWorkerUser UserType { get; set; }

        
        /// <summary>
        /// Заблокирован
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Заблокирован
        /// </summary>
        public string DisabledName
        {
            get { return Disabled ? Resource.Text_Disabled : Resource.Text_Enabled; }

        }

    }
}

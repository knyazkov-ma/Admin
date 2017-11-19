using System.Collections.Generic;

namespace Admin.DataService.Filters
{
    /// <summary>
    /// Для фильтрации пользователей
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// Список типов пользователя
        /// </summary>
        public IList<long> UserTypeIds { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        
    }
}

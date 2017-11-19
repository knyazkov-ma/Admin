using Admin.DataService.Interface;
using System.Collections.Generic;

namespace Admin.DataService
{
    /// <summary>
    /// Для работы с локализациями
    /// </summary>
    public class CultureService : BaseDataService, ICultureService
    {
        public IReadOnlyList<string> GetList()
        {
            //1-я - локализация по-умолчанию
            return new List<string> { "ru"/*, "en"*/ }; 
        }
                
    }
}

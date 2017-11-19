using System.Collections.Generic;

namespace Admin.DataService.Interface
{
    public interface ICultureService
    {
        IReadOnlyList<string> GetList();
    }
}

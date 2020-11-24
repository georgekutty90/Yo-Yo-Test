using System.Collections.Generic;

namespace Yo_Yo_App.DataAccess
{
    public interface IDBRepository<T> where T : class
    {

        IEnumerable<T> SelectByParams(string query, object parameters);
        IEnumerable<T> Select(string query);
        int ExecuteCommand(string query, object parameters);
       
    }
}

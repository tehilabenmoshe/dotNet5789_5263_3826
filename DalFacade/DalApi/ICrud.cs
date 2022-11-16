using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi {

    public interface ICrud<T> where T : struct
    {
        int Add(T? item);
        T? GetById(int id);
        void Delete(int id);
        void Update(T? item);
        IEnumerable<T?> GetAll(); //return object of yeshoot
    }

}







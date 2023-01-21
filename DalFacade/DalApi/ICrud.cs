using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
/// <summary>
/// Each entity inherits from the ICrud generic interface.

///</summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> where T : struct
    {
        int Add(T item);//adding new item 
        T GetById(int id);//returns the item according to the matching recived id
        void Delete(int id);//deleting the item with the recieved id
        void Update(T item);//updating the item that has received
        IEnumerable<T?> GetAll(Func<T?, bool>? filter = null); //returning all the items
        

}


  











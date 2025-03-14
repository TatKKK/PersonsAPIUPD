using PersonsDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Interfaces
{
    public interface IPersonService 
    {
        //List<Person> GetAll();

       object GetAll();

        void DeletePerson(int id);
    }
}

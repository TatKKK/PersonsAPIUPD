using PersonsDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Interfaces
{
    public interface IPersonRepository 
    {
        public void DeletePerson(int id);

        //List<Person> GetAll();

        object GetAll();
    }
}

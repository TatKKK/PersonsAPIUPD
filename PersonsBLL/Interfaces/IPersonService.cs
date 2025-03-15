using PersonsBLL.Dtos;
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
        void AddPerson(AddPersonDto person);
        void AddRelatedPerson(AddRelatedPersonDto addRelatedPersonDto);
        void DeleteRelatedPerson(DeleteRelatedPersonDto deleteRelatedPersonDto);
        GetPersonsDto? GetPersonInfoById(int id);

        object GetAll();

        void DeletePerson(int id);
    }
}

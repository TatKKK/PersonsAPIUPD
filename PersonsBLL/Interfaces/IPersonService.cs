using PersonsBLL.Dtos;
using PersonsDAL.Entities;
using PersonsDAL.Models;
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

        List<PersonInfo> GetAll();

        void DeletePerson(int id);
        void UpdatePerson(UpdatePersonDto person);

        List<PersonsReportDto> GetRelationshipReport();
        IEnumerable<GetPersonsDto> GetPersonsPaginated(int pageNumber, int rowCount);

    }
}

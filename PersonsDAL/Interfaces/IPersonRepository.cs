using PersonsDAL.Entities;
using PersonsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Interfaces
{
    public interface IPersonRepository 
    {
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(int id);
        Person? GetPersonInfoById(int id);
        List<PersonInfo> GetAll();

        void AddRelatedPerson(PersonRelationship personRelationship);
        void DeleteRelatedPerson(PersonRelationship personRelationship);
        List<Person> GetAllRelatedPersons(Person person);

        List<PhoneNumber> GetAllPhoneNumbers(Person person);

        List<PersonsReport> GetRelationshipReport();

        IEnumerable<Person> GetPersonsPaginated(int pageNumber, int rowCount);

        List<Person> QuickSearchPersons(int pageNumber, int rowCount, string? name, string lastname, string idCard);

        List<Person> DetailedSearchPersons(
            int pageNumber,
            int rowCount,
            string? name,
            string? lastname,
            string? idCard,
            int? gender,
            DateTime? birthDate,
            int? cityId,
            string? imagePath);

        void UploadPhoto(Person person);
    }
}

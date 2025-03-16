﻿using AutoMapper;
using Microsoft.VisualBasic;
using PersonsBLL.Dtos;
using PersonsBLL.Interfaces;
using PersonsBLL.Models;
using PersonsDAL.Entities;
using PersonsDAL.Entities.Enums;
using PersonsDAL.Interfaces;
using PersonsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonsBLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper mapper;
        private readonly IPersonRepository personRepository;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public void AddPerson(AddPersonDto personDto)
        {
            var person =mapper.Map<Person>(personDto);
            personRepository.AddPerson(person);           
        }
        

        public void DeletePerson (int id)
        {
            personRepository.DeletePerson(id);
        }

        public void AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public GetPersonsDto? GetPersonInfoById(int id)
        {
            var person = personRepository.GetPersonInfoById(id);

            if (person == null)
            {
                return null;
            }

            var relatedPersons = personRepository.GetAllRelatedPersons(person);
            var phoneNumbers = personRepository.GetAllPhoneNumbers(person);

            var personDto = mapper.Map<GetPersonsDto>(person);
            personDto.RelatedPersons = mapper.Map<List<GetPersonsDto>>(relatedPersons);
            personDto.PhoneNumbers = mapper.Map<List<PhoneNumberDto>>(phoneNumbers);

            return personDto;
        }

        public void AddRelatedPerson(AddRelatedPersonDto addReleatedPersonDto)
        {
            var relatedPerson = mapper.Map<PersonRelationship>(addReleatedPersonDto);
            personRepository.AddRelatedPerson(relatedPerson);
        }

        public void DeleteRelatedPerson(DeleteRelatedPersonDto deleteRelatedPersonDto)
        {
            var relatedPerson = mapper.Map<PersonRelationship>(deleteRelatedPersonDto);
            personRepository.DeleteRelatedPerson(relatedPerson);
        }

        List<PersonInfo> IPersonService.GetAll()
        {
            return personRepository.GetAll();
        }

        public void UpdatePerson(UpdatePersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);
            personRepository.UpdatePerson(person);

        }

        public List<PersonsReportDto> GetRelationshipReport()
        {
            var report = personRepository.GetRelationshipReport();
            return mapper.Map<List<PersonsReportDto>>(report);
        }
    }
}

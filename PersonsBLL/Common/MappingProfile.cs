using AutoMapper;
using PersonsBLL.Dtos;
using PersonsDAL.Entities;
using PersonsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddPersonDto, Person>();
            CreateMap<PhoneNumberDto, PhoneNumber>();
            CreateMap<PersonRelationshipDto, PersonRelationship>();
            //CreateMap<UpdatePersonDto, Person>();
            //CreateMap<UploadPersonPhotoDto, Person>();
            CreateMap<AddRelatedPersonDto, PersonRelationship>();
            CreateMap<DeleteRelatedPersonDto, PersonRelationship>();

            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PersonRelationship, PersonRelationshipDto>();
            CreateMap<Person, GetPersonsDto>();
            CreateMap<PersonsReport, PersonsReportDto>();

        }
    }
}

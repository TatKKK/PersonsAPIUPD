using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsBLL.Interfaces;
using PersonsBLL.Models;
using PersonsDAL.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using PersonsBLL.Dtos;
using PersonsBLL.Services;
using Microsoft.Extensions.Localization;
using WebApi.Filters;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IStringLocalizer<PersonController> localizer;
        private readonly IWebHostEnvironment env;

        public PersonController(IPersonService personService, IStringLocalizer<PersonController> localizer, IWebHostEnvironment env)
        {
            this.personService = personService;
            this.localizer = localizer;
            this.env = env;
        }

        [HttpGet("hello")]
        public IActionResult GetHelloMessage()
        {
            var message = localizer["HelloMessage"]; // Auto-selects based on culture
            return Ok(new { Message = message });
        }


        [HttpPost]
        [ValidateModel]
        public IActionResult AddPerson(AddPersonDto person)
        {
            personService.AddPerson(person);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePerson(UpdatePersonDto personDto)
        {
            personService.UpdatePerson(personDto);
            return Ok();
        }

        [HttpGet("PersonInfo")]
        public IActionResult GerPersonById(int id)
        {
            var person = personService.GetPersonInfoById(id);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult AddRelatedPerson(AddRelatedPersonDto person)
        {
            personService.AddRelatedPerson(person);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteRelatedPerson(DeleteRelatedPersonDto person)
        {
            personService.DeleteRelatedPerson(person);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            var persons =  personService.GetAll();

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    WriteIndented = true
            //};

            //string json = JsonSerializer.Serialize(persons, options);

            return Ok(persons);
        }

        [HttpGet]
        public IActionResult GetPersonsPaginated(int pageNumber, int rowCount)
        {
            var personsPaginated = personService.GetPersonsPaginated(pageNumber, rowCount);
            return Ok(personsPaginated);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personService.DeletePerson(id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetRelationshipReport()
        {
            var x = personService.GetRelationshipReport();
            return Ok(x);
        }

        [HttpGet]
        public IActionResult QuickSearchPersons(int pageNumber, int pageSize, string? name, string? lastname, string? idCard)
        {
            var x = personService.QuickSearchPersons(pageNumber, pageSize, name, lastname, idCard);
            return Ok(x);
        }
        [HttpGet]
        public IActionResult DetailedSearchPersons(int pageNumber,
           int rowCount,
           string? name,
           string? lastname,
           string? idCard,
           int? gender,
           DateTime? birthDate,
           int? cityId,
           string? imagePath)
        {
            var x = personService.DetailedSearchPersons(pageNumber, rowCount, name, lastname, idCard, gender, birthDate, cityId, imagePath);
            return Ok(x);
        }


        [HttpPut]
        public IActionResult UploadPhoto(UploadPhotoDto photoDto)
        {
            var result = personService.UploadPhoto(photoDto, env.ContentRootPath);
            if (!result) return BadRequest("File extension is not correct");

            return Ok();
        }
    }

}

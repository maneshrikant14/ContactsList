using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Entities;
using WebApi.Models;
using WebApi.Helpers;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        public ContactsController(IContactRepository contactRepository, IMapper mapper)
        {
            this.contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactDto>> GetContacts()
        {
            var contacts = this.contactRepository.GetContacts();
            return Ok(this.mapper.Map<IEnumerable<ContactDto>>(contacts));
        }

        [HttpGet("{contactID}", Name = "GetContact")]
        public ActionResult<ContactDto> GetContact(int contactID)
        {
            var contact = this.contactRepository.GetContact(contactID);
            if (contact == null)
            {
                return NotFound("Contact with ID-" + contactID + " doesn't exist!");
            }

            return Ok(this.mapper.Map<ContactDto>(contact));

        }

        [HttpPost]
        public ActionResult<ContactDto> CreateContact( ContactForSaveDto contactForSaveDto)
        {
            var contacttoSave = this.mapper.Map<Contact>(contactForSaveDto);
            this.contactRepository.AddNewContact(contacttoSave);
            var contactToReturn = this.mapper.Map<ContactDto>(contacttoSave);
            return CreatedAtRoute("GetContact", new { contactID = contactToReturn.ContactID }, contactToReturn);
        }

        [HttpPut("{contactID}")]
        public ActionResult UpdateContact(int contactID, [FromBody] ContactForUpdateDto contactforUpdateDto)
        {
            var contactFromRepo = this.contactRepository.GetContact(contactID);
            if (contactFromRepo == null)
            {
                return NotFound("Contact with ID-" + contactID + " did not exist!");
            }

            this.mapper.Map(contactforUpdateDto, contactFromRepo);
            this.contactRepository.UpdateContact(contactFromRepo);
            return NoContent();
        }

        [HttpDelete("{contactID}")]
        public ActionResult DeleteContact(int contactID)
        {
            var contactFromRepo = this.contactRepository.GetContact(contactID);
            if (contactFromRepo == null)
            {
                return NotFound("Contact with ID-" + contactID + " did not exist!");
            }

            var deleteResult = this.contactRepository.RemoveContact(contactID);
            return NoContent();
        }
    }
}

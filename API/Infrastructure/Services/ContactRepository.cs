using Infrastructure.DbContexts;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Services
{

    public class ContactRepository : IContactRepository, IDisposable
    {
        private readonly ContactContext contactContext;
        public ContactRepository(ContactContext _contactContext)
        {
            this.contactContext = _contactContext ?? throw new ArgumentNullException(nameof(_contactContext));
        }

        public Contact AddNewContact(Contact contact)
        {   
            /* Logic to prevent duplicate entries */
            this.contactContext.Contacts.Add(contact);
            this.contactContext.SaveChanges();
          
            return contact;
        }

        public Contact GetContact(int id)
        {
            return this.contactContext.Contacts.Where(c => c.ContactID == id).FirstOrDefault();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return this.contactContext.Contacts.ToList();
        }

        public bool RemoveContact(int id)
        {
            var contactToRemove = this.contactContext.Contacts.Where(c => c.ContactID == id).FirstOrDefault();
            if (contactToRemove != null)
            {
                this.contactContext.Remove(contactToRemove);
                return this.Save();
            }

            return false;
        }

        public bool UpdateContact(Contact contact)
        {
            return this.Save();
        }

        public bool Save()
        {
            return (this.contactContext.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            contactContext?.Dispose();
        }
    }
}

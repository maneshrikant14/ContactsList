using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        Contact AddNewContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool RemoveContact(int id);
        bool Save();
    }
}

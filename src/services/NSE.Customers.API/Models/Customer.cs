﻿using System;
using NSE.Core.DomainObjects;

namespace NSE.Customers.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Deleted { get; private set; }
        public Address Address { get; private set; }

        // EF Relation
        protected Customer() { }

        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Deleted = false;
        }

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

        public void setAddress(Address address)
        {
            Address = address;
        }
    }
}

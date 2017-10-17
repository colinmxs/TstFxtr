using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TstFxtr.Tests
{
    public interface IHaveAFirstName
    {
        string FirstName { get; set; }
    }

    public class Person : IHaveAFirstName
    {
        public Person(string firstName, string lastName, DateTime birthday, Location address, ContactInfo contactInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Address = address;
            ContactInfo = contactInfo;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Location Address { get; set; }
        public ContactInfo ContactInfo { get; set; }

    }

    public class Command
    {
        public Guid CampId { get; set; }
        public string CampName { get; set; }
        public object Camp { get; set; } 
        public ClaimsPrincipal User { get; set; }
    }

    public class Customer
    {    
        public Customer(Person person)
        {
            Person = person;
            Orders = new List<Order>();
        }
        public Person Person { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Location
    {
        public Location(string street, string city, string state, string zip, string country)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class ContactInfo
    {
        public ContactInfo(string email, string phone)
        {
            Email = email;
            Phone = phone;
        }

        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Order
    {
        public Order(Product[] products, Store store)
        {
            Products = products;
            Store = store;
            Timestamp = DateTime.Now;
        }

        public Product[] Products { get; set; }
        public DateTime Timestamp { get; set; }
        public Store Store { get; set; }
    }

    public class Product
    {
        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class Store
    {
        public Store(Location location, int number, Manager manager)
        {
            Location = location;
            Number = number;
            Manager = manager;
        }

        public Manager Manager { get; set; }
        public Location Location { get; set; }
        public int Number { get; set; }
    }

    public class Manager
    {
        public Manager(Person person, decimal salary)
        {
            Person = person;
            Salary = salary;
        }
        public Person Person { get; set; }
        public decimal Salary { get; set; }
    }
}

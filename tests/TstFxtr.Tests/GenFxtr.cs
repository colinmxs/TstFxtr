using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static TstFxtr.GenFxtr;

namespace TstFxtr.Tests
{
    [TestClass]
    public class GenFxtr
    {
        [TestMethod]
        public void DoesTheJob()
        {
            var customer = Create<Customer>();
            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Person);
            Assert.IsNotNull(customer.Person.Address);
            Assert.IsNotNull(customer.Person.Address.Street);
            Assert.IsNotNull(customer.Person.Address.City);
            Assert.IsNotNull(customer.Person.Address.State);
            Assert.IsNotNull(customer.Person.Address.Zip);
            Assert.IsNotNull(customer.Person.Address.Country);
            Assert.IsNotNull(customer.Person.Birthday);
            Assert.IsNotNull(customer.Person.ContactInfo);
            Assert.IsNotNull(customer.Person.ContactInfo.Email);
            Assert.IsNotNull(customer.Person.ContactInfo.Phone);
            Assert.IsNotNull(customer.Person.FirstName);
            Assert.IsNotNull(customer.Person.LastName);
            Assert.IsNotNull(customer.Orders);

            var orders = Create<Order>();
            Assert.IsNotNull(orders.Products);
            Assert.IsNotNull(orders.Products[0]);
            Assert.IsNotNull(orders.Products[0].Id);
            Assert.IsNotNull(orders.Products[0].Name);
            Assert.IsNotNull(orders.Store);
            Assert.IsNotNull(orders.Store.Location);
            Assert.IsNotNull(orders.Store.Manager);
            Assert.IsNotNull(orders.Store.Manager.Person);
            Assert.IsNotNull(orders.Store.Manager.Person);
            Assert.IsNotNull(orders.Store.Manager.Person.Address);
            Assert.IsNotNull(orders.Store.Manager.Person.Address.Street);
            Assert.IsNotNull(orders.Store.Manager.Person.Address.City);
            Assert.IsNotNull(orders.Store.Manager.Person.Address.State);
            Assert.IsNotNull(orders.Store.Manager.Person.Address.Zip);
            Assert.IsNotNull(orders.Store.Manager.Person.Address.Country);
            Assert.IsNotNull(orders.Store.Manager.Person.Birthday);
            Assert.IsNotNull(orders.Store.Manager.Person.ContactInfo);
            Assert.IsNotNull(orders.Store.Manager.Person.ContactInfo.Email);
            Assert.IsNotNull(orders.Store.Manager.Person.ContactInfo.Phone);
            Assert.IsNotNull(orders.Store.Manager.Person.FirstName);
            Assert.IsNotNull(orders.Store.Manager.Person.LastName);
            Assert.IsNotNull(orders.Store.Number);
            Assert.IsNotNull(orders.Timestamp);            
        }

        [TestMethod]
        public void ConstructorCustomizationExistingObject()
        {
            var person = Create<Person>();
            Customize(typeof(Customer), person);
            var customer = Create<Customer>();
            Assert.AreSame(person, customer.Person);
        }

        [TestMethod]
        public void ConstructorCustomizationFunc()
        {
            Customize(typeof(Customer), new Func<Person>(() => Create<Person>()));
            var cust1 = Create<Customer>();
            var cust2 = Create<Customer>();
            Assert.AreNotSame(cust1.Person, cust2.Person);
        }
        
        //[TestMethod]
        //public void BasicInfo()
        //{
        //    var person = Create<Person>();
        //    Assert.IsTrue(WordBank.FirstNames.Get().Contains(person.FirstName));
        //    Assert.IsTrue(WordBank.LastNames.Get().Contains(person.LastName));            
        //}        
    }
}

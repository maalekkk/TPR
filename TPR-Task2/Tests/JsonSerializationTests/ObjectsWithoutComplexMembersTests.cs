// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.IO;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using DL;
// using DL.DataObjects;
// using DL.DataObjects.EventsObjects;
// using Newtonsoft.Json;
// using TPR_Task2.Serialization;
//
// namespace Tests.JsonSerializationTests
// {
//     [TestClass]
//     public class ObjectsWithoutComplexMembersTests
//     {
//
//         [TestMethod]
//         public void AuthorSerialization()
//         {
//             // Creating Author
//             Author author1 = new Author(Guid.NewGuid(), "Adam", "Malysz");
//             
//             // Author Serialize
//             JsonFormatter<Author> jsonFormatter = new JsonFormatter<Author>();
//             using(Stream stream = File.Open("serializedAuthor.json", FileMode.Create, FileAccess.ReadWrite))
//                 jsonFormatter.Serialize(stream, author1);
//             
//             // Author Deserialize
//             Author author1_copy;
//             using(Stream stream = File.Open("serializedAuthor.json", FileMode.Open, FileAccess.Read))
//                 author1_copy = jsonFormatter.Deserialize(stream);
//             
//             // Checking 
//             Assert.AreEqual(author1.Name, author1_copy.Name);
//             Assert.AreEqual(author1.Surname, author1_copy.Surname);
//             Assert.AreEqual(author1.Id, author1_copy.Id);
//         }
//
//         [TestMethod]
//         public void PersonSerialization()
//         {
//             // Creating Person
//             Person orgPerson = new Person(Guid.NewGuid(), "Katarzyna", "Kowalska",
//                 new DateTime(1990, 3, 15), "123454321", "k_kowalska@gmail.com", 
//                 "female");
//             
//             // Serialize Person
//             JsonFormatter<Person> jsonFormatter = new JsonFormatter<Person>();
//             using (Stream stream = File.Open("serializedPerson.json", FileMode.Create, FileAccess.ReadWrite))
//             {
//                 jsonFormatter.Serialize(stream, orgPerson);
//             }
//
//             //Deserialize Person
//             Person personCopy;
//             using (Stream stream = File.Open("serializedPerson.json", FileMode.Open, FileAccess.Read))
//             {
//                 personCopy = jsonFormatter.Deserialize(stream);
//             }
//             
//             // Check
//             Assert.AreEqual(orgPerson.Id, personCopy.Id);
//             Assert.AreEqual(orgPerson.Name, personCopy.Name);
//             Assert.AreEqual(orgPerson.Surname, personCopy.Surname);
//             Assert.AreEqual(orgPerson.BirthDate, personCopy.BirthDate);
//             Assert.AreEqual(orgPerson.PhoneNumber, personCopy.PhoneNumber);
//             Assert.AreEqual(orgPerson.Email, personCopy.Email);
//             Assert.AreEqual(orgPerson.Gender1, personCopy.Gender1);
//         }
//
//         [TestMethod]
//         public void ReaderSerialization()
//         {
//             // Creating Reader 
//             Reader orgReader = new Reader(Guid.NewGuid(), "Katarzyna", "Kowalska",
//                 new DateTime(1990, 3, 15), "123454321", "k_kowalska@gmail.com", 
//                 "female", DateTime.Now);
//             
//             //Serialize Reader 
//             JsonFormatter<Reader> jsonFormatter = new JsonFormatter<Reader>();
//             using (Stream stream = File.Open("serializedReader.json", FileMode.Create, FileAccess.ReadWrite))
//             {
//                 jsonFormatter.Serialize(stream, orgReader);
//             }
//             
//             //Deserialize Reader
//             Reader readerCopy;
//             using (Stream stream = File.Open("serializedReader.json", FileMode.Open, FileAccess.Read))
//             {
//                 readerCopy = jsonFormatter.Deserialize(stream);
//             }
//             
//             //Check
//             Assert.AreEqual(orgReader.Id, readerCopy.Id);
//             Assert.AreEqual(orgReader.Name, readerCopy.Name);
//             Assert.AreEqual(orgReader.Surname, readerCopy.Surname);
//             Assert.AreEqual(orgReader.BirthDate, readerCopy.BirthDate);
//             Assert.AreEqual(orgReader.PhoneNumber, readerCopy.PhoneNumber);
//             Assert.AreEqual(orgReader.Email, readerCopy.Email);
//             Assert.AreEqual(orgReader.Gender1, readerCopy.Gender1);
//             Assert.AreEqual(orgReader.DateOfRegistration, readerCopy.DateOfRegistration);
//         }
//
//         [TestMethod]
//         public void EmployeeSerialization()
//         {
//             // Creating Employee
//             Employee orgEmployee = new Employee(Guid.NewGuid(), "Katarzyna", "Kowalska",
//                 new DateTime(1990, 3, 15), "123454321", "k_kowalska@gmail.com",
//                 "female", DateTime.Now);
//
//             // Serialize Employee
//             JsonFormatter<Employee> jsonFormatter = new JsonFormatter<Employee>();
//             using (Stream stream = File.Open("serializedEmployee.json", FileMode.Create, FileAccess.ReadWrite))
//             {
//                 jsonFormatter.Serialize(stream, orgEmployee);
//             }
//
//             // Deserialize Employee
//             Employee employeeCopy;
//             using (Stream stream = File.Open("serializedEmployee.json", FileMode.Open, FileAccess.Read))
//             {
//                 employeeCopy = jsonFormatter.Deserialize(stream);
//             }
//
//             // Check
//             Assert.AreEqual(orgEmployee.Id, employeeCopy.Id);
//             Assert.AreEqual(orgEmployee.Name, employeeCopy.Name);
//             Assert.AreEqual(orgEmployee.Surname, employeeCopy.Surname);
//             Assert.AreEqual(orgEmployee.BirthDate, employeeCopy.BirthDate);
//             Assert.AreEqual(orgEmployee.PhoneNumber, employeeCopy.PhoneNumber);
//             Assert.AreEqual(orgEmployee.Email, employeeCopy.Email);
//             Assert.AreEqual(orgEmployee.Gender1, employeeCopy.Gender1);
//             Assert.AreEqual(orgEmployee.DateOfEmployment, employeeCopy.DateOfEmployment);
//
//         }
//     }
// }
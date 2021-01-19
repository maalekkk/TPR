using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SL;

namespace SLTests
{
    [TestClass]
    public class SLTest
    {
        private IDataRepository dataRepository;

        public void Init()
        {
            dataRepository = new DataRepositoryInMemory();
            dataRepository.AddLocation(1, "A", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(2, "B", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(3, "C", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(4, "D", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(5, "E", 10, 20, new DateTime(2020, 1, 1));
        }

        [TestMethod]
        public void AddTest()
        {
            Init();
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 5);
            dataRepository.AddLocation(6, "F", 10, 20, new DateTime(2020, 1, 1));
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 6);
        }

        [TestMethod]
        public void AddTestException()
        {
            Init();
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 5);
            Assert.ThrowsException<ArgumentException>(() => dataRepository.AddLocation(1, "F", 10, 20, new DateTime(2020, 1, 1)));
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 5);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Init();
            dataRepository.UpdateLocation(1, "Z", 10, 20, new DateTime(2020, 1, 1));
            Assert.AreEqual(dataRepository.GetLocationName(1), "Z");
        }

        [TestMethod]
        public void UpdateTestException()
        {
            Init();
            Assert.ThrowsException<ArgumentException>(() => dataRepository.UpdateLocation(10, "Z", 10, 20, new DateTime(2020, 1, 1)));
        }

        [TestMethod]
        public void DeleteTest()
        {
            Init();
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 5);
            dataRepository.DeleteLocation(5);
            Assert.AreEqual(dataRepository.GetLocationsIds().Count, 4);
        }

        [TestMethod]
        public void DeleteTestException()
        {
            Init();
            dataRepository.DeleteLocation(5);
            Assert.ThrowsException<ArgumentException>(() => dataRepository.DeleteLocation(50));
        }
    }
}
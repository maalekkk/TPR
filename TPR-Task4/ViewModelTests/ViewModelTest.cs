using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using SL;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTest
    {
        MainViewModel viewModel;

        public void Init()
        {
            DataRepositoryInMemory dataRepository = new DataRepositoryInMemory();
            dataRepository.AddLocation(1, "A", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(2, "B", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(3, "C", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(4, "D", 10, 20, new DateTime(2020, 1, 1));
            dataRepository.AddLocation(5, "E", 10, 20, new DateTime(2020, 1, 1));
            viewModel = new MainViewModel(dataRepository);
        }

        [TestMethod]
        public void CommandTest()
        {
            Init();
            Assert.IsNotNull(viewModel.AddLocation);
            Assert.IsNotNull(viewModel.ModifyLocation1);
            Assert.IsNotNull(viewModel.DeleteLocation);
        }

        [TestMethod]
        public void AddTest()
        {
            Init();
            viewModel.Id = 6;
            viewModel.Name = "F";
            viewModel.CostRate = 10;
            viewModel.Availability = 20;
            viewModel.ModifiedData = new DateTime(2020, 2, 2);
            viewModel.AddLocation.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 6);
            Assert.AreEqual(viewModel.ErrorMessage, "");
        }

        [TestMethod]
        public void AddTestFail()
        {
            Init();
            viewModel.Id = 2;
            viewModel.Name = "F";
            viewModel.CostRate = 10;
            viewModel.Availability = 20;
            viewModel.ModifiedData = new DateTime(2020, 2, 2);
            viewModel.AddLocation.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 5);
            Assert.AreNotEqual(viewModel.ErrorMessage, "");
        }

        [TestMethod]
        public void DeleteTest()
        {
            Init();
            viewModel.Location.Id = 1;
            viewModel.DeleteLocation.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 4);
            Assert.AreEqual(viewModel.ErrorMessage, "");
            Assert.AreEqual(viewModel.Id, 0);
            Assert.AreEqual(viewModel.Name, "");
            Assert.AreEqual(viewModel.CostRate, 0);
            Assert.AreEqual(viewModel.Availability, 0);
            Assert.IsNotNull(viewModel.ModifiedData);
            Assert.AreEqual(viewModel.ErrorMessage, "");
        }


        [TestMethod]
        public void DeleteTestFail()
        {
            Init();
            viewModel.Location.Id = 10;
            viewModel.DeleteLocation.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 5);
            Assert.AreNotEqual(viewModel.ErrorMessage, "");
            Assert.AreEqual(viewModel.Location.Id, 10);
            Assert.AreNotEqual(viewModel.ErrorMessage, "");

        }

        [TestMethod]
        public void UpdateTest()
        {
            Init();
            viewModel.Id = 2;
            viewModel.Name = "U";
            viewModel.CostRate = 10;
            viewModel.Availability = 20;
            viewModel.ModifiedData = new DateTime(2020, 2, 2);
            viewModel.ModifyLocation1.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationName(2), "U");
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 5);
            Assert.AreEqual(viewModel.ErrorMessage, "");
        }

        [TestMethod]
        public void UpdateTestFail()
        {
            Init();
            viewModel.Id = 20;
            viewModel.Name = "U";
            viewModel.CostRate = 10;
            viewModel.Availability = 20;
            viewModel.ModifiedData = new DateTime(2020, 2, 2);
            viewModel.ModifyLocation1.Execute(null);
            Assert.AreEqual(viewModel.DataRepository.GetLocationName(2), "B");
            Assert.AreEqual(viewModel.DataRepository.GetLocationsIds().Count, 5);
            Assert.AreNotEqual(viewModel.ErrorMessage, "");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using DL;
using DL.Interfaces;

namespace DataLayerTest
{
    [TestClass]
    public class DataLayerTest
    {
        private IDataLayerAPI _dataLayer;
        [TestMethod]
        public void InitTest()
        {
            _dataLayer = new LibraryRepository();
        }
    }
}

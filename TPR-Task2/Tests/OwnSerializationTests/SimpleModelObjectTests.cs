using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Model;
using TPR_Task2.Serialization;

namespace Tests.OwnSerializationTests
{
    [TestClass]
    public class SimpleModelObjectTests
    {
        [TestMethod]
        public void TestOwnFormatter()
        {
            A classA = new A(null, "KlasaA", 2);
            B classB = new B(null, "KlasaB", 1);
            C classC = new C(null, "KlasaC", new DateTime(2020,10,10));
        
            classA._classB = classB;
            classB._classC = classC;
            classC._classA = classA;
        
            OwnFormatter formatter = new OwnFormatter();
            using (Stream stream = File.Open("ownSerializ.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                formatter.Serialize(stream,classA);
            }

            A classACopy;
            using (Stream stream = File.Open("ownSerializ.txt", FileMode.Open, FileAccess.Read))
            {
                classACopy = (A)formatter.Deserialize(stream);
            }
            
            Assert.AreSame(classACopy._classB._classC._classA, classACopy);
        }
    }
}
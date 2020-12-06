using System;
using System.IO;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Model;

namespace Tests.XMLSerializationTest
{
    [TestClass]
    public class SimpleObjectSerializeToXML
    {

        [TestMethod]
        public void test()
        {
            D classD = new D(15.32, "Czesc", false);
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(D));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            using (Stream stream = File.Open("..\\..\\..\\xmlSerialization.xml", FileMode.Create, FileAccess.ReadWrite))
            {
                XmlWriter writer = XmlWriter.Create(stream, settings);
                writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"style.css\"");
                xmlSerializer.Serialize(writer, classD);
            }
        }
    }
}
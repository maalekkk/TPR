using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Tests.Model
{
    [Serializable]
    public class D : ISerializable
    {
        public double Number { get; set; }
        public string Name { get; set; }
        public bool IsTrue { get; set; }

        public D(double number, string name, bool isTrue)
        {
            Number = number;
            Name = name;
            IsTrue = isTrue;
        }

        public D() {}

        public D(SerializationInfo info, StreamingContext context)
        {
            Number = info.GetDouble("Number");
            Name = info.GetString("Name");
            IsTrue = info.GetBoolean("IsTrue");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("IsTrue", IsTrue);
        }
    }
}
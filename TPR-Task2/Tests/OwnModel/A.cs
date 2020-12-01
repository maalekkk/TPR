using System;
using System.Runtime.Serialization;

namespace Tests.Model
{
    [Serializable]
    public class A : ISerializable
    {
        public B _classB { get; set; }
        public string _name { get; set; }
        public int _number { get; set; }

        public A(B classB, string name, int number)
        {
            _classB = classB;
            _name = name;
            _number = number;
        }
        
        public A(SerializationInfo info, StreamingContext context)
        {
            _classB = (B)info.GetValue("classB", typeof(B));
            _name = info.GetString("name");
            _number = info.GetInt32("number");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("classB", _classB, typeof(B));
            info.AddValue("name", _name);
            info.AddValue("number", _number);
        }
    }
}
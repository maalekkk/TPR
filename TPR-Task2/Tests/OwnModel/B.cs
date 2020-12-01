using System;
using System.Runtime.Serialization;

namespace Tests.Model
{
    [Serializable]
    public class B : ISerializable
    {
        public C _classC { get; set; }
        public string _name { get; set; }
        public int _number { get; set; }

        public B(C classC, string name, int number)
        {
            _classC = classC;
            _name = name;
            _number = number;
        }
    
        public B(SerializationInfo info, StreamingContext context)
        {
            _classC = (C)info.GetValue("classC", typeof(C));
            _name = info.GetString("name");
            _number = info.GetInt32("number");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("classC", _classC, typeof(C));
            info.AddValue("name", _name);
            info.AddValue("number", _number);
        }
    }
}
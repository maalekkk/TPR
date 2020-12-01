using System;
using System.Runtime.Serialization;

namespace Tests.Model
{
    [Serializable]
    public class C : ISerializable
    {
        public A _classA { get; set; }
        public string _name { get; set; }
        public DateTime _date { get; set; }

        public C(A classA, string name, DateTime dateTime)
        {
            _classA = classA;
            _name = name;
            _date = dateTime;
        }
    
        public C(SerializationInfo info, StreamingContext context)
        {
            _classA = (A)info.GetValue("classA", typeof(A));
            _name = info.GetString("name");
            _date = info.GetDateTime("date");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("classA", _classA, typeof(A));
            info.AddValue("name", _name);
            info.AddValue("date", _date);
        }
    }
}
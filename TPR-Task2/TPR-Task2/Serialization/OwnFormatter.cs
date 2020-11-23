using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using DL.DataObjects;

namespace TPR_Task2.Serialization
{
    public class OwnFormatter : Formatter
    {
        public OwnFormatter()
        {
            RefIdGenerator = new ObjectIDGenerator();
        }
        
        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable serializableData)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                info.AddValue("$id", RefIdGenerator.GetId(graph, out firstTime));
                info.AddValue("$type", graph.GetType().FullName);
                StreamingContext context = new StreamingContext(StreamingContextStates.File);
                serializableData.GetObjectData(info, context);
                foreach (SerializationEntry entry in info)
                {
                    WriteMember(entry.Name, entry.Value);
                }
                fileContent += (char) 30 + "\n";
                byte[] content = Encoding.UTF8.GetBytes(fileContent);
                serializationStream.Write(content, 0, content.Length);
                fileContent = "";
                foreach (KeyValuePair<string, object> entry in entries)
                {
                    if (entry.Value is ISerializable && entry.Value != null &&
                        entry.Value.GetType() != typeof(DateTime) && entry.Value.GetType() != typeof(Guid) && firstTime)
                    {
                        Serialize(serializationStream, entry.Value);
                    }
                }

                if (!entries.ContainsValue(graph))
                {
                    entries = new Dictionary<string, object>();
                }
            }
            else 
            {
                throw new ArgumentException("Objects dont implement ISerializable");
            }
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(Dictionary<int, Book>)))
            {
                
            }
            else if (memberType.Equals(typeof(List<>)))
            {
                fileContent += name + "==[";
                foreach (object it in (object[])obj)
                {
                    fileContent += RefIdGenerator.GetId(it, out firstTime) + (char) 29;
                    entries.Add(name, it);
                }
                fileContent += "]";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected override void WriteBoolean(bool val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            fileContent += name + "==" + val.ToString("g", CultureInfo.CreateSpecificCulture("fr-FR")) + (char)31;
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            fileContent += name + "==" + val.ToString() + (char)31;
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            fileContent += name + "==" + val.ToString() + (char)31;
        }

        protected override void WriteInt64(long val, string name)
        {
            fileContent += name + "==" + val.ToString() + (char)31;
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(string)))
            {
                fileContent += name + "==" + (string) obj + (char) 31;
            }
            else
            {
                fileContent += name + "==" + RefIdGenerator.GetId(obj, out firstTime) + (char) 31;
                entries.Add(name, obj);
            }
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            fileContent += name + "==" + obj.ToString() + (char) 31;
        }

        public override SerializationBinder Binder { get; set; }
        public override StreamingContext Context { get; set; }
        public override ISurrogateSelector SurrogateSelector { get; set; }
        public ObjectIDGenerator RefIdGenerator { get; set; }

        bool firstTime;
        private string fileContent;
        private Dictionary<string, object> entries = new Dictionary<string, object>();
    }
}
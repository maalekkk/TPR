using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        
        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable serializableData)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                info.AddValue("$id", RefIdGenerator.GetId(graph, out _firstTime));
                info.AddValue("$type", graph.GetType().FullName);
                Context = new StreamingContext(StreamingContextStates.File);
                serializableData.GetObjectData(info, Context);
                foreach (SerializationEntry entry in info)
                {
                    WriteMember(entry.Name, entry.Value);
                }
                
                _fileContent += rowSeparator;

                while (m_objectQueue.Count != 0)
                {
                    Serialize(null,this.m_objectQueue.Dequeue());
                }

                if (m_objectQueue.Count == 0 && serializationStream != null)
                {
                    WriteDataToFile(_fileContent, serializationStream);
                }
            }
            else 
            {
                throw new ArgumentException("Objects dont implement ISerializable");
            }
        }

        public override object Deserialize(Stream serializationStream)
        {
            StreamReader streamReader = new StreamReader(serializationStream ?? throw new ArgumentNullException(nameof(serializationStream)));
            string serializedText = streamReader.ReadToEnd();
            Context = new StreamingContext();
            List<string> serializedObject = serializedText.Split(rowSeparator).ToList();
            
            // id , object
            Dictionary<string, object> createdObjects = new Dictionary<string, object>();
            // object_id, id_ref
            Dictionary<string, List<string>> referencesToObject = new Dictionary<string, List<string>>();
            
            for (int i = 0; i < serializedObject.Count-1; i++)
            {
                List<string> fields = new List<string>();
                foreach (string field in serializedObject[i].Split(fieldSeparator))
                {
                    fields.AddRange(field.Split(fieldInfoSeparator).ToList());
                }
                
                SerializationInfo info = new SerializationInfo(GetTypeFromString(fields[5]), new FormatterConverter());
                List<string> referencesInCurrentObject = new List<string>();
                
                for (int j = 6; j < fields.Count-2; j += 3)
                {
                    if (fields[j].Equals("System.DateTime"))
                    {
                        info.AddValue(fields[j+1], DateTime.Parse(fields[j+2]).ToLocalTime());
                    }
                    else if (fields[j].Equals("System.Guid"))
                    {
                        info.AddValue(fields[j+1], Guid.Parse(fields[j+2]));
                    }
                    // else if ((fields[j].StartsWith("DL")) && (fields[j].Contains("+")))
                    // {
                    //     object enumField;
                    //     Enum.TryParse(GetTypeFromString(fields[j]), fields[j + 2], out enumField);
                    //     info.AddValue(fields[j+1], enumField);
                    // }
                    else if (fields[j].StartsWith("Test"))
                    {
                        info.AddValue(fields[j+1], null);
                        referencesInCurrentObject.Add(fields[j+2]);
                    }
                    else
                    {
                        info.AddValue(fields[j+1], fields[j+2]);
                    }
                }
                if (referencesInCurrentObject.Count != 0)
                    referencesToObject.Add(fields[2], referencesInCurrentObject);
                createdObjects.Add(fields[2], Activator.CreateInstance(GetTypeFromString(fields[5]), info, Context));
            }
            PutReferencesToObject(createdObjects, referencesToObject);
            return createdObjects.First().Value;
        }

        private Type GetTypeFromString(string type)
        {
            return Type.GetType(type + ", " + type.Split(".").ToList()[0]);
        }

        private void PutReferencesToObject(Dictionary<string, object> createdObjects, Dictionary<string, List<string>> referencesToObject)
        {
            foreach (string id in referencesToObject.Keys)
            {
                foreach (string referenceId in referencesToObject[id])
                {
                    foreach (PropertyInfo propertyInfo in createdObjects[id].GetType().GetProperties())
                    {
                        if (propertyInfo.PropertyType == createdObjects[referenceId].GetType())
                        {
                            propertyInfo.SetValue(createdObjects[id], createdObjects[referenceId]);
                        }
                    }
                }
            }
        }

        private void WriteDataToFile(string fileContent, Stream serializationStream)
        {
            byte[] content = Encoding.UTF8.GetBytes(fileContent);
            serializationStream.Write(content, 0, content.Length);
        }
        

        protected override void WriteDateTime(DateTime val, string name)
        {
            _fileContent += val.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + val.ToString("g", CultureInfo.CreateSpecificCulture("fr-FR")) + fieldSeparator;
        }

        protected override void WriteDouble(double val, string name)
        {
            _fileContent += val.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + val.ToString() + fieldSeparator;
        }

        protected override void WriteInt32(int val, string name)
        {
            _fileContent += val.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + val.ToString() + fieldSeparator;
        }

        protected override void WriteInt64(long val, string name)
        {
            _fileContent += val.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + val.ToString() + fieldSeparator;
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(string)))
            {
                _fileContent += obj.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + (string) obj + fieldSeparator;
            }
            else
            {
                _fileContent += obj.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + RefIdGenerator.GetId(obj, out _firstTime) + fieldSeparator;
                if (_firstTime)
                {
                    this.m_objectQueue.Enqueue(obj);
                }
            }
        }
        
        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            _fileContent += obj.GetType() + fieldInfoSeparator + name + fieldInfoSeparator + obj.ToString() +
                            fieldSeparator;
        }
        
        protected override void WriteArray(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(Dictionary<int, Book>)))
            {
                
            }
            else if (memberType.Equals(typeof(List<>)))
            {
                _fileContent += name + fieldInfoSeparator + '[';
                foreach (object it in (object[])obj)
                {
                    _fileContent += RefIdGenerator.GetId(it, out _firstTime) + (char) 29;
                    if (_firstTime)
                    {
                        this.m_objectQueue.Enqueue(it);
                    }
                }
                _fileContent += ']';
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        
        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }
        
        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
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

        public override SerializationBinder Binder { get; set; }
        public override StreamingContext Context { get; set; }
        public override ISurrogateSelector SurrogateSelector { get; set; }
        public ObjectIDGenerator RefIdGenerator { get; set; }

        private bool _firstTime;
        private string _fileContent;
        private string rowSeparator = (char) 30 + "\n";
        private char fieldSeparator = (char) 31;
        private string fieldInfoSeparator = "==";
        
    }
}
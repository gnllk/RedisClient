using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace Gnllk.JCommon.Helper
{
    public static class XmlHelper
    {
        public static string ToXml(object entity)
        {
            if (null != entity)
            {
                XmlSerializer serializer = new XmlSerializer(entity.GetType());
                using (MemoryStream ms = new MemoryStream(1024))
                {
                    TextWriter writer = new StreamWriter(ms, Encoding.UTF8);
                    serializer.Serialize(writer, entity);
                    ms.Position = 0;
                    TextReader reader = new StreamReader(ms);
                    return reader.ReadToEnd();
                }
            }
            return null;
        }

        public static string ToXml3(object entity, Encoding encoding = null)
        {
            if (null != entity)
            {
                if (encoding == null) encoding = Encoding.UTF8;
                DataContractSerializer serializer = new DataContractSerializer(entity.GetType());
                using (MemoryStream ms = new MemoryStream(1024))
                {
                    serializer.WriteObject(ms, entity);
                    ms.Position = 0;
                    TextReader reader = new StreamReader(ms);
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }

        public static string ToXml2(object entity, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            List<Member> list = GetObjectMember(entity);
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(ms, new XmlWriterSettings() { Indent = true });
                WriteElement(writer, "Object", list, 1);
                writer.Flush();
                return encoding.GetString(ms.ToArray());
            }
        }

        private static void WriteElement(XmlWriter writer, string name, List<Member> list, int deep)
        {
            if (writer == null || string.IsNullOrWhiteSpace(name)) return;
            writer.WriteStartElement(XmlEncode(name));
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.MType == MemberType.Final)
                    {
                        writer.WriteAttributeString(XmlEncode(item.MName), string.Format("{0}", item.MObject));
                    }
                    else if (item.MType == MemberType.List)
                    {
                        List<Member> temp = GetObjectMember(item.MObject);
                        foreach (var m in temp)
                        {
                            writer.WriteStartElement(XmlEncode(item.MName));
                            WriteElement(writer, m.MName, GetObjectMember(m.MObject), ++deep);
                            writer.WriteEndElement();
                        }
                    }
                    else if (item.MType == MemberType.Object)
                    {
                        WriteElement(writer, item.MName, GetObjectMember(item.MObject), ++deep);
                    }
                }
            }
            writer.WriteEndElement();
        }

        private static MemberType GetMemberType(object obj)
        {
            if (obj == null) return MemberType.Unkown;
            if (IsFinalValue(obj.GetType())) return MemberType.Final;
            if (obj is IEnumerable) return MemberType.List;
            return MemberType.Object;
        }
        private static bool IsFinalValue(Type type)
        {
            return (type.IsValueType
                || type.IsPointer
                || type.IsPrimitive
                || type.IsEnum
                || type == typeof(string));
        }
        private static List<Member> GetObjectMember(object obj)
        {
            List<Member> list = new List<Member>();
            if (obj == null) return list;
            else if (obj is IEnumerable)
            {
                int i = 0;
                IEnumerable objList = obj as IEnumerable;
                foreach (var item in objList)
                {
                    string name = string.Format("Index_{0}", i++.ToString());
                    list.Add(new Member()
                    {
                        MName = name,
                        MObject = item,
                        MType = IsFinalValue(item.GetType()) ? MemberType.Final : MemberType.Object
                    });
                }
            }
            else
            {
                Type objType = obj.GetType();
                PropertyInfo[] ps = objType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                foreach (var item in ps)
                {
                    var temp = item.GetValue(obj, null);
                    AddMemberTo(list, item, temp);
                }
                FieldInfo[] fs = objType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                foreach (var item in fs)
                {
                    var temp = item.GetValue(obj);
                    AddMemberTo(list, item, temp);
                }
            }
            return list;
        }
        private static void AddMemberTo(List<Member> list, MemberInfo info, object mObject)
        {
            list.Add(new Member()
            {
                MName = info.Name,
                MObject = mObject,
                MType = GetMemberType(mObject)
            });
        }

        private static string XmlEncode(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str.Replace("&", "&amp").Replace("<", "&lt").Replace(">", "&gt").Replace("\"", "&quot").Replace("\'", "&apos");
            }
            return str;
        }


        public static T FromXml<T>(string xml) where T : class
        {
            T entity = default(T);
            if (!string.IsNullOrWhiteSpace(xml))
            {
                XmlSerializer serializer = new XmlSerializer(entity.GetType());
                using (MemoryStream ms = new MemoryStream(1024))
                {
                    TextWriter writer = new StreamWriter(ms, Encoding.UTF8);
                    writer.Write(xml);
                    writer.Flush();
                    ms.Position = 0;
                    entity = serializer.Deserialize(ms) as T;
                }
            }
            return entity;
        }
    }

    internal enum MemberType
    {
        Unkown, Final, Object, List
    }

    internal class Member
    {
        public MemberType MType { get; set; }
        public string MName { get; set; }
        public object MObject { get; set; }
    }
}

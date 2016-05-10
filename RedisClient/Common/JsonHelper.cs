using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Gnllk.RedisClient.Common
{
    public static class JsonHelper
    {
        public static string ToJson(object entity)
        {
            if (null != entity)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(entity.GetType());
                using (MemoryStream ms = new MemoryStream(1024))
                {
                    serializer.WriteObject(ms, entity);
                    ms.Position = 0;
                    TextReader reader = new StreamReader(ms);
                    return reader.ReadToEnd();
                }
            }
            return null;
        }

        public static T FromJson<T>(string json) where T : class
        {
            T entity = default(T);
            if (!string.IsNullOrEmpty(json) && json.Length > 2)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(1024))
                {
                    TextWriter writer = new StreamWriter(ms);
                    writer.Write(json);
                    writer.Flush();
                    ms.Position = 0;
                    entity = serializer.ReadObject(ms) as T;
                }
            }
            return entity;
        }
    }
}

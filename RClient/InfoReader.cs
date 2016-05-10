using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RClient
{
    public class InfoReader
    {
        public static Info ReadAsInfo(byte[] data)
        {
            Info result = new Info();
            byte[] content = Readers.ReadAsBytes(data);
            if (content == null) throw new Exception("Read large content fail, the result is null");
            using (Stream stream = new MemoryStream(content))
            {
                TextReader reader = new StreamReader(stream);
                string line = null;
                Section sect = null;
                while (null != (line = reader.ReadLine()))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("#"))
                    {
                        sect = new Section();
                        result.Add(line, sect);
                    }
                    else
                    {
                        string[] spl = line.Split(':');
                        sect.Add(spl[0], spl[1]);
                    }
                }
            }
            return result;
        }
    }

    public class Info : Dictionary<string, Section>
    {
    }

    public class Section : Dictionary<string, string>
    {
    }
}

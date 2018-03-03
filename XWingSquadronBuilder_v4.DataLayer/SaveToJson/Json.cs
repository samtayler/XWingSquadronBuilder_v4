using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace XWingSquadronBuilder_v4.DataLayer.SaveToJson
{
    public class Json
    {
        public static Task<string> Serialise<T>(object o)
        {
            return Task.Run<string>(() =>
            {
                using (var textStream = new MemoryStream())
                {
                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(T));
                    mySerializer.WriteObject(textStream, o);
                    return new StreamReader(textStream).ReadToEnd();
                }
            });
        }

        public static Task<T> Deserialise<T>(string s) where T : class
        {
            return Task.Run<T>(() =>
            {
                T obj;
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    obj = ser.ReadObject(ms) as T;
                    return obj;
                }
            });
        }
    }
}

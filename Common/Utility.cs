using System;
using System.Web.Script.Serialization;

namespace Common
{
    public class Utility
    {
        public static string JsonSerialize(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(obj);
        }

        public static T Deserialize<T>(string jsonData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T result = serializer.Deserialize<T>(jsonData);
            return result;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LsqParserEngine.Common.Convertor
{
    //类型转换类
    public class Convertor
    {
        public static object Add(object V1, object V2)
        {
            DateTime time;
            if ((V1 == null) || (V1 == DBNull.Value))
            {
                return V2;
            }
            if ((V2 == null) || (V2 == DBNull.Value))
            {
                return V1;
            }
            if (V1 is Array)
            {
                return AddToArray((Array)V1, V2);
            }
            if (V2 is Array)
            {
                return AddToArray((Array)V2, V1);
            }
            if ((V1 is DateTime) && (V2 is TimeSpan))
            {
                time = (DateTime)V1;
                return time.Add((TimeSpan)V2);
            }
            if ((V2 is DateTime) && (V1 is TimeSpan))
            {
                time = (DateTime)V2;
                return time.Add((TimeSpan)V1);
            }
            if (V1.GetType() != V2.GetType())
            {
                throw new ArgumentException("The 2 values can not be added, which are " + V1.ToString() + " and " + V2.ToString());
            }
            if (V1 is int)
            {
                return (((int)V1) + ((int)V2));
            }
            if (V1 is long)
            {
                return (((long)V1) + ((long)V2));
            }
            if (V1 is float)
            {
                return (((float)V1) + ((float)V2));
            }
            if (V1 is double)
            {
                return (((double)V1) + ((double)V2));
            }
            if (V1 is byte)
            {
                return (((byte)V1) + ((byte)V2));
            }
            if (V1 is decimal)
            {
                return (((decimal)V1) + ((decimal)V2));
            }
            if (V1 is string)
            {
                return (((string)V1) + ((string)V2));
            }
            if (V1 is bool)
            {
                return (((bool)V1) ? ((object)1) : ((object)((bool)V2)));
            }
            if (!(V1 is TimeSpan))
            {
                throw new Exception("The type " + V1.GetType().FullName + " can not be added.");
            }
            return (((TimeSpan)V1) + ((TimeSpan)V2));
        }

        private static Array AddToArray(Array V1, object V2)
        {
            int num;
            Array array = V1;
            ConstructorInfo constructor = V1.GetType().GetConstructor(new Type[] { typeof(int) });
            Array array2 = null;
            if (V2 is Array)
            {
                Array array3 = (Array)V2;
                array2 = (Array)constructor.Invoke(new object[] { array.Length + array3.Length });
                for (num = 0; num < array.Length; num++)
                {
                    array2.SetValue(array.GetValue(num), num);
                }
                for (num = 0; num < array3.Length; num++)
                {
                    array2.SetValue(array3.GetValue(num), (int)(num + array.Length));
                }
                return array2;
            }
            array2 = (Array)constructor.Invoke(new object[] { array.Length + 1 });
            for (num = 0; num < array.Length; num++)
            {
                array2.SetValue(array.GetValue(num), num);
            }
            array2.SetValue(V2, array.Length);
            return array2;
        }


        /// <summary>
        /// 二进制反序列化：byte=>实体object
        /// </summary>
        /// <param name="SerializedObj"></param>
        /// <param name="ThrowException"></param>
        /// <returns></returns>
        public static object ByteArrayToObject(byte[] SerializedObj, bool ThrowException)
        {
            if (SerializedObj == null)
            {
                return null;
            }
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream(SerializedObj);
                return formatter.Deserialize(serializationStream);
            }
            catch (Exception exception)
            {
                if (ThrowException)
                {
                    throw exception;
                }
                return null;
            }
        }

        public static bool CanStatistic(Type Type)
        {
            return ((((Type == typeof(long)) || (Type == typeof(bool))) || ((Type == typeof(double)) || (Type == typeof(int)))) || (Type == typeof(decimal)));
        }

        public static bool Check(string Type, object Value)
        {
            if (Type == null)
            {
                return false;
            }
            if (Type != typeof(object).FullName)
            {
                if (Type == typeof(decimal).FullName)
                {
                    decimal result = 0M;
                    return decimal.TryParse(Value + string.Empty, out result);
                }
                if (Type == typeof(long).FullName)
                {
                    long num2 = 0L;
                    return long.TryParse(Value + string.Empty, out num2);
                }
                if (Type == typeof(int).FullName)
                {
                    int num3 = 0;
                    return int.TryParse(Value + string.Empty, out num3);
                }
                if (Type == typeof(short).FullName)
                {
                    short num4 = 0;
                    return short.TryParse(Value + string.Empty, out num4);
                }
                if (Type == typeof(byte).FullName)
                {
                    byte num5 = 0;
                    return byte.TryParse(Value + string.Empty, out num5);
                }
                if (Type == typeof(double).FullName)
                {
                    double num6 = 0.0;
                    return double.TryParse(Value + string.Empty, out num6);
                }
                if (Type == typeof(float).FullName)
                {
                    float num7 = 0f;
                    return float.TryParse(Value + string.Empty, out num7);
                }
                if (Type == typeof(DateTime).FullName)
                {
                    DateTime minValue = DateTime.MinValue;
                    return DateTime.TryParse(Value + string.Empty, out minValue);
                }
                if (Type == typeof(bool).FullName)
                {
                    bool flag = false;
                    return bool.TryParse(Value + string.Empty, out flag);
                }
                if (Type == typeof(TimeSpan).FullName)
                {
                    TimeSpan span = new TimeSpan(1, 0, 0);
                    return TimeSpan.TryParse(Value + string.Empty, out span);
                }
                if (Type == typeof(char).FullName)
                {
                    char ch = '\0';
                    return char.TryParse(Value + string.Empty, out ch);
                }
                if (Type == typeof(Guid).FullName)
                {
                    if ((Value == null) || (Value == DBNull.Value))
                    {
                        return true;
                    }
                    if (Value is Guid)
                    {
                        return true;
                    }
                    if (Value is string)
                    {
                        if (string.IsNullOrEmpty((string)Value))
                        {
                            return true;
                        }
                        try
                        {
                            Guid guid = new Guid((string)Value);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    return false;
                }
                if ((Value != null) && (Type != Value.GetType().FullName))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Check(Type Type, object Value)
        {
            return Check(Type.FullName, Value);
        }

        public static object Clone(object Obj)
        {
            if (Obj == null)
            {
                return null;
            }
            return ByteArrayToObject(ObjectToByteArray(Obj, true), true);
        }

        public static T Convert<T>(object Source)
        {
            return (T)Convert(Source, typeof(T));
        }

        public static object Convert(object Source, Type ConversionType)
        {
            return Convert(Source, ConversionType, true);
        }

        public static bool Convert(object Source, Type ConversionType, ref object Result)
        {
            try
            {
                Result = Convert(Source, ConversionType, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object Convert(object Source, Type ConversionType, bool ThrowException)
        {
            if (ConversionType == typeof(object))
            {
                return Source;
            }
            if ((Source != null) && (Source.GetType() == ConversionType))
            {
                return Source;
            }
            if (((Source == null) || (Source == DBNull.Value)) || (Source == ""))
            {
                return null;
            }
            if (Source.GetType() == ConversionType)
            {
                return Source;
            }
            try
            {
                Array array;
                int num;
                if (ConversionType == typeof(DateTime))
                {
                    if (Source == "0000-00-00")
                    {
                        return new DateTime();
                    }
                    if ((Source == null) || (Source == DBNull.Value))
                    {
                        return DateTime.MinValue;
                    }
                    return DateTime.Parse(Source.ToString());
                }
                if (ConversionType == typeof(TimeSpan))
                {
                    return TimeSpan.Parse(Source.ToString().Replace('$', '.'));
                }
                if (ConversionType == typeof(Guid))
                {
                    if (Source is byte[])
                    {
                        return new Guid((byte[])Source);
                    }
                    if (string.IsNullOrEmpty(Source.ToString()))
                    {
                        return null;
                    }
                    return new Guid(Source.ToString());
                }
                if (ConversionType == typeof(bool))
                {
                    if ((((Source == null) || ((Source is int) && (((int)Source) == 0))) || ((Source is string) && ((((string)Source) == "0") || (((string)Source).ToLower() == "false")))) || ((Source.ToString() == "0") || (Source.ToString().ToLower() == "false")))
                    {
                        return false;
                    }
                    if (((((Source == null) || !(Source is int)) || (((int)Source) != 1)) && (!(Source is string) || ((((string)Source) != "1") && (((string)Source).ToLower() != "true")))) && ((Source.ToString() != "1") && (Source.ToString().ToLower() != "true")))
                    {
                        throw new InvalidCastException("Can't convert \"" + Source + "\" to System.Boolean.");
                    }
                    return true;
                }
                if (ConversionType == typeof(char))
                {
                    if ((Source == null) || (Source == ""))
                    {
                        return '\0';
                    }
                    if (Source is string)
                    {
                        string str = (string)Source;
                        if (str.Length > 1)
                        {
                            throw new ArgumentOutOfRangeException("Can not convert \"" + str + "\" to a char.");
                        }
                        return str[0];
                    }
                    return (char)Source;
                }
                if (ConversionType == typeof(char[]))
                {
                    if (Source == null)
                    {
                        return null;
                    }
                    if (Source is string)
                    {
                        return ((string)Source).ToCharArray();
                    }
                    if (Source is Array)
                    {
                        array = (Array)Source;
                        char[] chArray = new char[array.Length];
                        for (num = 0; num < array.Length; num++)
                        {
                            chArray[num] = (char)Convert(array.GetValue(num), typeof(char));
                        }
                        return chArray;
                    }
                    return System.Convert.ChangeType(Source, ConversionType);
                }
                if (ConversionType == typeof(byte))
                {
                    if (Source == null)
                    {
                        return 0;
                    }
                    if (Source is string)
                    {
                        return byte.Parse((string)Source);
                    }
                    return (byte)Source;
                }
                if (ConversionType == typeof(short))
                {
                    if (Source == null)
                    {
                        return 0;
                    }
                    if (Source is string)
                    {
                        return short.Parse((string)Source);
                    }
                    return (short)Source;
                }
                if (ConversionType == typeof(string))
                {
                    if (Source == null)
                    {
                        return null;
                    }
                    if (Source is char[])
                    {
                        return new string((char[])Source);
                    }
                    return Source.ToString();
                }
                if (ConversionType.IsEnum)
                {
                    if (Source is int)
                    {
                        int num2 = (int)Source;
                        return Enum.ToObject(ConversionType, num2);
                    }
                    return Enum.Parse(ConversionType, Source.ToString());
                }
                if (ConversionType.IsArray && ((Source is string) && (((string)Source) == "")))
                {
                    return null;
                }
                if (ConversionType.IsArray)
                {
                    Array array2;
                    Type elementType;
                    object obj3;
                    if (Source == null)
                    {
                        return null;
                    }
                    if (Source is Array)
                    {
                        array = (Array)Source;
                        array2 = (Array)ConversionType.GetConstructor(new Type[] { typeof(int) }).Invoke(new object[] { array.Length });
                        elementType = ConversionType.GetElementType();
                        for (num = 0; num < array.Length; num++)
                        {
                            obj3 = Convert(array.GetValue(num), elementType);
                            array2.SetValue(obj3, num);
                        }
                        return array2;
                    }
                    array2 = (Array)ConversionType.GetConstructor(new Type[] { typeof(int) }).Invoke(new object[] { 1 });
                    elementType = ConversionType.GetElementType();
                    obj3 = Convert(Source, elementType);
                    array2.SetValue(obj3, 0);
                    return array2;
                }
                if (ConversionType.FullName[ConversionType.FullName.Length - 1] == '&')
                {
                    Type type = Type.GetType(ConversionType.FullName.Substring(0, ConversionType.FullName.Length - 1));
                    return Convert(Source, type, ThrowException);
                }
                return System.Convert.ChangeType(Source, ConversionType);
            }
            catch (Exception exception)
            {
                if (ThrowException)
                {
                    throw exception;
                }
                return GetDefaultValue(ConversionType);
            }
        }

        public static string CreateShortGuid()
        {
            return ToShortGuid(Guid.NewGuid());
        }

        public static string Format(DateTime Time)
        {
            return string.Concat(new object[] { Time.Year, "/", Time.Month, "/", Time.Day, " ", Time.Hour, ":", Time.Minute });
        }

        public static double GetDays(TimeSpan Span)
        {
            return (((Span.Days + (((double)Span.Hours) / 24.0)) + ((((double)Span.Minutes) / 24.0) / 60.0)) + (((((double)Span.Seconds) / 24.0) / 60.0) / 60.0));
        }

        public static string GetDefaultStringValue(Type Type)
        {
            if (Type == typeof(bool))
            {
                return "false";
            }
            object defaultValue = GetDefaultValue(Type);
            if (defaultValue == null)
            {
                return "";
            }
            return defaultValue.ToString();
        }

        public static object GetDefaultValue(Type Type)
        {
            if (Type != typeof(string))
            {
                if (Type == typeof(DateTime))
                {
                    return DateTime.MinValue;
                }
                if (Type == typeof(bool))
                {
                    return false;
                }
                if ((Type == typeof(double)) || (Type == typeof(float)))
                {
                    return 0.0;
                }
                if ((((((Type == typeof(long)) || (Type == typeof(int))) || ((Type == typeof(byte)) || (Type == typeof(short)))) || (((Type == typeof(short)) || (Type == typeof(long))) || ((Type == typeof(ushort)) || (Type == typeof(ulong))))) || ((Type == typeof(uint)) || (Type == typeof(sbyte)))) || (Type == typeof(decimal)))
                {
                    return 0;
                }
                if (Type == typeof(TimeSpan))
                {
                    return new TimeSpan(0L);
                }
                if (Type == typeof(DataTable))
                {
                    return null;
                }
                if (Type == typeof(SqlDateTime))
                {
                    return SqlDateTime.MinValue;
                }
                if (Type == typeof(Guid))
                {
                    return null;
                }
            }
            return null;
        }

        public static bool IsJsonString(string text)
        {
            return !string.IsNullOrWhiteSpace(text) &&
                ((text[0] == '[' && text[text.Length - 1] == ']') || (text[0] == '{' && text[text.Length - 1] == '}'));
        }

        public static bool IsSearchable(Type Type)
        {
            return ((((((Type == typeof(long)) || (Type == typeof(bool))) || ((Type == typeof(double)) || (Type == typeof(int)))) || (((Type == typeof(Guid)) || (Type == typeof(decimal))) || ((Type == typeof(string)) || (Type == typeof(byte))))) || (Type == typeof(float))) || (Type == typeof(DateTime)));
        }

        public static bool IsSerializable(object Obj)
        {
            if (Obj == null)
            {
                return true;
            }
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream();
                formatter.Serialize(serializationStream, Obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 二进制序列化：实体=》byte
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="ThrowException"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object Obj, bool ThrowException)
        {
            if (Obj == null)
            {
                return null;
            }
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream();
                formatter.Serialize(serializationStream, Obj);
                return serializationStream.ToArray();
            }
            catch (Exception exception)
            {
                if (ThrowException)
                {
                    throw exception;
                }
                return null;
            }
        }

        public static string ObjectToXml(object Obj)
        {
            if (Obj == null || string.IsNullOrEmpty(Obj.ToString()))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(Obj.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            serializer.Serialize((TextWriter)writer, Obj);
            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());
            return document.ChildNodes[1].OuterXml;
        }

        public static string SqlInjectionPrev(string InputValue)
        {
            if (InputValue == null)
            {
                return null;
            }
            return InputValue.Replace("'", string.Empty).Replace("--", string.Empty);
        }

        private static char ToBase32Char(int Value)
        {
            char ch;
            int num;
            int num2;
            if (Value < 10)
            {
                ch = '0';
                num = ch;
                num2 = Value + num;
                return (char)num2;
            }
            if (Value >= 0x20)
            {
                throw new NotImplementedException();
            }
            ch = 'a';
            num = ch;
            num2 = (Value + num) - 10;
            return (char)num2;
        }

        public static string ToBase32String(byte Value)
        {
            return ToBase32String((long)Value);
        }

        public static string ToBase32String(long Value)
        {
            if (Value < 0L)
            {
                return ("-" + ToUnsignedBase32String(-Value));
            }
            if (Value == 0L)
            {
                return "0";
            }
            return ToUnsignedBase32String(Value);
        }

        private static string ToBase32String(long Value, int Length)
        {
            string str = ToBase32String(Value);
            int num = Length - str.Length;
            for (int i = 0; i < num; i++)
            {
                str = str.Insert(0, "0");
            }
            return str;
        }

        public static string ToShortGuid(Guid Guid)
        {
            string str;
            byte[] buffer = Guid.ToByteArray();
            StringBuilder builder = new StringBuilder();
            long num = 0L;
            for (int i = 0; i < 15; i += 5)
            {
                num = (((buffer[i] * 0x100) * 0x10) + (buffer[i + 1] * 0x10)) + (buffer[i + 2] / 0x10);
                str = ToBase32String(num, 4);
                builder.Append(str);
                num = ((((buffer[i + 2] % 0x10) * 0x100) * 0x100) + (buffer[i + 3] * 0x100)) + buffer[i + 4];
                str = ToBase32String(num, 4);
                builder.Append(str);
            }
            str = ToBase32String((long)buffer[15], 2);
            builder.Append(str);
            return builder.ToString();
        }

        private static string ToUnsignedBase32String(long value)
        {
            if (value < 0L)
            {
                throw new NotImplementedException();
            }
            StringBuilder builder = new StringBuilder();
            for (long i = value; i > 0L; i /= 0x20L)
            {
                int num2 = (int)(i % 0x20L);
                char ch = ToBase32Char(num2);
                builder.Insert(0, ch);
            }
            if (builder.Length == 0)
            {
                return "0";
            }
            return builder.ToString();
        }

        public static void WriteXml(XmlWriter writer, string name, object value)
        {
            writer.WriteStartElement(name);
            if (value != null)
            {
                writer.WriteValue(value);
            }
            writer.WriteEndElement();
        }

        public static T XmlToObject<T>(string xml)
        {
            return (T)XmlToObject(typeof(T), xml);
        }

        public static object XmlToObject(Type type, string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return GetDefaultValue(type);
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                StringReader textReader = new StringReader(xml);
                return serializer.Deserialize(textReader);
            }
            catch (Exception exception)
            {
                if ((type != typeof(string)) && !type.IsSubclassOf(typeof(string)))
                {
                    // throw exception;
                }
                return null;
            }
        }

        public static object JsonToObject(Type type, string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return GetDefaultValue(type);
            }
            try
            {
                if (!IsJsonString(json))
                {
                    return GetDefaultValue(type);
                }
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
                return JsonConvert.DeserializeObject(json, jsonSerializerSettings);
            }
            catch (Exception exception)
            {
                if ((type != typeof(string)) && !type.IsSubclassOf(typeof(string)))
                {
                    //throw exception;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(DateTime.Now.ToString() + " : " + $"JsonToObject:【ERROR】:{exception.Message}; 【JSON】:{json}");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                return json;
            }
        }
        public static T JsonToObject<T>(string json)
        {
            return (T)JsonToObject(typeof(T), json);
        }

        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}

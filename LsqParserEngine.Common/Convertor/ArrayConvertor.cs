using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsqParserEngine.Common.Convertor
{
    /// <summary>
    /// 数组处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayConvertor<T>
    {
        public const char Separator = ';';

        public static T[] Add(T[] a1, T[] a2)
        {
            int num;
            if (a1 == null)
            {
                return a2;
            }
            if (a2 == null)
            {
                return a1;
            }
            T[] localArray = new T[a1.Length + a2.Length];
            for (num = 0; num < a1.Length; num++)
            {
                localArray[num] = a1[num];
            }
            for (num = 0; num < a2.Length; num++)
            {
                localArray[a1.Length + num] = a2[num];
            }
            return localArray;
        }

        public static T[] AddToArray(T[] array, T item)
        {
            if (array != null)
            {
                T[] localArray = new T[array.Length + 1];
                for (int i = 0; i < array.Length; i++)
                {
                    localArray[i] = array[i];
                }
                localArray[localArray.Length - 1] = item;
                return localArray;
            }
            return new T[] { item };
        }

        public static string ArrayToString(T[] array)
        {
            return ArrayConvertor<T>.ArrayToString(array, ';');
        }

        public static string ArrayToString(T[] array, char separator)
        {
            if (array == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            foreach (T local in array)
            {
                if (local != null)
                {
                    builder.Append(local.ToString() + separator);
                }
            }
            return builder.ToString();
        }

        public static bool Compare(T[] array1, T[] array2)
        {
            if ((array1 != null) || (array2 != null))
            {
                if ((array1 == null) || (array2 == null))
                {
                    return false;
                }
                if (array1.Length != array2.Length)
                {
                    return false;
                }
                Hashtable hashtable = new Hashtable();
                foreach (T local in array1)
                {
                    hashtable.Add(local, local);
                }
                foreach (T local in array2)
                {
                    if (!hashtable.Contains(local))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Contains(T[] array, T item)
        {
            if ((array != null) && (array.Length != 0))
            {
                foreach (T local in array)
                {
                    if (local.Equals(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Contains(string[] array, string item, bool ignoreCase)
        {
            if ((array != null) && (array.Length != 0))
            {
                foreach (string str in array)
                {
                    if (string.Compare(str, item, ignoreCase) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static T2[] Convert<T2>(T[] a)
        {
            T2[] result;
            if (a == null)
            {
                result = null;
            }
            else
            {
                T2[] array = new T2[a.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (T2)((object)a[i]);
                }
                result = array;
            }
            return result;
        }

        public static T[] RemoveDuplicate(T[] a)
        {
            if ((a == null) || (a.Length == 0))
            {
                return a;
            }
            List<T> list = new List<T>();
            foreach (T local in a)
            {
                if (!((local == null) || list.Contains(local)))
                {
                    list.Add(local);
                }
            }
            return list.ToArray();
        }

        public static T[] RemoveFromArray(T[] a, int index)
        {
            if (a == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            for (int i = 0; i < a.Length; i++)
            {
                if (i != index)
                {
                    list.Add(a[i]);
                }
            }
            return list.ToArray();
        }

        public static T[] RemoveFromArray(T[] a, int index, int length)
        {
            if (a == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            for (int i = 0; i < a.Length; i++)
            {
                if ((i < index) || (i >= (index + length)))
                {
                    list.Add(a[i]);
                }
            }
            return list.ToArray();
        }

        public static T[] StringToArray(string ArrayString)
        {
            return ArrayConvertor<T>.StringToArray(ArrayString, ';');
        }

        public static T[] StringToArray(string arrayString, char separator)
        {
            if (arrayString == null)
            {
                return null;
            }
            string[] strArray = arrayString.Split(new char[] { separator });
            if (strArray == null)
            {
                return new T[0];
            }
            ArrayList list = new ArrayList();
            foreach (string str in strArray)
            {
                if ((str != null) && (str != ""))
                {
                    object obj2 = Convertor.Convert(str, typeof(T));
                    list.Add(obj2);
                }
            }
            return ArrayConvertor<T>.ToArray(list);
        }

        public static T[] ToArray(ArrayList list)
        {
            if (list == null)
            {
                return null;
            }
            T[] localArray = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                localArray[i] = (T)list[i];
            }
            return localArray;
        }

        public static ArrayList ToArrayList(T[] array)
        {
            ArrayList list = new ArrayList();
            if (array != null)
            {
                T[] localArray = array;
                for (int i = 0; i < localArray.Length; i++)
                {
                    object obj2 = localArray[i];
                    list.Add(obj2);
                }
            }
            return list;
        }
    }
}

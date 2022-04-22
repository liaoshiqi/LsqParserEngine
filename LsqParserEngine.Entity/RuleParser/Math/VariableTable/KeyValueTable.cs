using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// Dictionary<string, object>类型的数据源
    /// </summary>
    public class KeyValueTable : Hashtable, IVariableTable, IDictionary, ICollection, IEnumerable
    {
        private Dictionary<string, object> _dicData;

        public KeyValueTable(Dictionary<string, object> dicData)
        {
            this._dicData = dicData;
        }

        public override bool Contains(object key)
        {
            return this._dicData.ContainsKey((string)key);
        }

        public override bool ContainsKey(object key)
        {
            return this._dicData.ContainsKey((string)key);
        }

        public VariantType GetVariableType(string variableName)
        {
            VariantType type = VariantType.Unknown;
            if (ContainsKey(variableName))
            {
                object o = _dicData[variableName];
                if (o is bool)
                {
                    type = VariantType.Bool;
                }
                else if (o is int || o is long)
                {
                    type = VariantType.Int;
                }
                else if (o is double || o is decimal || o is float)
                {
                    type = VariantType.Double;
                }
                else if (o is DateTime)
                {
                    type = VariantType.DateTime;
                }
                else if (o is string)
                {
                    type = VariantType.String;
                }
                else if (o is TimeSpan)
                {
                    type = VariantType.TimeSpan;
                }
                else if (o is Array)
                {
                    type = VariantType.Array;
                }
            }

            return type;
        }

        public DataLogicType GetLogicType(string variableName)
        {
            return DataLogicType.String;
        }

        public override object this[object key]
        {
            get
            {
                return this._dicData.ContainsKey((string)key) ? this._dicData[(string)key] : null;
            }
        }
    }
}

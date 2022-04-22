using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 数据库业务表类型的数据源
    /// </summary>
    public class FieldValueTable : Hashtable, IVariableTable, IDictionary, ICollection, IEnumerable
    {
        //private BizObject _bizObject;

        public FieldValueTable()
        {
            //this._bizObject = bizObject;
        }

        public override bool Contains(object key)
        {
            //数据模型中是否包含该字段
            //return (this._bizObject.GetField((string)key) != null);

            return true;
        }

        public override bool ContainsKey(object key)
        {
            return this.Contains(key);
        }

        public VariantType GetVariableType(string variableName)
        {
            //从数据模型中获取字段的实际类型
            //PropertySchema property = this._bizObject.GetProperty(variableName);
            //if (property == null)
            //{
            //    return VariantType.Unknown;
            //}
            //return DataLogicTypeConvertor.GetVariantType(property.LogicType);
            return VariantType.String;
        }

        public DataLogicType GetLogicType(string variableName)
        {
            //从数据模型中获取字段的实际类型
            //PropertySchema property = this._bizObject.GetProperty(variableName);
            //if (property == null)
            //{
            //    return DataLogicType.Unspecified;
            //}
            //return property.LogicType;

            return DataLogicType.String;
        }

        public override object this[object key]
        {
            get
            {
                //return this.bizObject[(string)Key];
                return string.Empty;
            }
        }
    }
}

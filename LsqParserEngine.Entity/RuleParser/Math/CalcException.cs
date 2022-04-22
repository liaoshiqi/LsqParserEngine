using System;
using System.Runtime.Serialization;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 表达式解析异常
    /// </summary>
    [Serializable]
    public class CalcException : Exception
    {
        public CalcException()
        {
        }

        public CalcException(string s) : base(s)
        {
        }

        protected CalcException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

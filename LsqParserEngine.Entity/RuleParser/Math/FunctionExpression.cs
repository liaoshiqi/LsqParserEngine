using System;
using System.Collections;

namespace LsqParserEngine.Entity
{
    public class FunctionExpression : ArrayList
    {
        public string functionName;

        public FunctionExpression()
        {
            this.functionName = "";
        }

        public FunctionExpression(string s)
        {
            this.functionName = s;
        }

        public int Add(Variant v)
        {
            return base.Add(v);
        }

        public Variant this[int index]
        {
            get
            {
                return (Variant)base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}


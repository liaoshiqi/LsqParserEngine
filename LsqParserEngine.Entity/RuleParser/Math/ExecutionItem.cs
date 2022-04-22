using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    [Serializable]
    public abstract class ExecutionItem
    {
        private string _text;

        public abstract Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables);
        public virtual string Describe(FunctionFactory functionFactory, Stack<string> stack)
        {
            return this.Text;
        }

        public virtual string DescribeAsHtml(FunctionFactory functionFactory, Stack<string> stack, IDictionary variables)
        {
            return this.Text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            protected set
            {
                this._text = value;
            }
        }

        public abstract ExecutionItemType Type { get; }
    }
}

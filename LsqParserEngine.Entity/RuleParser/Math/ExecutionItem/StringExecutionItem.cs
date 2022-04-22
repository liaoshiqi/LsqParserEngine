using System;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal abstract class StringExecutionItem : ExecutionItem
    {
        protected StringExecutionItem()
        {
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            return new Variant(base.Text);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.String;
            }
        }
    }
}


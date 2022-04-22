using System;

namespace LsqParserEngine.Entity
{

    [Serializable]
    internal class NullExecutionItem : ExecutionItem
    {
        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            return new Variant(VariantType.Null);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Null;
            }
        }
    }
}


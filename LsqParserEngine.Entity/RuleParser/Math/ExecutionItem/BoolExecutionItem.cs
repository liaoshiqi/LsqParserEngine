using System;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// bool解析器
    /// </summary>
    [Serializable]
    internal class BoolExecutionItem : ExecutionItem
    {
        public BoolExecutionItem(bool b)
        {
            base.Text = b.ToString();
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            if (string.Compare(base.Text, "True", true) == 0)
            {
                return new Variant(true);
            }
            return new Variant(false);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Bool;
            }
        }
    }
}


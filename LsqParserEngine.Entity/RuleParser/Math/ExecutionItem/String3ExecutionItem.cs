using System;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal class String3ExecutionItem : StringExecutionItem
    {
        public String3ExecutionItem(string s)
        {
            base.Text = s;
        }
    }
}


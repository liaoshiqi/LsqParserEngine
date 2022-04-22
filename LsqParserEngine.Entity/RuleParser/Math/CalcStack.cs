using System.Collections;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 堆栈计算
    /// </summary>
    public class CalcStack : Stack
    {
        public Variant Peek()
        {
            return (Variant)base.Peek();
        }

        public Variant Pop()
        {
            return (Variant)base.Pop();
        }

        public void Push(Variant v)
        {
            base.Push(v);
        }

        private void Push(object o)
        {
        }
    }
}


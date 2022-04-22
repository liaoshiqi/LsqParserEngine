using System.Collections;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 堆栈操作
    /// </summary>
    internal class OperatorStack : Stack
    {
        public Operator Peek()
        {
            return (Operator)base.Peek();
        }

        public Operator Pop()
        {
            return (Operator)base.Pop();
        }

        public void Push(Operator op)
        {
            base.Push(op);
        }
    }
}


using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    internal class Describer
    {
        private Stack<string> _calcStack;
        private FunctionFactory _functionFactory;
        private IDictionary _variables;

        public Describer(FunctionFactory FunctionFactory)
        {
            this._calcStack = new Stack<string>();
            this._variables = null;
            this._functionFactory = FunctionFactory;
        }

        public Describer(IOrganization organization, Function[] customFunctions) : this(organization, customFunctions, null)
        {
        }

        public Describer(IOrganization organization,  Function[] customFunctions, IDictionary variables)
        {
            this._calcStack = new Stack<string>();
            this._variables = null;
            this._functionFactory = new FunctionFactory(organization, customFunctions);
            this._variables = variables;
        }

        public string Describe(ExecutionQueue eq)
        {
            if (eq.Count == 0)
            {
                return "";
            }
            for (int i = 0; i < eq.Count; i++)
            {
                string item = eq[i].Describe(this._functionFactory, this._calcStack);
                if (item != null)
                {
                    this._calcStack.Push(item);
                }
            }
            if (this._calcStack.Count == 0)
            {
                return null;
            }
            return this._calcStack.Pop();
        }

        public string DescribeAsHtml(ExecutionQueue eq)
        {
            if (eq.Count == 0)
            {
                return "";
            }
            for (int i = 0; i < eq.Count; i++)
            {
                string item = eq[i].DescribeAsHtml(this._functionFactory, this._calcStack, this._variables);
                if (item != null)
                {
                    this._calcStack.Push(item);
                }
            }
            if (this._calcStack.Count == 0)
            {
                return null;
            }
            return this._calcStack.Pop();
        }
    }
}


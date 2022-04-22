namespace LsqParserEngine.Entity
{
    internal class Calculator
    {
        private CalcStack _calcStack;
        private FunctionFactory _functionFactory;
        private IVariableTable _variables;

        public Calculator(FunctionFactory functionFactory, IVariableTable variables)
        {
            this._variables = variables;
            this._calcStack = new CalcStack();
            this._functionFactory = functionFactory;
        }
        public Calculator(IOrganization organization,  IVariableTable variables)
        {
            this._variables = variables;
            this._calcStack = new CalcStack();
            this._functionFactory = new FunctionFactory(organization, null);
        }
        public Calculator(IOrganization organization, Function[] customFunctions, IVariableTable variables)
        {
            this._variables = variables;
            this._calcStack = new CalcStack();
            this._functionFactory = new FunctionFactory(organization, customFunctions);
        }

        public Variant Calculate(ExecutionQueue eq)
        {
            if (eq.Count == 0)
            {
                return new Variant("");
            }
            for (int i = 0; i < eq.Count; i++)
            {
                Variant v = eq[i].Calculate(this._functionFactory, this._calcStack, this._variables);
                this._calcStack.Push(v);
            }
            if (this._calcStack.Count == 0)
            {
                return null;
            }
            if (this._calcStack.Count != 1)
            {
                throw new CalcException("Calculate failed.");
            }
            return this._calcStack.Pop();
        }
    }
}


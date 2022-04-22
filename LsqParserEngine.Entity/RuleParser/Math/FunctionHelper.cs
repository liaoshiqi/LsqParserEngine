using System;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 规则帮助
    /// </summary>
    [Serializable]
    public class FunctionHelper
    {
        private string _description;
        private string _example;
        private string _name;
        private Parameter[] _parameters;
        private Parameter _return;

        public FunctionHelper(string name, string description, string example, Parameter[] parameters, Parameter param)
        {
            this._name = name;
            this._description = description;
            this._example = example;
            this._parameters = parameters;
            this._return = param;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
        }

        public string Example
        {
            get
            {
                return this._example;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public Parameter[] Parameters
        {
            get
            {
                return this._parameters;
            }
        }

        public Parameter Return
        {
            get
            {
                return this._return;
            }
        }
    }
}

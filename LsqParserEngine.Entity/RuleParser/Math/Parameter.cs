using System;

namespace LsqParserEngine.Entity
{
    [Serializable]
    public class Parameter
    {
        private string _description;
        private DataLogicType[] _logicTypes;
        private string _name;
        private bool _required;

        public Parameter(string name, string description, DataLogicType[] logicTypes)
        {
            this._required = true;
            this._name = name;
            this._description = description;
            this._logicTypes = logicTypes;
        }

        public Parameter(string name, string description, DataLogicType[] logicTypes, bool required)
        {
            this._required = true;
            this._name = name;
            this._description = description;
            this._logicTypes = logicTypes;
            this._required = required;
        }

        public bool IsLogicTypeAcceptable(DataLogicType logicType)
        {
            if (this.LogicTypes != null)
            {
                foreach (DataLogicType type in this.LogicTypes)
                {
                    if (type == logicType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
        }

        public DataLogicType[] LogicTypes
        {
            get
            {
                return this._logicTypes;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public bool Required
        {
            get
            {
                return this._required;
            }
        }
    }
}

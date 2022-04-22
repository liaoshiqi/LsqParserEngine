using LsqParserEngine.Common.Convertor;
using System;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variant
    {
        private object _value;
        private VariantType _type;
        private string _name;
        //实际的类型
        private DataLogicType _dataLogicType;

        public Variant(object obj, string variableName, VariantType type, DataLogicType dataLogicType)
        {
            this._dataLogicType = dataLogicType;    //字段实际类型
            this._Variant(obj);
            this._name = variableName;
            if (type != VariantType.Unknown)
            {
                this._type = type;
            }
        }
        public Variant(object o, VariantType type)
        {
            this._Variant(o: o);
            this._type = type;
        }

        public Variant()
        {
            this._type = VariantType.Unknown;
        }
        public Variant(VariantType Type)
        {
            this._type = Type;
        }
        public Variant(object o)
        {
            this._Variant(o);
        }
        public Variant(Variant v)
        {
            _Variant(v);
        }

        public Variant(bool b)
        {
            _Variant(b);
        }

        public Variant(DateTime dt)
        {
            _Variant(dt);
        }

        public Variant(double d)
        {
            _Variant(d);
        }

        public Variant(int i)
        {
            _Variant(i);
        }

        private void _Variant(object o)
        {
            if (o is null)
            {
                this._value = null;
                this._type = VariantType.Null;
                switch (this.DataLogicType)
                {
                    case DataLogicType.Double:
                        this._value = (double)0;
                        this._type = VariantType.Double;
                        break;
                    case DataLogicType.Long:
                        this._value = (int)((long)0);
                        this._type = VariantType.Int;
                        break;
                    case DataLogicType.Int:
                        this._value = (int)0;
                        this._type = VariantType.Int;
                        break;
                    default:
                        break;
                }
            }
            else if (o is bool)
            {
                this._value = (bool)o;
                this._type = VariantType.Bool;
            }
            else if (o is int)
            {
                this._value = (int)o;
                this._type = VariantType.Int;
            }
            else if (o is double)
            {
                this._value = (double)o;
                this._type = VariantType.Double;
            }
            else if (o is DateTime)
            {
                this._value = (DateTime)o;
                this._type = VariantType.DateTime;
            }
            else if (o is string)
            {
                this._value = (string)o;
                this._type = VariantType.String;
            }
            else if (o is Variant)
            {
                this._value = ((Variant)o).Value;
                this._type = ((Variant)o).Type;
            }
            else if (o is decimal)
            {
                this._value = (int)((decimal)o);
                this._type = VariantType.Int;
            }
            else if (o is long)
            {
                this._value = (int)((long)o);
                this._type = VariantType.Int;
            }
            else if (o is float)
            {
                this._value = (float)o;
                this._type = VariantType.Double;
            }
            else if (o is TimeSpan)
            {
                this._value = (TimeSpan)o;
                this._type = VariantType.TimeSpan;
            }
            else
            {
                if (!(o is Array))
                {
                    throw new CalcException("Invalid object type for Variant conversion");
                }
                this._value = o;
                this._type = VariantType.Array;
            }
        }

        public Variant(string s)
        {
            _Variant(s);
        }

        public override bool Equals(object o)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static Variant operator +(Variant b, Variant a)
        {
            DateTime oValue;
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with plus operator");
                    }
                    return new Variant(a.ToString() + b.ToString());

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with plus operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) + ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) + ((double)b.Value));

                        case VariantType.String:
                            return new Variant(a.ToString() + b.ToString());

                        case VariantType.DateTime:
                            throw new CalcException("Bad 2-st operand datetime type with plus operator");
                    }
                    throw new CalcException("Bad 2-nd operand type with plus operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with plus operator");

                        case VariantType.Int:
                            return new Variant(((double)a.Value) + ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) + ((double)b.Value));

                        case VariantType.String:
                            return new Variant(a.ToString() + b.ToString());

                        case VariantType.DateTime:
                            throw new CalcException("Bad 2-st operand datetime type with plus operator");
                    }
                    throw new CalcException("Bad 2-nd operand type with plus operator");

                case VariantType.String:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                        case VariantType.Int:
                        case VariantType.Double:
                        case VariantType.String:
                        case VariantType.DateTime:
                            return new Variant(a.ToString() + b.ToString());
                    }
                    throw new CalcException("Bad 2-nd operand type with plus operator");

                case VariantType.DateTime:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with minus operator");
                    }
                    oValue = (DateTime)a.Value;
                    return new Variant(oValue.Add((TimeSpan)b.Value));

                case VariantType.TimeSpan:
                    switch (b._type)
                    {
                        case VariantType.DateTime:
                            oValue = (DateTime)b.Value;
                            return new Variant(oValue.Add((TimeSpan)a.Value));

                        case VariantType.TimeSpan:
                            {
                                TimeSpan span = (TimeSpan)a.Value;
                                return new Variant(span.Add((TimeSpan)b.Value));
                            }
                    }
                    throw new CalcException("Bad 2-nd operand type with minus operator");
                case VariantType.Unit:
                    VariantType type = b._type;
                    switch (type)
                    {
                        case VariantType.String:
                        case VariantType.Unit:
                            return new Variant(new object[] { a.Value, b.Value }, VariantType.Array);
                    }
                    if (type != VariantType.Array)
                    {
                        throw new CalcException("Bad 2-nd operand type with minus operator");
                    }
                    return new Variant(ArrayConvertor<object>.Add(new object[] { a.Value }, (object[])b.Value));

                case VariantType.Array:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                        case VariantType.Int:
                        case VariantType.Double:
                        case VariantType.String:
                        case VariantType.DateTime:
                        case VariantType.TimeSpan:
                        case VariantType.Unit:
                            return new Variant(ArrayConvertor<object>.AddToArray((object[])a.Value, b.Value));

                        case VariantType.Array:
                            return new Variant(ArrayConvertor<object>.Add((object[])a.Value, (object[])b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with plus operator");
            }
            throw new CalcException("Bad 1-st operand type with plus operator");
        }

        public static Variant operator /(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            if (((int)b.Value) == 0)
                            {
                                return new Variant(0);
                            }
                            return new Variant(((int)a.Value) / ((int)b.Value));

                        case VariantType.Double:
                            if (((double)b.Value) == 0.0)
                            {
                                return new Variant(0.0);
                            }
                            return new Variant(((double)((int)a.Value)) / ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with div operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            if (((int)b.Value) == 0)
                            {
                                return new Variant(0.0);
                            }
                            return new Variant(((double)a.Value) / ((double)((int)b.Value)));

                        case VariantType.Double:
                            if (((double)b.Value) == 0.0)
                            {
                                return new Variant(0.0);
                            }
                            return new Variant(((double)a.Value) / ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with div operator");
            }
            throw new CalcException("Bad 1-st operand type with div operator");
        }

        public static Variant operator &(Variant b, Variant a)
        {

            Variant result;

            VariantType type = a._type;
            if (type != VariantType.Bool)
            {
                if (type != VariantType.String)
                {
                    throw new CalcException("Bad 1-st operand type with & operator");
                }
                type = b._type;
                if (type != VariantType.Bool)
                {
                    if (type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with minus operator");
                    }
                    bool flag = a != string.IsNullOrEmpty(a) && bool.Parse(a);
                    bool flag2 = b != string.IsNullOrEmpty(b) && bool.Parse(b);
                    result = new Variant(flag && flag2);
                }
                else
                {
                    bool flag = !string.IsNullOrEmpty(a) && bool.Parse(a);
                    result = new Variant(flag && (bool)b.Value);
                }
            }
            else
            {
                type = b._type;
                if (type != VariantType.Bool)
                {
                    throw new CalcException("Bad 2-nd operand type with & operator");
                }
                if ((bool)a.Value && (bool)b.Value)
                {
                    result = new Variant(true);
                }
                else
                {
                    result = new Variant(false);
                }

            }
            return result;
        }
        public static Variant operator |(Variant b, Variant a)
        {

            VariantType type = a._type;
            Variant result;
            if (type != VariantType.Bool)
            {
                if (type != VariantType.String)
                {
                    throw new CalcException("Bad 1-st operand type with & operator");
                }
                type = b._type;
                if (type != VariantType.Bool)
                {
                    if (type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with minus operator");
                    }
                    bool flag = !string.IsNullOrEmpty(a) && bool.Parse(a);
                    bool flag2 = !string.IsNullOrEmpty(b) && bool.Parse(b);
                    result = new Variant(flag || flag2);
                }
                else
                {
                    result = new Variant((!string.IsNullOrEmpty(a) && bool.Parse(a)) || (bool)b.Value);
                }
            }
            else
            {
                type = b._type;
                if (type != VariantType.Bool)
                {
                    throw new CalcException("Bad 2-nd operand type with & operator");
                }
                if ((bool)a.Value || (bool)b.Value)
                {
                    result = new Variant(true);
                }
                else
                {
                    result = new Variant(false);
                }
            }
            return result;
        }

        public static Variant operator ==(Variant a, Variant b)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with == operator");
                    }
                    if (((bool)a.Value) ^ ((bool)b.Value))
                    {
                        return new Variant(false);
                    }
                    return new Variant(true);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with == operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) == ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) == ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with == operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) == ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) == ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with == operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with == operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) == 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with == operator");
                    }
                    return new Variant(((DateTime)a.Value) == ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with == operator");
                    }
                    return new Variant(((TimeSpan)a.Value) == ((TimeSpan)b.Value));
                case VariantType.Unit:
                    switch (b._type)
                    {
                        case VariantType.String:
                        case VariantType.Unit:
                        case VariantType.Null:
                            return new Variant(string.Compare((string)a.Value, (string)b.Value, true) == 0);

                    }
                    throw new CalcException("Bad 2-nd operand type with == operator");
            }
            throw new CalcException("Bad 1-st operand type with == operator");
        }

        public static Variant operator >(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with > operator");
                    }
                    if (!(!((bool)a.Value) || ((bool)b.Value)))
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with > operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) > ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) > ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with > operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) > ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) > ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with > operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with > operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) > 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with > operator");
                    }
                    return new Variant(((DateTime)a.Value) > ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with > operator");
                    }
                    return new Variant(((TimeSpan)a.Value) > ((TimeSpan)b.Value));
            }
            throw new CalcException("Bad 1-st operand type with > operator");
        }

        public static Variant operator >=(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with >= operator");
                    }
                    if (!(((bool)a.Value) || !((bool)b.Value)))
                    {
                        return new Variant(false);
                    }
                    return new Variant(true);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with >= operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) >= ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) >= ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with >= operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) >= ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) >= ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with >= operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with >= operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) >= 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with >= operator");
                    }
                    return new Variant(((DateTime)a.Value) >= ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with >= operator");
                    }
                    return new Variant(((TimeSpan)a.Value) >= ((TimeSpan)b.Value));
            }
            throw new CalcException("Bad 1-st operand type with >= operator");
        }

        public static implicit operator bool(Variant v)
        {
            if (v.Type != VariantType.Bool)
            {
                throw new CalcException("Bad typecast from " + v.Type + " to bool");
            }
            return (bool)v.Value;
        }

        public static implicit operator DateTime(Variant v)
        {
            if (v.Type != VariantType.DateTime)
            {
                throw new CalcException("Bad typecast from " + v.Type + " to DateTime");
            }
            return (DateTime)v.Value;
        }

        public static implicit operator double(Variant v)
        {
            switch (v.Type)
            {
                case VariantType.Int:
                    return (double)((int)v.Value);

                case VariantType.Double:
                    return (double)v.Value;
            }
            throw new CalcException("Bad typecast from " + v.Type + " to double");
        }

        public static implicit operator int(Variant v)
        {
            switch (v.Type)
            {
                case VariantType.Int:
                    return (int)v.Value;

                case VariantType.Double:
                    return (int)((double)v.Value);
            }
            throw new CalcException("Bad typecast from " + v.Type + " to int");
        }

        public static implicit operator string(Variant v)
        {
            if (v._type == VariantType.Unknown)
            {
                return "";
            }
            return v.ToString();
        }

        public static implicit operator TimeSpan(Variant v)
        {
            if (v.Type != VariantType.TimeSpan)
            {
                throw new CalcException("Bad typecast from " + v.Type + " to DateTime");
            }
            return (TimeSpan)v.Value;
        }

        public static implicit operator Variant(string s)
        {
            return new Variant(s);
        }

        public static Variant operator !=(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with != operator");
                    }
                    if (((bool)a.Value) ^ ((bool)b.Value))
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with != operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) != ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) != ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with != operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) != ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) != ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with != operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with != operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) != 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with != operator");
                    }
                    return new Variant(((DateTime)a.Value) <= ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with != operator");
                    }
                    return new Variant(((TimeSpan)a.Value) != ((TimeSpan)b.Value));
            }
            throw new CalcException("Bad 1-st operand type with != operator");
        }

        public static Variant operator <(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with < operator");
                    }
                    if (!(((bool)a.Value) || !((bool)b.Value)))
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with < operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) < ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) < ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with < operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) < ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) < ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with < operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with < operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) < 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with < operator");
                    }
                    return new Variant(((DateTime)a.Value) < ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with > operator");
                    }
                    return new Variant(((TimeSpan)a.Value) < ((TimeSpan)b.Value));
            }
            throw new CalcException("Bad 1-st operand type with < operator");
        }

        public static Variant operator <=(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    if (b._type != VariantType.Bool)
                    {
                        throw new CalcException("Bad 2-nd operand type with <= operator");
                    }
                    if (!(!((bool)a.Value) || ((bool)b.Value)))
                    {
                        return new Variant(false);
                    }
                    return new Variant(true);

                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Bool:
                            throw new CalcException("Bad 2-st operand bool type with <= operator");

                        case VariantType.Int:
                            return new Variant(((int)a.Value) <= ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) <= ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with <= operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) <= ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) <= ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with <= operator");

                case VariantType.String:
                    if (b._type != VariantType.String)
                    {
                        throw new CalcException("Bad 2-nd operand type with <= operator");
                    }
                    if (string.Compare(a.ToString(), b.ToString(), true) <= 0)
                    {
                        return new Variant(true);
                    }
                    return new Variant(false);

                case VariantType.DateTime:
                    if (b._type != VariantType.DateTime)
                    {
                        throw new CalcException("Bad 2-nd operand type with <= operator");
                    }
                    return new Variant(((DateTime)a.Value) <= ((DateTime)b.Value));

                case VariantType.TimeSpan:
                    if (b._type != VariantType.TimeSpan)
                    {
                        throw new CalcException("Bad 2-nd operand type with <= operator");
                    }
                    return new Variant(((TimeSpan)a.Value) <= ((TimeSpan)b.Value));
            }
            throw new CalcException("Bad 1-st operand type with <= operator");
        }

        public static Variant op_LogicalNot(Variant a)
        {
            //return new Variant(op_UnaryNegation(a));//lzp
            return null;
        }

        public static Variant operator *(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((int)a.Value) * ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) * ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with mul operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) * ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) * ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with mul operator");
            }
            throw new CalcException("Bad 1-st operand type with mul operator");
        }

        public static Variant operator -(Variant b, Variant a)
        {
            switch (a._type)
            {
                case VariantType.Int:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((int)a.Value) - ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((int)a.Value) - ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with plus operator");

                case VariantType.Double:
                    switch (b._type)
                    {
                        case VariantType.Int:
                            return new Variant(((double)a.Value) - ((int)b.Value));

                        case VariantType.Double:
                            return new Variant(((double)a.Value) - ((double)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with minus operator");

                case VariantType.DateTime:
                    DateTime oValue;
                    switch (b._type)
                    {
                        case VariantType.DateTime:
                            oValue = (DateTime)a.Value;
                            return new Variant(oValue.Subtract((DateTime)b.Value));

                        case VariantType.TimeSpan:
                            oValue = (DateTime)a.Value;
                            return new Variant(oValue.Subtract((TimeSpan)b.Value));
                    }
                    throw new CalcException("Bad 2-nd operand type with minus operator");

                case VariantType.TimeSpan:
                    {
                        if (b._type != VariantType.TimeSpan)
                        {
                            throw new CalcException("Bad 2-nd operand type with minus operator");
                        }
                        TimeSpan span = (TimeSpan)a.Value;
                        return new Variant(span.Subtract((TimeSpan)b.Value));
                    }
            }
            throw new CalcException("Bad 1-st operand type with plus operator");
        }

        public static Variant operator -(Variant a)
        {
            switch (a._type)
            {
                case VariantType.Bool:
                    return new Variant(!((bool)a.Value));

                case VariantType.Int:
                    return new Variant(-((int)a.Value));

                case VariantType.Double:
                    return new Variant(-((double)a.Value));
            }
            throw new CalcException("Bad operand type for UnMinus operator");
        }

        public static Variant operator +(Variant a)
        {
            return new Variant(a);
        }

        public override string ToString()
        {
            switch (this._type)
            {
                case VariantType.Unknown:
                    return "";

                case VariantType.Bool:
                    {
                        bool oValue = (bool)this.Value;
                        return oValue.ToString();
                    }
                case VariantType.Int:
                    {
                        int num = (int)this.Value;
                        return num.ToString();
                    }
                case VariantType.Double:
                    {
                        double num2 = (double)this.Value;
                        return num2.ToString();
                    }
                case VariantType.String:
                    return this.Value.ToString();

                case VariantType.DateTime:
                    {
                        DateTime time = (DateTime)this.Value;
                        return time.ToString();
                    }
            }
            return "";
        }

        public VariantType Type => this._type;


        public object Value => this._value;

        public string Name => _name;
        public DataLogicType DataLogicType => _dataLogicType;
    }
}

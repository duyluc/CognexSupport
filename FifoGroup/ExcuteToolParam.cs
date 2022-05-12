using System;

namespace FifoGroup
{
    public class ExcuteToolParam : IEquatable<ExcuteToolParam>
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        public ExcuteToolParam(string name, Type type, object value)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name");
            if(type == null) throw new ArgumentNullException("Type");
            if(value == null) throw new ArgumentNullException("Value");
            Name = name;
            Type = type;
            Value = value;
        }

        public bool Equals(ExcuteToolParam other)
        {
            if (Name != other.Name) return false;
            if (Type != other.Type) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
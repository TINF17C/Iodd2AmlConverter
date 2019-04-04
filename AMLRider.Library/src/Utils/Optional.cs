using System;
using System.Dynamic;
using System.Runtime.Serialization;
using AMLRider.Library.Rules;

namespace AMLRider.Library.Utils
{
    
    public class Optional<T>
    {

        private T Value { get; }

        public bool IsPresent => Value != null;

        public bool IsEmpty => Value == null;

        private Optional()
        {
            Value = default(T);
        }
        
        private Optional(T value)
        {
            Value = value;
        }

        public T Get()
        {
            if(Value == null)
                throw new InvalidOperationException("No value present.");

            return Value;
        }

        public void IfPresent(Action<T> action)
        {
            if (Value != null)
                action(Value);
        }

        public static Optional<T> Empty()
        {
            return new Optional<T>();
        }
        
        public static Optional<T> Of(T value)
        {
            if(value == null)
                throw new InvalidOperationException($"'{nameof(value)}' must not be null.");
            
            return new Optional<T>(value);
        }

        public static Optional<T> OfNullable(T value)
        {
            return value == null ? Empty() : new Optional<T>(value);
        }
        
    }
    
}
namespace DictionaryList
{
    public class ValueWrapper<T>
    {
        public ValueWrapper(bool hasValue, T value)
        {
            HasValue = hasValue;
            Value = value;
        }

        public bool HasValue { get; private set; }

        public T Value { get; private set; }

        public override string ToString()
        {
            return $"{(HasValue ? Value : "NULL")}";
        }
    }
}

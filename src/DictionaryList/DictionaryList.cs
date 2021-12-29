using System;
using System.Linq;
using System.Collections.Generic;

namespace DictionaryList
{
    public class DictionaryList<T, U>
    {
        private readonly Node<T, U> Root = new Node<T, U>(default!, null) { IsRoot = true };

        public bool AllowNULLsInKeys { get; set; }

        public void Add(List<T> data, U value)
        {
            var current = Root;

            for (int i = 0; i < data.Count; i++)
            {
                T item = data[i];

                if (!AllowNULLsInKeys && item == null)
                    throw new ArgumentException($"Element at index '{i}' is null. It cannot be used as a Key's element");

                var found = AllowNULLsInKeys ?
                    current.Children.FirstOrDefault(x => Equals(x.ArrayValue, item)) :
                    current.Children.FirstOrDefault(x => x.ArrayValue!.Equals(item));

                var isLast = i == data.Count - 1;

                if (found != null)
                {
                    if (isLast)
                    {
                        if (found.StoredValue is null)
                        {
                            found.StoredValue = new ValueWrapper<U>(true, value);
                        }
                        else
                        {
                            if (found.StoredValue.HasValue && !found.StoredValue.Value!.Equals(value))
                            {
                                throw new ArgumentException($"Value: '{value}' cannot be saved because there's already value:" +
                                    $" {found.StoredValue.Value}. Key: {string.Join(",", data)}");
                            }
                        }
                    }

                    current = found;
                }
                else
                {
                    var wrapper2 = new ValueWrapper<U>(isLast, value);
                    current = current.Add(item, wrapper2);
                }
            }
        }

        public bool TryGet(List<T> data, out U? value)
        {
            var current = Root;

            for (int i = 0; i < data.Count; i++)
            {
                T item = data[i];

                var found = AllowNULLsInKeys ?
                    current.Children.FirstOrDefault(x => Equals(x.ArrayValue, item)) :
                    current.Children.FirstOrDefault(x => x.ArrayValue!.Equals(item));

                var isLast = i == data.Count - 1;

                if (found != null)
                {
                    if (isLast)
                    {
                        if (found.StoredValue == null || !found.StoredValue.HasValue)
                            goto Fail;

                        value = found.StoredValue.Value;
                        return true;
                    }

                    current = found;
                }
                else
                {
                    goto Fail;
                }
            }

            Fail:
            value = default;
            return false;
        }
    }
}
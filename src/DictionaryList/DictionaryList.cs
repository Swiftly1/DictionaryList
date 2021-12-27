using System;
using System.Linq;
using System.Collections.Generic;

namespace DictionaryList
{
    public class DictionaryList<T, U>
    {
        private Node<T, U> Root = new Node<T, U>() { IsRoot = true };

        public void Add(List<T> data, U value)
        {
            var current = Root;

            for (int i = 0; i < data.Count; i++)
            {
                T item = data[i];
                var found = current.Children.FirstOrDefault(x => x.ArrayValue.HasValue && x.ArrayValue.Value.Equals(item));
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
                            if (found.StoredValue.HasValue && !found.StoredValue.Value.Equals(value))
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
                    var wrapper1 = new ValueWrapper<T>(true, item);
                    var wrapper2 = new ValueWrapper<U>(isLast, value);
                    current = current.Add(wrapper1, wrapper2);
                }
            }
        }

        public bool TryGet(List<T> data, out U? value)
        {
            var current = Root;

            for (int i = 0; i < data.Count; i++)
            {
                T item = data[i];
                var found = current.Children.FirstOrDefault(x => x.ArrayValue.HasValue && x.ArrayValue.Value.Equals(item));
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
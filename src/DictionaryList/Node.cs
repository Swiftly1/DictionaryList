using System;
using System.Collections.Generic;

namespace DictionaryList
{
    internal class Node<T, U>
    {
        public bool IsRoot { get; set; } = false;

        private T _ArrayValue;

        public Node(T arrayValue, ValueWrapper<U>? storedValue)
        {
            ArrayValue = arrayValue;
            StoredValue = storedValue;
        }

        public T ArrayValue
        {
            get
            {
                if (IsRoot)
                    throw new Exception("Root does not contain value.");

                return _ArrayValue;
            }
            set { _ArrayValue = value; }
        }

        public ValueWrapper<U>? StoredValue { get; set; }

        public List<Node<T, U>> Children { get; set; } = new List<Node<T, U>>();

        public Node<T, U> Add(T arr, ValueWrapper<U> value)
        {
            var newNode = new Node<T, U>(arr, value.HasValue ? value : null);

            Children.Add(newNode);
            return newNode;
        }
    }
}
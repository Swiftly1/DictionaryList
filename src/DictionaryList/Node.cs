using System.Collections.Generic;

namespace DictionaryList
{
    internal class Node<T, U>
    {
        public bool IsRoot { get; set; } = false;

        public ValueWrapper<T> ArrayValue { get; set; }

        public ValueWrapper<U> StoredValue { get; set; }

        public List<Node<T, U>> Children { get; set; } = new List<Node<T, U>>();

        public Node<T, U> Add(ValueWrapper<T> arr, ValueWrapper<U> value)
        {
            var newNode = new Node<T, U>
            {
                ArrayValue = arr,
                StoredValue = value.HasValue ? value : null
            };

            Children.Add(newNode);
            return newNode;
        }
    }
}
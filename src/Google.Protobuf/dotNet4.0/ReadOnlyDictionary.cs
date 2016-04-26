using System;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a read-only, generic collection of key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> dictionary;

        private const string ReadOnlyExceptionMessage = "Collection is read - only";

        private ReadOnlyDictionary()
        {
            dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyDictionary(TKey, TValue) class that is a wrapper around the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to wrap.</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        /// <summary>
        /// Gets the element that has the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element that has the specified key.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return dictionary[key];
            }

            set
            {
                throw new NotSupportedException(ReadOnlyExceptionMessage);
            }
        }

        /// <summary>
        /// Gets the number of items in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a collection that contains the keys of the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets a collection that contains the values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return dictionary.Values;
            }
        }

        /// <summary>
        /// Throws a NotSupportedException exception in all cases.
        /// </summary>
        /// <param name="item">The object to add to the dictionary.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException(ReadOnlyExceptionMessage);
        }

        /// <summary>
        /// Throws a NotSupportedException exception in all cases.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException(ReadOnlyExceptionMessage);
        }

        /// <summary>
        /// Throws a NotSupportedException exception in all cases.
        /// </summary>
        public void Clear()
        {
            throw new NotSupportedException(ReadOnlyExceptionMessage);
        }

        /// <summary>
        /// Determines whether the dictionary contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the dictionary.</param>
        /// <returns>true if item is found in the dictionary; otherwise, false.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        /// <summary>
        /// Determines whether the dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Copies the elements of the dictionary to an array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator for the dictionary.
        /// </summary>
        /// <returns>An enumerator for the dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        /// <summary>
        /// Throws a NotSupportedException exception in all cases.
        /// </summary>
        /// <param name="item">The object to remove from the dictionary.</param>
        /// <returns>Throws a NotSupportedException exception in all cases.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException(ReadOnlyExceptionMessage);
        }

        /// <summary>
        /// Throws a NotSupportedException exception in all cases.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>Throws a NotSupportedException exception in all cases.</returns>
        public bool Remove(TKey key)
        {
            throw new NotSupportedException(ReadOnlyExceptionMessage);
        }

        /// <summary>
        /// Retrieves the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value will be retrieved.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the object that implements ReadOnlyDictionary(TKey, TValue) contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }
}

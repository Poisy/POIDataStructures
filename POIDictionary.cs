using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace POI
{
    /// <summary>
    /// Typical Dictionary just maded by Poigrammer.
    /// </summary>
    /// <typeparam name="K">The type of the Keys.</typeparam>
    /// <typeparam name="V">The type of the Values.</typeparam>
    public class POIDictionary<K, V> : IEnumerable
    {
        #region Members
        private int capacity;
        private V[] values = new V[16];
        private K[] keys = new K[16];
        #endregion Members
        #region Constructors
        /// <summary>
        /// Only parameter is the size of the Dictionary.
        /// </summary>
        /// <param name="size">The size of the Dictionary.</param>
        public POIDictionary(int size)
        {
            this.capacity = size;
            this.values = new V[size];
            this.keys = new K[size];
        }
        /// <summary>
        /// Auto size of the Dictionary will be 16.
        /// </summary>
        public POIDictionary()
        {
            this.capacity = 16;
        }
        #endregion Constructors
        #region Methods
        //PUBLIC
        /// <summary>
        /// Adding a pair of Key and Value to the Dictionary.
        /// </summary>
        /// <param name="key">The Key with type <see cref="K"/>.</param>
        /// <param name="value">The Value with type <see cref="V"/>.</param>
        public void Add(K key, V value)
        {
            if (ContainsKey(key))
            {
                if (Comparer<K>.Default.Compare(this.keys[GetIndex(key)], key) == 0)
                {
                    this.values[GetIndex(key)] = value;
                    return;
                }
                ExtendingCapacity();
                Add(key, value);
            }
            else
            {
                this.values[GetIndex(key)] = value;
                this.keys[GetIndex(key)] = key;
            }
        }
        /// <summary>
        /// Checks if the Dictionary contains the Key.<br/><br/>
        /// Returns: <br/>
        ///         true - if the Dictionary contains the Key.<br/>
        ///         false - if don't contains it.
        /// </summary>
        /// <param name="key">The key with type <see cref="K"/></param>
        /// <returns>Returns bool type with is <see cref="true"/> if contains the Key or <see cref="false"/> if didn't contain.</returns>
        public bool ContainsKey(K key)
        {
            if (Comparer<K>.Default.Compare(this.keys[GetIndex(key)], default) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check if the Dictionary contains the Value.
        /// Returns: <br/>
        /// true - if the Dictionary contains the Value.<br/>
        /// false - if don't contains it.
        /// </summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(V value)
        {
            K[] tempKeys = Keys;
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Comparer<V>.Default.Compare(this.values[GetIndex(tempKeys[i])], value) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Removes a pair of Key and Value from the Dictionary.<br/><br/>
        /// Returns:<br/>
        /// The Value which you remove with the Key.
        /// </summary>
        /// <param name="key">The Key which you want to remove with his Value.</param>
        /// <returns></returns>
        public V Remove(K key)
        {
            if (ContainsKey(key))
            {
                int keyIndex = GetIndex(key);
                V returnValue = this.values[keyIndex];
                this.values[keyIndex] = default;
                this.keys[keyIndex] = default;
                return returnValue;
            }
            return default;
        }
        /// <summary>
        /// Clears the Dictionary.
        /// </summary>
        public void Clear()
        {
            this.keys = new K[capacity];
            this.values = new V[capacity];
        }
        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < Values.Length; i++)
            {
                text += $"{Keys[i]} - {Values[i]}\n";
            }
            return text;
        }
        //PRIVATE
        private void ExtendingCapacity()
        {
            this.capacity *= 2;
            V[] tempValues = values.Where(value => value != null).ToArray();
            K[] tempKeys = keys;
            this.values = new V[capacity];
            this.keys = new K[capacity];
            for (int i = 0; i < tempKeys.Length; i++)
            {
                Add(tempKeys[i], tempValues[i]);
            }
        }
        private int GetIndex(K key) => Math.Abs(key.GetHashCode()) % capacity;
        #endregion Methods
        #region Properties
        public V this[K key]
        {
            get
            {
                if (ContainsKey(key)) return values[GetIndex(key)];
                throw new Exception("This key doesn't exist.");
            }
            set { Add(key, value); }
        }
        /// <summary>
        /// Returns all Keys in the Dictionary.
        /// </summary>
        public K[] Keys
        {
            get { return this.keys.Where(key => Comparer<K>.Default.Compare(key, default) != 0).ToArray(); }
        }
        /// <summary>
        /// Returns all Values from the Dictionary.
        /// </summary>
        public V[] Values
        {
            get
            {
                int tempCount = 0;
                V[] tempValues = new V[capacity];
                for (int i = 0; i < keys.Length; i++)
                {
                    if (Comparer<K>.Default.Compare(keys[i], default) != 0)
                    {
                        tempValues[tempCount] = values[i];
                        tempCount++;
                    }
                }
                V[] valuesToReturn = new V[tempCount];
                for (int i = 0; i < tempCount; i++)
                {
                    valuesToReturn[i] = tempValues[i];
                }
                return valuesToReturn;
            }
        }
        #endregion Properties
        #region IEnumerator
        public class PairKeyValue
        {
            private K key;
            private V value;
            public PairKeyValue(K key, V value)
            {
                this.key = key;
                this.value = value;
            }
            public K Key { get { return this.key; } }
            public V Value { get { return this.value; } }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public DictEnum GetEnumerator()
        {
            return new DictEnum(Keys, Values);
        }
        public class DictEnum : IEnumerator
        {
            public PairKeyValue[] pairs;
            private int position = -1;
            public DictEnum(K[] keys, V[] values)
            {
                pairs = new PairKeyValue[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    pairs[i] = new PairKeyValue(keys[i], values[i]);
                }
            }
            public bool MoveNext()
            {
                position++;
                return (position < pairs.Length);
            }
            public void Reset()
            {
                position = -1;
            }
            object IEnumerator.Current
            {
                get { return Current; }
            }
            public PairKeyValue Current
            {
                get
                {
                    try
                    {
                        return pairs[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new Exception("Out of range.");
                    }
                }
            }
            
        }
        #endregion IEnumerator
    }
}

using System;
using System.Collections.Generic;


namespace POI
{
    /// <summary>
    /// Represents a Set maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class POISet<T>
    {
        #region Members
        private const int DEFAULT_CAPACITY = 16;
        private T[] array;
        private int count;
        #endregion Members
        #region Constructors
        /// <summary>
        /// Adding array with elements to the Set.
        /// </summary>
        /// <param name="array">The array to add in the Set.</param>
        public POISet(T[] array)
        {
            this.array = new T[DEFAULT_CAPACITY];
            this.count = 0;
            foreach (var x in array)
            {
                Add(x);
            }
        }
        public POISet(int capacity)
        {
            this.array = new T[capacity];
        }
        public POISet()
        {
            this.array = new T[DEFAULT_CAPACITY];
        }
        #endregion Constructors
        #region Methods
        //PUBLIC
        /// <summary>
        /// Add element to the Set.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <returns>Bool which depends if the adding is successful or not.</returns>
        public bool Add(T element)
        {
            int hashCode = HashCodeOfElement(element);
            if (hashCode > array.Length || Compare(array[hashCode], default) != 0)
            {
                ExtendArray();
                return Add(element);
            }
            array[hashCode] = element;
            count++;
            return true;
        }
        /// <summary>
        /// Add array with elements to the Set.
        /// </summary>
        /// <param name="array">The array to add.</param>
        public void Add(T[] array)
        {
            foreach (var x in array)
            {
                Add(x);
            }
        }
        /// <summary>
        /// Removes a element from the Set.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        /// <returns>Bool which depends if the removing was successful or not.</returns>
        public bool Remove(T element)
        {
            if (!Contains(element)) return false;
            array[HashCodeOfElement(element)] = default;
            count--;
            return true;
        }
        /// <summary>
        /// Chech if the element already exist in the Set.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>Bool result that depends if the element exist in the Set or not.</returns>
        public bool Contains(T element)
        {
            if (Compare(element, array[HashCodeOfElement(element)]) == 0) return true;
            return false;
        }
        /// <summary>
        /// CLears the Set.
        /// </summary>
        public void Clear()
        {
            array = new T[DEFAULT_CAPACITY];
            count = 0;
        }
        /// <summary>
        /// Returns array with all elements which don't have default value.
        /// </summary>
        /// <returns>The array.</returns>
        public T[] ToArray()
        {
            T[] arrayToReturn = new T[count];
            int index = 0;
            foreach (var x in array)
            {
                if (Compare(x, default) != 0)
                {
                    arrayToReturn[index] = x;
                    index++;
                }
            }
            return arrayToReturn;
        }
        //PRIVATE
        private void ExtendArray()
        {
            T[] newArray = new T[array.Length * 2];
            T[] oldArray = array;
            this.array = newArray;
            for (int i = 0; i < oldArray.Length; i++)
            {
                newArray[HashCodeOfElement(oldArray[i])] = oldArray[i];
            }
        }
        private int HashCodeOfElement(T element)
        {
            return Math.Abs(element.GetHashCode()) % array.Length;
        }
        private int Compare(T element1, T element2)
        {
            return Comparer<T>.Default.Compare(element1, element2);
        }
        #endregion Methods
        #region Properties
        /// <summary>
        /// Returns the number of all elements in the Set.
        /// </summary>
        public int Count => this.count;
        #endregion Properties
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace POI
{
    /// <summary>
    /// Represent a List maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class POIList<T>
    {
        #region Members
        private const int DEFAULT_CAPACITY = 16;
        private const int DEFAULT_INDEX = 0;
        private int index = 0;
        private int capacity;
        private int lastIndex = 1;
        private T[] array;
        #endregion Members
        #region Constructor
        public POIList(int capacity = 16)
        {
            this.array = new T[capacity];
            this.capacity = capacity;
        }
        public POIList(T[] otherList)
        {
            this.array = otherList;
            this.capacity = otherList.Length;
        }
        #endregion Constructor
        #region Methods
        //PUBLIC
        /// <summary>
        /// Add element to the List.
        /// </summary>
        /// <param name="element">Element to be added.</param>
        public void Add(T element)
        {
            if (index >= capacity) ExtendArray();
            array[index] = element;
            index++;
            lastIndex++;
        }
        /// <summary>
        /// Add element to specific place.
        /// </summary>
        /// <param name="index">The index to be put the element.</param>
        /// <param name="element">The element to be putted.</param>
        public void AddAt(int index, T element)
        {
            if (this.index == index)
            {
                Add(element);
                return;
            }
            if (index >= capacity)
            {
                ExtendArray();
                AddAt(index, element);
                return;
            }
            if (index > this.index)
            {
                lastIndex = index;
                this.index = index+1;
            }
            array[index] = element;
        }
        /// <summary>
        /// Gets the element from the List.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element.</returns>
        public T Get(int index)
        {
            return array[index];
        }
        /// <summary>
        /// Searching for the element with the given index.
        /// </summary>
        /// <param name="element">The element to search for.</param>
        /// <returns>The index of the element<br/>Returns -1 if didn't found it.</returns>
        public int FindIndex(T element)
        {
            if (Contains(element))
            {
                for (int i = 0; i < lastIndex+1; i++)
                {
                    if (Compare(element, array[i]) == 0) return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Removes the first element with the value from the List.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(T element)
        {
            if (!Contains(element)) throw new Exception("No such element in the list exist.");
            for (int i = 0; i < lastIndex; i++)
            {
                if (Comparer<T>.Default.Compare(array[i], element) == 0)
                {
                    this.array[i] = default;
                }
            }
        }
        /// <summary>
        /// Removes the element from the List with the given Index.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns>The removed element.</returns>
        public T RemoveAt(int index)
        {
            if (index < 0 || index > this.index) throw new IndexOutOfRangeException();
            T elementToReturn = array[index];
            array[index] = default;
            return elementToReturn;
        }
        /// <summary>
        /// Checks if the List contain the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>bool type.</returns>
        public bool Contains(T element)
        {
            foreach (T x in array)
            {
                if (Compare(x, element) == 0) return true;
            }
            return false;
        }
        /// <summary>
        /// Delete all elements from the list and restore the List to a new one.
        /// </summary>
        /// <returns>All elements from the List.</returns>
        public T[] Clear()
        {
            T[] arrayToReturn = array;
            capacity = DEFAULT_CAPACITY;
            array = new T[capacity];
            index = DEFAULT_INDEX;
            lastIndex = DEFAULT_INDEX+1;
            return arrayToReturn;
        }
        /// <summary>
        /// Returns all elements from the List.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            if (index == 0) return new T[capacity];
            T[] arrayToReturn = new T[lastIndex+1];
            for (int i = 0; i < lastIndex+1; i++)
            {
                arrayToReturn[i] = array[i];
            }
            return arrayToReturn;
        }
        //PRIVATE
        private void ExtendArray()
        {
            this.capacity *= 2;
            T[] oldArray = array;
            this.array = new T[capacity];
            for (int i = 0; i < oldArray.Length; i++)
            {
                array[i] = oldArray[i];
            }
        }
        private int Compare(T element1, T element2) => Comparer<T>.Default.Compare(element1, element2);
        #endregion Methods
        #region Properties
        public T this[int index]
        {
            get => Get(index);
            set => AddAt(index, value);
        }
        /// <summary>
        /// Returns the number of all elements in the List.
        /// </summary>
        public int Count => Elements.Length;
        /// <summary>
        /// Returns only elements which are added to the List.
        /// </summary>
        public T[] Elements
        {
            get => ToArray().Where(element => Compare(element, default) != 0).ToArray();
        }
        /// <summary>
        /// The capacity of the List.
        /// </summary>
        public int Capacity => capacity;
        #endregion Properties
    }
}

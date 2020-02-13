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
        private int count = 0;
        private int index = 0;
        private int capacity = 16;
        private int wantedCapacity = -1;
        private T[] array;
        #endregion Members
        #region Constructor
        public POIList()
        {
            this.array = new T[capacity];
        }
        public POIList(int capacity)
        {
            this.array = new T[capacity];
            this.capacity = capacity;
            this.wantedCapacity = capacity;
        }
        public POIList(T[] otherList)
        {
            this.array = otherList;
            this.capacity = otherList.Length;
            this.index = capacity;
            this.count = capacity;
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
            if (wantedCapacity > 0 && wantedCapacity <= index) throw new Exception("The List is full.");
            if (index >= capacity) ExtendArray();
            array[index] = element;
            index++;
            count++;
        }
        /// <summary>
        /// Add element to specific place.
        /// </summary>
        /// <param name="index">The index to be put the element.</param>
        /// <param name="element">The element to be putted.</param>
        public void AddAt(int index, T element)
        {
            if ((wantedCapacity > 0 && wantedCapacity <= index) || index >= capacity)
            {
                throw new IndexOutOfRangeException("The capacity of the List is less than the given index.");
            }
            if (this.index == index)
            {
                Add(element);
                return;
            }
            if (index > this.index)
            {
                this.index = index+1;
            }
            array[index] = element;
            count++;
        }
        /// <summary>
        /// Gets the element from the List.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element.</returns>
        public T Get(int index)
        {
            if (index >= capacity) throw new IndexOutOfRangeException("The capacity of the List is less than the given index.");
            return array[index];
        }
        /// <summary>
        /// Searching for the first element in the given index.
        /// </summary>
        /// <param name="element">The element to search for.</param>
        /// <returns>The index of the element<br/>Returns -1 if didn't found it.</returns>
        public int FindIndex(T element)
        {
            if (Contains(element))
            {
                for (int i = 0; i < index+1; i++)
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
            for (int i = 0; i < index; i++)
            {
                if (Comparer<T>.Default.Compare(array[i], element) == 0)
                {
                    Removing(i);
                    return;
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
            T valueToReturn = array[index];
            Removing(index);
            return valueToReturn;
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
            count = 0;
            return arrayToReturn;
        }
        /// <summary>
        /// Returns all elements from the List.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            int size = wantedCapacity >= 0 ? wantedCapacity : index;
            if (index == 0) return new T[size];
            T[] arrayToReturn = new T[size];
            for (int i = 0; i < size; i++)
            {
                arrayToReturn[i] = array[i];
            }
            return arrayToReturn;
        }
        /// <summary>
        /// Returns part of the array with Beginning and End.
        /// </summary>
        /// <param name="startIndex">Start index.</param>
        /// <param name="endIndex">End index.</param>
        /// <returns>The new Array.</returns>
        public T[] ToArray(int startIndex, int endIndex)
        {
            int tempCapacity = endIndex - startIndex;
            if (tempCapacity < 0) throw new IndexOutOfRangeException("Indexes cannot be zero or negative number.");
            T[] tempArray = new T[tempCapacity];
            for (int i = startIndex; i < endIndex; i++)
            {
                tempArray[i-startIndex] = array[i];
            }
            return tempArray;
        }
        /// <summary>
        /// Add array to the end of the List.
        /// </summary>
        /// <param name="array">The array to Join.</param>
        public void JoinArray(T[] array)
        {
            T[] newArray = new T[this.array.Length + array.Length];
            for (int i = 0; i < this.array.Length; i++)
            {
                newArray[i] = this.array[i];
            }
            for (int i = 0; i < array.Length; i++)
            {
                newArray[this.array.Length + i] = array[i];
            }
            this.array = newArray;
            this.capacity = this.array.Length;
            this.index = capacity;
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
        private void Removing(int index)
        {
            count--;
            if (wantedCapacity > 0)
            {
                array[index] = default;
                return;
            }
            T[] leftPartOfTheArray = ToArray(0, index);
            T[] rightPartOfTheArray = ToArray(index+1, array.Length);
            this.array = leftPartOfTheArray;
            JoinArray(rightPartOfTheArray);
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
        /// Returns part of the array with Beginning and End.
        /// </summary>
        /// <param name="startIndex">Start index.</param>
        /// <param name="endIndex">End index.</param>
        /// <returns>The new Array.</returns>
        public T[] this[int startIndex, int endIndex] => ToArray(startIndex, endIndex);
        /// <summary>
        /// Returns the number of all elements in the List.
        /// </summary>
        public int Count => count;
        /// <summary>
        /// The capacity of the List.
        /// </summary>
        public int Capacity => capacity;
        #endregion Properties
    }
}

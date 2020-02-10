using System;

namespace POI
{
    /// <summary>
    /// Represents a Stack structure maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class POIStack<T>
    {
        #region Members
        private int count = 0;
        private Node last = new Node(default);
        #endregion Members
        #region Internal Class
        private class Node
        {
            public T data;
            public Node previous;

            public Node(T data)
            {
                this.data = data;
                this.previous = null;
            }
        }
        #endregion Internal Class
        #region Methods
        /// <summary>
        /// Add element to the Stack.
        /// </summary>
        /// <param name="value">Element to be added.</param>
        public void Add(T value)
        {
            Node temp = new Node(value);
            temp.previous = last;
            last = temp;
            count++;
        }
        /// <summary>
        /// Remove the last added element in the Stack.
        /// </summary>
        /// <returns>The element.</returns>
        public T Remove()
        {
            if (last.previous == null) throw new Exception("There are not elements in the Stack.");
            T dataToReturn = last.data;
            last = last.previous;
            count--;
            return dataToReturn;
        }
        /// <summary>
        /// Returns the first added element without removing it.
        /// </summary>
        /// <returns>The first added element in the Stack.</returns>
        public T Peek()
        {
            return last.data;
        }
        #endregion Methods
        #region Properties
        /// <summary>
        /// The number of elements in the Stack.
        /// </summary>
        public int Count => count;
        #endregion Properties
    }
}

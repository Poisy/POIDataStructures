using System;
using System.Collections.Generic;

namespace POI
{
    /// <summary>
    /// Represents a Queue maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the Queue.</typeparam>
    public class POIQueue<T>
    {
        #region Members
        private int count;
        private Node lastAdded= null;
        #endregion Members
        #region Internal Class
        private class Node
        {
            public T data;
            public Node previous = null;
            public Node(T data)
            {
                this.data = data;
            }

        }
        #endregion Internal Class
        #region Methods
        //PUBLC
        /// <summary>
        /// Add element to the end of the Queue.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(T element)
        {

            if (lastAdded == null)
            {
                lastAdded = new Node(element);
                count++;
                return;
            }
            Node newNode = new Node(element);
            Add(lastAdded, newNode);
        }
        /// <summary>
        /// Remove the first added element and returns it.
        /// </summary>
        /// <returns>The first added element.</returns>
        public T Remove()
        {
            if (lastAdded == null) throw new Exception("Queue is empty.");
            T newLastNode = lastAdded.data;
            lastAdded = lastAdded.previous;
            count--;
            return newLastNode;
        }
        /// <summary>
        /// Returns the first added element without removing it from the Queue.
        /// </summary>
        /// <returns>The first added element.</returns>
        public T Peek()
        {
            return lastAdded.data;
        }
        /// <summary>
        /// Checks if the element is on the Queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Bool represents if is true of false.</returns>
        public bool Contains(T element)
        {
            Node temp = lastAdded;
            while (temp != null)
            {
                if (Comparer<T>.Default.Compare(temp.data, element) == 0) return true;
                temp = temp.previous;
            }
            return false;
        }
        //PRIVATE
        private void Add(Node last, Node newNode)
        {
            if (last.previous == null)
            {
                last.previous = newNode;
                count++;
                return;
            }
            Add(last.previous, newNode);
        }
        #endregion Methods
        #region Properties
        /// <summary>
        /// The amount of all elements in the Queue.
        /// </summary>
        public int Count => count;
        /// <summary>
        /// Array with all elements.
        /// </summary>
        public T[] Element
        { 
            get
            {
                T[] allElements = new T[count];
                Node tempNode = lastAdded;
                for (int i = 0; i < count; i++)
                {
                    allElements[i] = tempNode.data;
                    tempNode = tempNode.previous;
                }
                return allElements;
            }
        }
        #endregion Properties
    }
}

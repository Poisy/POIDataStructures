using System;
using System.Collections.Generic;

namespace POI
{
    /// <summary>
    /// Represents a Linked List maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class POILinkedList<T>
    {
        #region Members
        private Node first = null;
        private int count = 0;
        #endregion Members
        #region Internal Class
        private class Node
        {
            public T data;
            public Node next = null;
            public Node(T data)
            {
                this.data = data;
            }
        }
        #endregion Internal Class
        #region Methods
        //PUBLIC
        /// <summary>
        /// Add element to the beginning of the List.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void AddFirst(T element)
        {
            Node tempNode = new Node(element);
            tempNode.next = first;
            first = tempNode;
            count++;
        }
        /// <summary>
        /// Add element to the end of the List.
        /// </summary>
        /// <param name="element"></param>
        public void AddLast(T element)
        {
            Node tempNode = new Node(element);
            if (count == 0)
            {
                first = tempNode;
                count++;
                return;
            }
            AddLast(tempNode, first);
        }
        /// <summary>
        /// Add element in the specific index which will overwrite the current element in the List.
        /// </summary>
        /// <param name="index">The index which is the place to put the element.</param>
        /// <param name="element">The element to add in the List.</param>
        public void AddAt(int index, T element)
        {
            CheckIfIndexIsCorrect(index);
            CheckIfListIsEmpty();
            Node current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.next;
            }
            current.data = element;
        }
        /// <summary>
        /// Add element after the first found element equal to the first argument.
        /// </summary>
        /// <param name="afterElement">Element to search for.</param>
        /// <param name="element">The element to add in the List.</param>
        public void AddAfter(T afterElement, T element)
        {
            CheckIfListIsEmpty();
            if (!Contains(afterElement)) throw new Exception("There is no such element like the first argument in the List.");
            AddAfter(afterElement, element, first);
        }
        /// <summary>
        /// Add element before the first found element equal to the first argument.
        /// </summary>
        /// <param name="beforeElement">The element to search for.</param>
        /// <param name="element">The element to add to the List.</param>
        public void AddBefore(T beforeElement, T element)
        {
            CheckIfListIsEmpty();
            if (!Contains(beforeElement)) throw new Exception("There is no such element like the first argument in the List.");
            if (Comparer<T>.Default.Compare(first.data, beforeElement) == 0)
            {
                Node tempNode = new Node(element);
                tempNode.next = first;
                first = tempNode;
                count++;
                return;
            }
            AddBefore(beforeElement, element, first);
        }
        /// <summary>
        /// Removes and returns the first element in the List.
        /// </summary>
        /// <returns>The first element.</returns>
        public T RemoveFirst()
        {
            CheckIfListIsEmpty();
            T elementToReturn = first.data;
            first = first.next;
            count--;
            return elementToReturn;
        }
        /// <summary>
        /// Removes and returns the last element in the List.
        /// </summary>
        /// <returns>The last element in the list.</returns>
        public T RemoveLast()
        {
            CheckIfListIsEmpty();
            if (first.next == null)
            {
                T elementToReturn = first.data;
                first = null;
                count--;
                return elementToReturn;
            }
            return RemoveLast(first);
        }
        /// <summary>
        /// Removes a element from the List.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(T element)
        {
            CheckIfListIsEmpty();
            if (Contains(element))
            {
                if (Comparer<T>.Default.Compare(first.data, element) == 0)
                {
                    first = first.next;
                    count--;
                    return;
                }
                Remove(element, first);
            }
        }
        /// <summary>
        /// Removes the element from the List in the given index and returns it.
        /// </summary>
        /// <param name="index">The index to search for.</param>
        /// <returns>The element which is removed.</returns>
        public T RemoveAt(int index)
        {
            CheckIfIndexIsCorrect(index);
            CheckIfListIsEmpty();
            T element = this[index];
            if (Comparer<T>.Default.Compare(first.data, element) == 0)
            {
                first = first.next;
                count--;
                return element;
            }
            return RemoveAt(element, first);
        }
        /// <summary>
        /// Check if the List contains the element.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>Bool which deppends if the List contain the element or not.</returns>
        public bool Contains(T element)
        {
            if (first == null) return false;
            Node currentNode = first;
            while (currentNode != null)
            {
                if (Comparer<T>.Default.Compare(currentNode.data, element) == 0) return true;
                currentNode = currentNode.next;
            }
            return false;
        }
        /// <summary>
        /// Returns the index of the first element in the List which is equal to the given element.
        /// </summary>
        /// <param name="element">The element to search for.</param>
        /// <returns>The found index of the element.</returns>
        public int IndexOf(T element)
        {
            if (!Contains(element)) return -1;
            Node current = first;
            int count = 0;
            while (current != null)
            {
                if (Comparer<T>.Default.Compare(current.data, element) == 0)
                {
                    return count;
                }
                count++;
                current = current.next;
            }
            return -1;
        }
        /// <summary>
        /// Returns array with all elements from the List.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            CheckIfListIsEmpty();
            T[] allElements = new T[this.count];
            int count = 0;
            Node current = first;
            while (current != null)
            {
                allElements[count] = current.data;
                current = current.next;
                count++;
            }
            return allElements;
        }
        /// <summary>
        /// Returns array which is contain the elements from the List in the range of first and second arguments.
        /// </summary>
        /// <param name="startIndex">The index of where to start to take elements from the List.</param>
        /// <param name="endIndex">The index of where to end to take elements from the List.</param>
        /// <returns>The array.</returns>
        public T[] ToArray(int startIndex, int endIndex)
        {
            CheckIfListIsEmpty();
            int capacity = endIndex - startIndex;
            if (capacity < 1) throw new IndexOutOfRangeException("Cannot return array with these indexes.");
            T[] arrayToReturn = new T[capacity+1];
            Node tempNode = first;
            for (int i = 0; i < startIndex; i++)
            {
                tempNode = tempNode.next;
            }
            for (int i = 0; i <= capacity; i++)
            {
                arrayToReturn[i] = tempNode.data;
                tempNode = tempNode.next;
            }
            return arrayToReturn;
        }
        /// <summary>
        /// Removes all elements from the List.
        /// </summary>
        public void Clear()
        {
            first = null;
            count = 0;
        }
        //PRIVATE
        private void AddLast(Node newNode, Node currentNode)
        {
            if (currentNode.next == null)
            {
                currentNode.next = newNode;
                count++;
                return;
            }
            AddLast(newNode, currentNode.next);
        }
        private void AddAfter(T elementAfter, T element, Node currentNode )
        {
            if (Comparer<T>.Default.Compare(currentNode.data, elementAfter) == 0)
            {
                Node tempNode = currentNode.next;
                currentNode.next = new Node(element);
                currentNode.next.next = tempNode;
                count++;
                return;
            }
            AddAfter(elementAfter, element, currentNode.next);
        }
        private void AddBefore(T elementBefore, T element, Node currentNode)
        {
            if (Comparer<T>.Default.Compare(currentNode.next.data, elementBefore) == 0)
            {
                Node tempNode = currentNode.next;
                currentNode.next = new Node(element);
                currentNode.next.next = tempNode;
                count++;
                return;
            }
            AddBefore(elementBefore, element, currentNode.next);
        }
        private T RemoveLast(Node current)
        {
            if (current.next.next == null)
            {
                T elementToReturn = current.next.data;
                current.next = null;
                count--;
                return elementToReturn;
            }
            return RemoveLast(current.next);
        }
        private void Remove(T elementToRemove, Node current)
        {
            if (Comparer<T>.Default.Compare(current.next.data, elementToRemove) == 0)
            {
                current.next = current.next.next;
                count--;
                return;
            }
            Remove(elementToRemove, current.next);
        }
        private T RemoveAt(T elementToSearch, Node current)
        {
            if (Comparer<T>.Default.Compare(current.next.data, elementToSearch) == 0)
            {
                current.next = current.next.next;
                count--;
                return elementToSearch;
            }
            return RemoveAt(elementToSearch, current.next);
        }
        private void CheckIfListIsEmpty()
        {
            if (first == null) throw new Exception("The List is empty.");
        }
        private void CheckIfIndexIsCorrect(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException("Index cannot be negative number.");
            if (index + 1 > count) throw new IndexOutOfRangeException("No element exist on this index.");
        }
        #endregion Methods
        #region Properties
        public T this[int index]
        {
            get
            {
                CheckIfIndexIsCorrect(index);
                CheckIfListIsEmpty();
                Node current = first;
                for (int i = 0; i < index; i++)
                {
                    current = current.next;
                }
                return current.data;
            }
            set => AddAt(index, value);
        }
        /// <summary>
        /// The amount of all elements in the List.
        /// </summary>
        public int Count => count;
        /// <summary>
        /// Returns the first element in the List.
        /// </summary>
        public T First
        {
            get
            {
                CheckIfListIsEmpty();
                return first.data;
            }
        }
        /// <summary>
        /// Returns the last element in the List.
        /// </summary>
        public T Last
        {
            get
            {
                CheckIfListIsEmpty();
                Node currentNode = first;
                while (currentNode.next != null)
                {
                    currentNode = currentNode.next;
                }
                return currentNode.data;
            }
        }
        #endregion Properties
    }
}

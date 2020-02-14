using System;
using System.Collections.Generic;


namespace POI
{
    /// <summary>
    /// Represents a Tree maded by Poigrammer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class POITree<T>
    {
        #region Members
        private Node root = null;
        private int count;
        private Node traveler;
        #endregion Members
        #region Internal Class
        private class Node
        {
            public T data;
            public Node leftChild = null;
            public Node rightChild = null;
            public Node(T data)
            {
                this.data = data;
            }
        }
        #endregion Internal Class
        #region Methods
        //PUBLIC
        /// <summary>
        /// Add new element to the Tree.
        /// </summary>
        /// <param name="data">The element to add.</param>
        public void Add(T data)
        {
            if (root == null)
            {
                root = new Node(data);
                count++;
                return;
            }
            Add(new Node(data), root);
        }
        /// <summary>
        /// Removes element from the Tree.
        /// </summary>
        /// <param name="data">The element which need to be removed.</param>
        public void Remove(T data)
        {
            if (!Contains(data)) throw new Exception("This element does not exist in the Tree.");
            if (Compare(root.data, data) == 0)
            {
                ChangeRoot();
                return;
            }
            Remove(data, root);
        }
        /// <summary>
        /// Checks if element is in the Tree.
        /// </summary>
        /// <param name="data">The element to check for.</param>
        /// <returns>Bool which depends if the element is in the tree or not.</returns>
        public bool Contains(T data)
        {
            return Contains(data, root);
        }
        /// <summary>
        /// Clears the Tree.
        /// </summary>
        public void Clear()
        {
            root = null;
        }
        /// <summary>
        /// Restart the path your are following on the Tree. (Does not affect the Tree.)
        /// </summary>
        public void Restart()
        {
            traveler = null;
        }
        //PRIVATE
        private void Add(Node newNode, Node currentNode)
        {
            if (Compare(newNode.data, currentNode.data) == 0)
            {
                throw new Exception("This element already exist in the Tree.");
            }
            if (Compare(newNode.data, currentNode.data) > 0)
            {
                if (currentNode.rightChild == null)
                {
                    currentNode.rightChild = newNode;
                    count++;
                    return;
                }
                Add(newNode, currentNode.rightChild);
                return;
            }
            if (currentNode.leftChild == null)
            {
                currentNode.leftChild = newNode;
                count++;
                return;
            }
            Add(newNode, currentNode.leftChild);
        }
        private void Remove(T dataToRemove, Node currentNode)
        {
            if (currentNode.leftChild != null)
            {
                if (Compare(dataToRemove, currentNode.leftChild.data) == 0)
                {
                    RemoveLeftChild(currentNode);
                    return;
                }
            }
            if (Compare(dataToRemove, currentNode.rightChild.data) == 0)
            {
                RemoveRightChild(currentNode);
                return;
            }
            if (Compare(dataToRemove, currentNode.data) > 0)
            {
                Remove(dataToRemove, currentNode.rightChild);
                return;
            }
            Remove(dataToRemove, currentNode.leftChild);
        }
        private void RemoveLeftChild(Node parent)
        {
            if (parent.leftChild.leftChild == null && parent.leftChild.rightChild != null)
            {
                parent.leftChild = parent.leftChild.rightChild;
                count--;
                return;
            }
            if (parent.leftChild.leftChild != null && parent.leftChild.rightChild == null)
            {
                parent.leftChild = parent.leftChild.leftChild;
                count--;
                return;
            }
            if (parent.leftChild.leftChild != null && parent.leftChild.rightChild != null)
            {
                RemoveLeftChild(parent, parent.leftChild.rightChild);
                parent.leftChild = parent.leftChild.rightChild;
                count--;
                return;
            }
            parent.leftChild = null;
            count--;
        }
        private void RemoveLeftChild(Node parent, Node currentNode)
        {
            if (currentNode.leftChild == null)
            {
                currentNode.leftChild = parent.leftChild.leftChild;
                return;
            }
            RemoveLeftChild(parent, currentNode.leftChild);
        }
        private void RemoveRightChild(Node parent)
        {
            if (parent.rightChild.leftChild == null && parent.rightChild.rightChild != null)
            {
                parent.rightChild = parent.rightChild.rightChild;
                count--;
                return;
            }
            if (parent.rightChild.leftChild != null && parent.rightChild.rightChild == null)
            {
                parent.rightChild = parent.rightChild.leftChild;
                count--;
                return;
            }
            if (parent.rightChild.leftChild != null && parent.rightChild.rightChild != null)
            {
                RemoveRightChild(parent, parent.rightChild.leftChild);
                parent.rightChild = parent.rightChild.leftChild;
                count--;
                return;
            }
            parent.rightChild = null;
            count--;
        }
        private void RemoveRightChild(Node parent, Node currentNode)
        {
            if (currentNode.rightChild == null)
            {
                currentNode.rightChild = parent.rightChild.rightChild;
                return;
            }
            RemoveRightChild(parent, currentNode.rightChild);
        }
        private void ChangeRoot()
        {
            if (root.leftChild == null)
            {
                root = root.rightChild;
                count--;
                return;
            }
            if (root.rightChild == null)
            {
                root = root.leftChild;
                count--;
                return;
            }
            if (root.leftChild.rightChild == null)
            {
                Node tempNode = root.rightChild;
                root.leftChild.rightChild = tempNode;
                root = root.leftChild;
                count--;
                return;
            }
            ChangeRootFromLeft(root.rightChild, root.leftChild);
            root = root.leftChild;
            count--;
        }
        private void ChangeRootFromLeft(Node rigthNode, Node currentNode)
        {
            if (currentNode.rightChild == null)
            {
                currentNode.rightChild = rigthNode;
                return;
            }
            ChangeRootFromLeft(rigthNode, currentNode.rightChild);
        }
        private bool Contains(T data, Node currentNode)
        {
            if (Compare(data, currentNode.data) == 0) return true;
            if (Compare(data, currentNode.data) > 0)
            {
                if (currentNode.rightChild == null) return false;
                return Contains(data, currentNode.rightChild);
            }
            if (currentNode.leftChild == null) return false;
            return Contains(data, currentNode.leftChild);
        }
        private int Compare(T data1, T data2)
        {
            return Comparer<T>.Default.Compare(data1, data2);
        }
        private T Travel(int direction)
        {
            if (traveler == null)
            {
                traveler = root;
            }
            try
            {
                if (direction == 0) traveler = traveler.leftChild;
                else traveler = traveler.rightChild;
            }
            catch (Exception)
            {
                throw new Exception("There is no next path.");
            }
            return traveler.data;
        }
        #endregion Methods
        #region Properties
        /// <summary>
        /// The amount of all elements in the Tree.
        /// </summary>
        public int Count => count;
        /// <summary>
        /// Go to the Right element in the Tree.
        /// </summary>
        public T Right => Travel(1);
        /// <summary>
        /// Go to the Left element in the Tree.
        /// </summary>
        public T Left => Travel(0);
        /// <summary>
        /// The first added element in the Tree.
        /// </summary>
        public T Root => root.data;
        #endregion Properties
    }
}

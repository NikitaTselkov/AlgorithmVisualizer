using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class TreeNode<T>
    {
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();
        
        public TreeNode<T> Parent { get; set; }

        public T Item { get; set; }

        public TreeNode(T item)
        {
            Item = item;
        }

        public TreeNode<T> FindNode(T item)
        {
            return Find(this, item);
        }

        private TreeNode<T> Find(TreeNode<T> node, T item)
        {
            if (item.Equals(node.Item))
                return node;

            foreach (var child in node.Children)
            {
                var result = Find(child, item);
                if (result != null)
                    return result;
            }

            return null;
        }

        public TreeNode<T> AddChild(T item)
        {
            TreeNode<T> nodeItem = new TreeNode<T>(item);
            nodeItem.Parent = this;
            Children.Add(nodeItem);
            return nodeItem;
        }
    }
}

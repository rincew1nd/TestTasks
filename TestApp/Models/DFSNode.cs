using System.Collections.Generic;

namespace TestApp.Models
{
    public class TreeNode
    {
        public int Number;
        public bool IsVisited;
        public int Level;
        public List<TreeNode> Neighbors;

        public TreeNode(int number)
        {
            Number = number;
            Neighbors = new List<TreeNode>();
            IsVisited = false;
            Level = -1;
        }

        public TreeNode AddNeighbor(TreeNode node)
        {
            if (!Neighbors.Contains(node))
                Neighbors.Add(node);
            return this;
        }

        public override string ToString()
        {
            return $"{Number}({Level})";
        }
    }
}
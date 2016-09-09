using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestApp.Models;

namespace TestApp.FourthTaskFolder
{
    /// I think the best way of detecting List loops is DFS (Depth-First Search) | But not optimal :(
    /// First of all we should convert List to DFS forest
    /// Then using the algorithm find loops in forest

    public class FourthTask
    {
        public List<ListNode> List;

        public FourthTask(int size)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            List = new List<ListNode>();

            for (var i = 0; i < size; i++)
            {
                List.Add(new ListNode(i));
            }
            for (var i = 0; i < size; i++)
            {
                if (rnd.Next(0, 10) > 1)
                    List[i].NextNode = List[rnd.Next(0, size)];
            }
        }

        /// <summary>
		/// Depth first search for looping trees
		/// </summary>
		public List<TreeNode> BfsSearch(List<ListNode> list)
        {
            Console.WriteLine($"\nDepth first search for looping trees");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //List for result
            var loopNodes = new List<TreeNode>(list.Count);

            //Initialize DFS forest list
            var forest = new List<TreeNode>(list.Count);

            //Fill forest with not connected ListNode
            forest.AddRange(list.Select(t => new TreeNode(t.Number)));

            //Make forest connected
            foreach (var listNode in list)
            {
                var nextTreeNode = list.IndexOf(listNode.NextNode);
                if (nextTreeNode == -1) continue;
                var currentTreeNode = list.IndexOf(listNode);
                forest[currentTreeNode].AddNeighbor(forest[nextTreeNode]);
            }

            //Set levels for all elements by DFS
            SetLevel(forest.First(), 0);
            while (true)
            {
                TreeNode notVisited = null;
                foreach (var treeNode in forest)
                {
                    if (treeNode.IsVisited == false)
                    {
                        notVisited = treeNode;
                        break;
                    }
                }
                if (notVisited != null)
                {
                    SetLevel(notVisited, 0);
                }
                else break;
            }
            
            //Find all looping elements
            foreach (var treeNode in forest)
            {
                foreach (var treeNodeNeighbor in treeNode.Neighbors)
                {
                    if (treeNode.Level >= treeNodeNeighbor.Level)
                    {
                        loopNodes.Add(treeNode);
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"Time elapsed {stopwatch.Elapsed}");

            //Print all elements with connections and looped nodes
            //forest.ForEach(elem => Console.WriteLine($"{elem} to {string.Join(" | ", elem.Neighbors)}"));
            foreach (var loopNode in loopNodes)
                Console.WriteLine($"{loopNode} is a loop node");


            return loopNodes;
        }

        /// <summary>
		/// Depth first search for looping trees
		/// </summary>
		public void BfsSearchSlowLINQ(List<ListNode> list)
        {
            Console.WriteLine($"\nDepth first search for looping trees with LINQ");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //List for result
            var loopNodes = new List<TreeNode>(list.Count);

            //Initialize DFS forest list
            var forest = new List<TreeNode>(list.Count);

            //Fill forest with not connected ListNode
            forest.AddRange(list.Select(t => new TreeNode(t.Number)));

            //Make forest connected
            foreach (var listNode in list)
            {
                var nextTreeNode = list.IndexOf(listNode.NextNode);
                if (nextTreeNode == -1) continue;
                var currentTreeNode = list.IndexOf(listNode);
                forest[currentTreeNode].AddNeighbor(forest[nextTreeNode]);
            }

            //Set levels for all elements by DFS
            SetLevel(forest.First(), 0);
            while (forest.Any(t => !t.IsVisited))
                    SetLevel(forest.First(t => !t.IsVisited), 0);

            //Find all looping elements
            loopNodes.AddRange(
                from treeNode in forest
                from treeNodeNeighbor in treeNode.Neighbors
                where treeNode.Level >= treeNodeNeighbor.Level
                select treeNode);

            stopwatch.Stop();

            //Print all elements with connections and looped nodes
            forest.ForEach(elem => Console.WriteLine($"{elem} to {string.Join(" | ", elem.Neighbors)}"));
            foreach (var loopNode in loopNodes)
                Console.WriteLine($"{loopNode} is a loop node");

            Console.WriteLine($"Time elapsed {stopwatch.Elapsed}");
        }

        public void SetLevel(TreeNode node, int level)
        {
            node.Level = level;
            node.IsVisited = true;

            foreach (var neighborNode in node.Neighbors)
            {
                if (neighborNode == node) break;
                if ((neighborNode.Level == node.Level && neighborNode.IsVisited == true) ||
                    (neighborNode.IsVisited == true && !IsConnected(node, node.Neighbors.First())) ||
                    (!neighborNode.IsVisited))
                    SetLevel(neighborNode, node.Level+1);
            }
        }

        public bool IsConnected(TreeNode node1, TreeNode node2)
        {
            while (true)
            {
                if (node2.Neighbors.Count == 0)
                    return false;
                if (node2.Neighbors.First() == node1)
                    return true;
                return IsConnected(node1, node2.Neighbors.First());
            }
        }
    }


}

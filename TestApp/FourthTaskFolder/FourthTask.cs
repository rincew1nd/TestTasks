using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestApp.FourthTaskFolder
{
    /// I think the best way of detecting List loops is DFS (Depth-First Search)
    /// First of all we should convert List to DFS forest
    /// Then using the algorithm find loops in forest

    public class FourthTask
    {
        public FourthTask(int size)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var list = new List<ListNode>();

            for (var i = 0; i < size; i++)
            {
                list.Add(new ListNode(i));
            }
            for (var i = 0; i < size; i++)
            {
                if (rnd.Next(0, 10) > 1)
                    list[i].NextNode = list[rnd.Next(0, size)];
            }
            
            BfsSearch(list);
            BfsSearchSlowLINQ(list);
        }

        /// <summary>
		/// Depth first search for looping trees
		/// </summary>
		public void BfsSearch(List<ListNode> list)
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
            
            //Print all elements with connections and looped nodes
            forest.ForEach(elem => Console.WriteLine($"{elem} to {string.Join(" | ", elem.Neighbors)}"));
            foreach (var loopNode in loopNodes)
                Console.WriteLine($"{loopNode} is a loop node");

            Console.WriteLine($"Time elapsed {stopwatch.Elapsed}");
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
                    (!neighborNode.IsVisited))
                    SetLevel(neighborNode, node.Level+1);
            }
        }
    }


}

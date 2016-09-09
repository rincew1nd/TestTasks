using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.FourthTaskFolder;
using TestApp.Models;

namespace TestApp
{
    public class FifthTask
    {
        public List<ListNode> List;

        public FifthTask(int size)
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

        public List<ListNode> FindLoops(List<ListNode> list)
        {
            Console.WriteLine($"\nDefault loop for testing one-way list loops");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loopedNodes = new List<ListNode>();
            var visitedList = new Dictionary<ListNode, bool>();
            foreach (var node in list)
            {
                visitedList[node] = true;
                if (node.NextNode != null && visitedList.ContainsKey(node.NextNode))
                {
                    var lastNode = node.NextNode;
                    while (true)
                    {
                        //First neighbor node contais no element
                        if (lastNode.NextNode == null)
                            break;

                        //For 1 cycle loop
                        if (lastNode.NextNode?.NextNode == lastNode)
                        {
                            lastNode = lastNode?.NextNode;
                            break;
                        }

                        //For endles loop
                        if (loopedNodes.Contains(lastNode.NextNode))
                            break;

                        //Main check
                        if (lastNode.NextNode != node)
                        {
                            lastNode = lastNode.NextNode;
                        }
                        else
                        {
                            lastNode = lastNode.NextNode;
                            break;
                        }
                    }
                    if (lastNode == node)
                        loopedNodes.Add(lastNode);
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Time elapsed {stopwatch.Elapsed}");
            
            //foreach (var listNode in list)
            //    Console.WriteLine($"Node {listNode.Number} to {listNode.NextNode?.Number}");
            foreach (var node in loopedNodes)
                Console.WriteLine($"Node {node.Number} is naughty loopy link");

            return loopedNodes;
        }
    }
}

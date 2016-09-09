using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestApp.FourthTaskFolder;
using TestApp.Models;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
            var firstTask = new FirstTask(50, 50);
		    firstTask.CalcIntersectionCustom(firstTask.first, firstTask.second);

            var secondTask = new SecondTask(50, 50);
		    secondTask.CalcUnionCustom(secondTask.first, secondTask.second);

            var fourthTask = new FourthTask(10);
            fourthTask.BfsSearch(fourthTask.List);

            var fifthTask = new FifthTask(10);
            fifthTask.FindLoops(fifthTask.List);

		    Console.ReadLine();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
	public class FirstTask
	{
	    private readonly Stopwatch _stopwatch;
        public int[] first;
        public int[] second;

        /// <summary>
        /// Intersection of two arrays
        /// </summary>
        /// <param name="firstArraySize">Size of first array</param>
        /// <param name="secondArraySize">Size of second array</param>
        public FirstTask(int firstArraySize, int secondArraySize)
		{
		    var rnd = new Random((int)DateTime.Now.Ticks);
			_stopwatch = new Stopwatch();
			first = new int[firstArraySize];
			second = new int[secondArraySize];

			first[0] = -30 + rnd.Next(0, 10);
			second[0] = -30 + rnd.Next(0, 10);

			for (var i = 1; i < first.Length; i++)
				first[i] = first[i-1] + rnd.Next(1, 3);
			for (var i = 1; i < second.Length; i++)
				second[i] = second[i-1] + rnd.Next(1, 3);

			Console.WriteLine(Environment.NewLine + "Intersection");
        }

		/// <summary>
		/// Pass two arrays to find intersection between them
		/// </summary>
		/// <param name="first">First array</param>
		/// <param name="second">Second array</param>
		/// <returns>Array with result of intersection</returns>
		public int[] CalcIntersectionCustom(int[] first, int[] second)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			var intersection = new int[(first.Length > second.Length) ? first.Length : second.Length];
			
			var firstIndex = 0;
			var secondIndex = 0;
			var intersectionIndex = 0;
			
			while(true)
			{
				if (firstIndex == first.Length || secondIndex == second.Length)
				{
					break;
				}

                if (first[firstIndex] == second[secondIndex])
				{
					if (intersectionIndex == 0 ||
						(intersectionIndex != 0 && intersection[intersectionIndex-1] != first[firstIndex]))
					{
						intersection[intersectionIndex] = first[firstIndex];
						intersectionIndex++;
					}
					firstIndex++;
					secondIndex++;
				}
				else if (first[firstIndex] > second[secondIndex])
				{
					secondIndex++;
				}
				else
				{
					firstIndex++;
				}
			}

			var intersectionCopy = new int[intersectionIndex];
			Array.Copy(intersection, intersectionCopy, intersectionIndex);

			_stopwatch.Stop();
			Console.WriteLine($"Custom method  | Time elapsed {_stopwatch.Elapsed}");

			//PrintResult(intersectionCopy);

			return intersectionCopy;
		}

		/// <summary>
		/// Find intersection between two arrays
		/// </summary>
		/// <param name="first">First array</param>
		/// <param name="second">Second array</param>
		/// <returns>Array with result of intersection</returns>
		public int[] CalcIntersectionDefault(int[] first, int[] second)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			var intersection = first.Intersect(second).ToArray();

			_stopwatch.Stop();
			Console.WriteLine($"Default method | Time elapsed {_stopwatch.Elapsed}");

			return intersection;
        }

		public void PrintResult(int[] intersection)
		{
			//Console.WriteLine(string.Join(", ", first));
			//Console.WriteLine(string.Join(", ", second));
			Console.WriteLine("-----------------");
			Console.WriteLine(string.Join(", ", intersection));
			Console.WriteLine("-----------------");
		}
	}
}

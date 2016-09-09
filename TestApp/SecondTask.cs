using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
	class SecondTask
	{
	    private readonly Stopwatch _stopwatch;

		/// <summary>
		/// Union of two arrays
		/// </summary>
		/// <param name="firstArraySize">Size of first array</param>
		/// <param name="secondArraySize">Size of second array</param>
		public SecondTask(int firstArraySize, int secondArraySize)
		{
		    var rnd = new Random((int)DateTime.Now.Ticks);
			_stopwatch = new Stopwatch();
			var first = new int[firstArraySize];
			var second = new int[secondArraySize];

			first[0] = 0 + rnd.Next(0, 10);
			second[0] = 0 + rnd.Next(0, 10);

			for (var i = 1; i < first.Length; i++)
				first[i] = first[i - 1] + rnd.Next(1, 3);
			for (var i = 1; i < second.Length; i++)
				second[i] = second[i - 1] + rnd.Next(1, 3);

			Console.WriteLine(Environment.NewLine + "Union");
			CalcUnionHard(first, second);
			CalcUnionEasy(first, second);
		}

		/// <summary>
		/// Pass two arrays to find intersection between them
		/// </summary>
		/// <param name="first">First array</param>
		/// <param name="second">Second array</param>
		/// <returns>Array with result of intersection</returns>
		public void CalcUnionHard(int[] first, int[] second)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			var union = new int[first.Length + second.Length];

			var firstIndex = 0;
			var secondIndex = 0;
			var unionIndex = 0;

			while (true)
			{
				if (firstIndex == first.Length && secondIndex == second.Length)
				{
					break;
				}
				
				if (firstIndex == first.Length || secondIndex == second.Length)
				{
					if (firstIndex == first.Length)
					{
						if (unionIndex == 0 || union[unionIndex - 1] != second[secondIndex])
						{
							union[unionIndex] = second[secondIndex];
							unionIndex++;
						}
						secondIndex++;
					}
					else
					{
						if (unionIndex == 0 || union[unionIndex - 1] != first[firstIndex])
						{
							union[unionIndex] = first[firstIndex];
							unionIndex++;
						}
						firstIndex++;
					}
					continue;
				}

				if (first[firstIndex] > second[secondIndex])
				{
					if (unionIndex == 0 || union[unionIndex - 1] != second[secondIndex])
					{
						union[unionIndex] = second[secondIndex];
						unionIndex++;
					}
					secondIndex++;
				}
				else if (first[firstIndex] < second[secondIndex])
				{
					if (unionIndex == 0 || union[unionIndex - 1] != first[firstIndex])
					{
						union[unionIndex] = first[firstIndex];
						unionIndex++;
					}
					firstIndex++;
				}
				else
				{
					if (unionIndex == 0 || union[unionIndex - 1] != second[secondIndex])
					{
						union[unionIndex] = second[secondIndex];
						unionIndex++;
					}
					firstIndex++;
					secondIndex++;
				}
			}

			var unionCopy = new int[unionIndex];
			Array.Copy(union, unionCopy, unionIndex);

			_stopwatch.Stop();
			Console.WriteLine($"Custom method  | Time elapsed {_stopwatch.Elapsed}");

			//PrintResult(unionCopy);
		}

		/// <summary>
		/// Find union of two arrays
		/// </summary>
		/// <param name="first">First array</param>
		/// <param name="second">Second array</param>
		/// <returns>Array with result of intersection</returns>
		public void CalcUnionEasy(int[] first, int[] second)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			var union = first.Union(second).ToArray();

			_stopwatch.Stop();
			Console.WriteLine($"Default method | Time elapsed {_stopwatch.Elapsed}");

			//PrintResult(union);
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

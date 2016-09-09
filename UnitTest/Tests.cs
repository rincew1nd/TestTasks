using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TestApp;
using TestApp.FourthTaskFolder;
using TestApp.Models;

namespace UnitTest
{
    [TestFixture]
    class Test
    {
        [Test]
        public void FirstTaskTest()
        {
            var ft = new FirstTask(10, 10);
            
            Assert.AreEqual(
                ft.CalcIntersectionCustom(new[] {0, 4, 6, 9, 10}, new[] {0, 1, 3, 5, 8, 9, 11}),
                new[] {0, 9}
            );

            Assert.AreEqual(
                ft.CalcIntersectionCustom(new[] { 0, 0, 3, 3, 11 }, new[] { 0, 1, 3, 5, 8, 9, 11 }),
                new[] { 0, 3, 11 }
            );

            Assert.AreEqual(
                ft.CalcIntersectionCustom(new[] { 0, 4, 6, 9, 10, 12 }, new[] { 0, 12 }),
                new[] { 0, 12 }
            );
        }

        [Test]
        public void SecondTaskTest()
        {
            var st = new SecondTask(10, 10);

            Assert.AreEqual(
                st.CalcUnionCustom(new[] { 0, 4, 6, 9, 10 }, new[] { 0, 1, 3, 5, 8, 9, 11 }),
                new[] { 0, 1, 3, 4, 5, 6, 8, 9, 10, 11 }
            );

            Assert.AreEqual(
                st.CalcUnionCustom(new[] { 0, 0, 3, 3, 11 }, new[] { 0, 1, 3, 5, 8, 9, 11 }),
                new[] { 0, 1, 3, 5, 8, 9, 11 }
            );

            Assert.AreEqual(
                st.CalcUnionCustom(new[] { 0, 4, 6, 9, 10, 12 }, new[] { 0, 11, 12 }),
                new[] { 0, 4, 6, 9, 10, 11, 12 }
            );
        }


        [Test]
        public void FourthTaskTest()
        {
            var ft = new FourthTask(10);

            var list = ft.List;
            list[0].NextNode = list[0];
            list[1].NextNode = list[2];
            list[2].NextNode = list[3];
            list[3].NextNode = list[1];
            list[4].NextNode = list[5];
            list[5].NextNode = list[4];
            list[6].NextNode = list[0];
            list[7].NextNode = null;
            list[8].NextNode = list[9];
            list[9].NextNode = list[7];

            var result = ft.BfsSearch(list);
            
            Assert.AreEqual(
                result.Select(r => r.Number).ToArray(),
                new[] { 0, 3, 5 }
            );
        }


        [Test]
        public void FifthTaskTest()
        {
            var ft = new FifthTask(10);

            var list = ft.List;
            list[0].NextNode = list[0];
            list[1].NextNode = list[2];
            list[2].NextNode = list[3];
            list[3].NextNode = list[1];
            list[4].NextNode = list[5];
            list[5].NextNode = list[4];
            list[6].NextNode = list[0];
            list[7].NextNode = null;
            list[8].NextNode = list[9];
            list[9].NextNode = list[7];

            var result = ft.FindLoops(list);

            Assert.AreEqual(
                result.Select(r => r.Number).ToArray(),
                new[] { 0, 3, 5 }
            );
        }

        public static void Main(string[] args)
        {
            
        }
    }
}

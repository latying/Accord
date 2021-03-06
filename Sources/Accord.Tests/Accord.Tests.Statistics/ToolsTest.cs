﻿// Accord Unit Tests
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Tests.Statistics
{
    using Accord.Statistics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Accord.Math;

    using Tools = Accord.Statistics.Tools;
    using System;

    [TestClass()]
    public class ToolsTest
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void StandardDeviationTest4()
        {
            double[][] matrix = 
            {
                new double[] { 2, -1.0, 5 },
                new double[] { 7,  0.5, 9 },
            };

            double[] means = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(means.IsEqual(4.5000, -0.2500, 7.0000));

            double[] stdev = Accord.Statistics.Tools.StandardDeviation(matrix, means);
            Assert.IsTrue(stdev.IsEqual(3.5355339059327378, 1.0606601717798212, 2.8284271247461903));

            double[] stdev2 = Accord.Statistics.Tools.StandardDeviation(matrix);
            Assert.IsTrue(stdev2.IsEqual(stdev));
        }

        [TestMethod()]
        public void StandardDeviationTest3()
        {
            double[,] matrix = 
            {
                { 2, -1.0, 5 },
                { 7,  0.5, 9 },
            };

            double[] means = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(means.IsEqual(4.5000, -0.2500, 7.0000));

            double[] stdev = Accord.Statistics.Tools.StandardDeviation(matrix, means);
            Assert.IsTrue(stdev.IsEqual(3.5355339059327378, 1.0606601717798212, 2.8284271247461903));

            double[] stdev2 = Accord.Statistics.Tools.StandardDeviation(matrix);
            Assert.IsTrue(stdev2.IsEqual(stdev));
        }

        [TestMethod()]
        public void StandardDeviationTest2()
        {
            double[] values = { -5, 0.2, 2 };
            double expected = 3.6350149013908224;
            double actual = Tools.StandardDeviation(values);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void MeanTest4()
        {
            double[,] matrix = 
            {
                { 2, -1.0, 5 },
                { 7,  0.5, 9 },
            };

            double[] sums = Matrix.Sum(matrix);
            Assert.IsTrue(sums.IsEqual(9.0, -0.5, 14.0));

            double[] expected = { 4.5000, -0.2500, 7.0000 };
            double[] actual = Tools.Mean(matrix, sums);

            Assert.IsTrue(expected.IsEqual(actual));
        }

        [TestMethod()]
        public void MeanTest3()
        {
            double[] values = { -5, 0.2, 2 };
            double expected = -0.93333333333333324;
            double actual = Tools.Mean(values);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MeanTest()
        {
            double[,] matrix = 
            {
                { 2, -1.0, 5 },
                { 7,  0.5, 9 },
            };

            double[] rowMean = Accord.Statistics.Tools.Mean(matrix, 0);
            Assert.IsTrue(rowMean.IsEqual(4.5000, -0.2500, 7.0000));

            double[] colMean = Accord.Statistics.Tools.Mean(matrix, 1);
            Assert.IsTrue(colMean.IsEqual(2, 5.5));
        }


        [TestMethod()]
        public void MedianTest1()
        {
            double[] values;
            double expected;
            double actual;

            values = new double[] { -5, 0.2, 2, 5, -0.7 };
            expected = 0.2;
            actual = Tools.Median(values, false);
            Assert.AreEqual(expected, actual);

            values = new double[] { -5, 0.2, 2, 5 };
            expected = 1.1;
            actual = Tools.Median(values, false);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MedianTest2()
        {
            double[][] matrix = 
            {
                new double[] {   2, -1.0,  5 },
                new double[] {   7,  1.7,  9 },
                new double[] {  -4,  2.5,  6 },
                new double[] { 0.2,  0.5, -2 },
            };

            double[] mean = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(mean.IsEqual(1.3000, 0.9250, 4.5000));

            double[] median = Accord.Statistics.Tools.Median(matrix);
            Assert.IsTrue(median.IsEqual(1.1000, 1.1000, 5.5000));


            matrix = matrix.Transpose();
            mean = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(mean.IsEqual(2.0000, 5.8999999999999995, 1.5000, -0.43333333333333335));

            median = Accord.Statistics.Tools.Median(matrix);
            Assert.IsTrue(median.IsEqual(2.0000, 7.0000, 2.5000, 0.2000));
        }

        [TestMethod()]
        public void MedianTest()
        {
            double[,] matrix = 
            {
                {   2, -1.0,  5 },
                {   7,  1.7,  9 },
                {  -4,  2.5,  6 },
                { 0.2,  0.5, -2 },
            };

            double[] mean = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(mean.IsEqual(1.3000, 0.9250, 4.5000));

            double[] median = Accord.Statistics.Tools.Median(matrix);
            Assert.IsTrue(median.IsEqual(1.1000, 1.1000, 5.5000));


            matrix = matrix.Transpose();
            mean = Accord.Statistics.Tools.Mean(matrix);
            Assert.IsTrue(mean.IsEqual(2.0000, 5.8999999999999995, 1.5000, -0.43333333333333335));

            median = Accord.Statistics.Tools.Median(matrix);
            Assert.IsTrue(median.IsEqual(2.0000, 7.0000, 2.5000, 0.2000));
        }


        [TestMethod()]
        public void ModeTest1()
        {
            double[] values = { 3, 3, 1, 4 };
            double expected = 3;
            double actual = Tools.Mode(values);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ModeTest()
        {
            double[,] matrix = 
            {
                { 3, 3, 1, 4 },
                { 0, 1, 1, 1 },
                { 0, 1, 2, 4 },
            };


            double[] expected = { 0, 1, 1, 4 };
            double[] actual = Tools.Mode(matrix);
            Assert.IsTrue(expected.IsEqual(actual));
        }


        [TestMethod()]
        public void CovarianceTest()
        {
            double[,] matrix = new double[,]
            {
                { 4.0, 2.0, 0.60 },
                { 4.2, 2.1, 0.59 },
                { 3.9, 2.0, 0.58 },
                { 4.3, 2.1, 0.62 },
                { 4.1, 2.2, 0.63 }
            };


            double[,] expected = new double[,]
            {
                { 0.02500, 0.00750, 0.00175 },
                { 0.00750, 0.00700, 0.00135 },
                { 0.00175, 0.00135, 0.00043 },
            };


            double[,] actual = Tools.Covariance(matrix);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));

            actual = Tools.Covariance(matrix, 0);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));


            matrix = matrix.Transpose();

            actual = Tools.Covariance(matrix, 1);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));
        }

        [TestMethod()]
        public void CovarianceTest2()
        {
            double[][] matrix = new double[,]
            {
                { 4.0, 2.0, 0.60 },
                { 4.2, 2.1, 0.59 },
                { 3.9, 2.0, 0.58 },
                { 4.3, 2.1, 0.62 },
                { 4.1, 2.2, 0.63 }
            }.ToArray();


            double[,] expected = new double[,]
            {
                { 0.02500, 0.00750, 0.00175 },
                { 0.00750, 0.00700, 0.00135 },
                { 0.00175, 0.00135, 0.00043 },
            };


            double[,] actual = Tools.Covariance(matrix);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));

            actual = Tools.Covariance(matrix, 0);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));


            matrix = matrix.Transpose();

            actual = Tools.Covariance(matrix, 1);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));
        }

        [TestMethod()]
        public void CovarianceTest3()
        {
            double[][] matrix = new double[,]
            {
                { 4.0, 2.0, 0.60 },
                { 4.2, 2.1, 0.59 },
                { 3.9, 2.0, 0.58 },
                { 4.3, 2.1, 0.62 },
                { 4.1, 2.2, 0.63 }
            }.ToArray();


            double[,] expected = new double[,]
            {
                { 0.02500, 0.00750, 0.00175 },
                { 0.00750, 0.00700, 0.00135 },
                { 0.00175, 0.00135, 0.00043 },
            };

            double[] weights = { 0.2, 0.2, 0.2, 0.2, 0.2 };

            double[,] actual = Tools.WeightedCovariance(matrix, weights, 0);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));

            matrix = matrix.Transpose();

            actual = Tools.WeightedCovariance(matrix, weights, 1);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.0001));
        }

        [TestMethod()]
        public void CovarianceTest4()
        {
            double[] u = { -2, 1, 5 };
            double[] v = { 7, 1, 6 };

            double[,] expected =
            {
                { 12.3333,   -0.8333 },
                { -0.8333,   10.3333}
            };

            double[,] actual = Tools.Covariance(u, v);

            Assert.IsTrue(expected.IsEqual(actual, 0.0001));
        }



        [TestMethod()]
        public void CorrelationTest()
        {
            // http://www.solvemymath.com/online_math_calculator/statistics/descriptive/correlation.php

            double[,] matrix = new double[,]
            {
                { 4.0, 2.0, 0.60 },
                { 4.2, 2.1, 0.59 },
                { 3.9, 2.0, 0.58 },
                { 4.3, 2.1, 0.62 },
                { 4.1, 2.2, 0.63 }
            };


            double[,] expected = new double[,]
            {
                { 1.000000, 0.5669467, 0.533745 },
                { 0.566946, 1.0000000, 0.778127 },
                { 0.533745, 0.7781271, 1.000000 }
            };


            double[,] actual = Tools.Correlation(matrix);

            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.001));

        }




        [TestMethod()]
        public void ProportionsTest()
        {
            int[,] summary =
            {
                { 1, 4, 5 },
                { 2, 1, 3 },
            };

            double[,] probabilities =
            {
                { 1, 4.0/(4+5) },
                { 2, 1.0/(1+3) },
            };

            int[] positives = summary.GetColumn(1);
            int[] negatives = summary.GetColumn(2);
            double[] expected = probabilities.GetColumn(1);
            double[] actual;

            actual = Tools.Proportions(positives, negatives);
            Assert.IsTrue(Matrix.IsEqual(expected, actual, 0.000000001));
        }

        [TestMethod()]
        public void GroupTest()
        {

            int[][] data = {
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 2, 1 },
                 new int[] { 2, 0 },
                 new int[] { 2, 0 },
                 new int[] { 2, 0 },
            };

            int[][] expected = {
                 new int[] { 1, 4, 5 },
                 new int[] { 2, 1, 3 },
            };


            int[][] actual;
            actual = Tools.Group(data, 0, 1);

            Assert.IsTrue(Matrix.IsEqual(expected, actual));
        }

        [TestMethod()]
        public void ExtendTest()
        {
            int[,] summary =
            {
                { 1, 4, 5 },
                { 2, 1, 3 },
            };

            int[] group = summary.GetColumn(0);
            int[] positives = summary.GetColumn(1);
            int[] negatives = summary.GetColumn(2);

            int[][] expected = 
            {
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 1 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 1, 0 },
                 new int[] { 2, 1 },
                 new int[] { 2, 0 },
                 new int[] { 2, 0 },
                 new int[] { 2, 0 },
            };

            int[][] actual;
            actual = Tools.Expand(group, positives, negatives);
            Assert.IsTrue(Matrix.IsEqual(expected, actual));
        }

        [TestMethod()]
        public void ShuffleTest()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

            Tools.Shuffle(array);

            for (int i = 0; i < 10; i++)
                Assert.IsTrue(array.Contains(i));
        }


     
        [TestMethod()]
        public void ExtendTest2()
        {
            int[][] data = 
            {
                new int[] { 0, 2, 4 },
                new int[] { 1, 1, 2 },
            };

            int labelColumn = 0;
            int positiveColumn = 1;
            int negativeColumn = 2;

            int[][] expected = 
            {
                // For label 0
                new int[] { 0, 1 }, // Two positive cases
                new int[] { 0, 1 },

                new int[] { 0, 0 }, // Four negative cases
                new int[] { 0, 0 },
                new int[] { 0, 0 },
                new int[] { 0, 0 },
                
                // For label 1
                new int[] { 1, 1 }, // One positive case
                new int[] { 1, 0 }, // Three negative cases
                new int[] { 1, 0 },
            };

            int[][] actual = Tools.Expand(data, labelColumn, positiveColumn, negativeColumn);

            Assert.IsTrue(expected.IsEqual(actual));
        }

        [TestMethod()]
        public void KurtosisTest()
        {
            double[] values = { 3.4, 7.1, 1.5, 8.6, 4.9 };
            double expected = -1.939370186981427;
            double actual = Tools.Kurtosis(values);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SkewnessTest()
        {
            double[] values = { 3.4, 7.1, 1.5, 8.6, 4.9 };
            double expected = -0.0061643641512110154;
            double actual = Tools.Skewness(values);
            Assert.AreEqual(expected, actual);
        }



        [TestMethod()]
        public void WhiteningTest()
        {
            double[,] value = 
            {
                { 0.4218,    0.6557,    0.6787,    0.6555 },
                { 0.9157,    0.0357,    0.7577,    0.1712 },
                { 0.7922,    0.8491,    0.7431,    0.7060 },
                { 0.9595,    0.9340,    0.3922,    0.0318 },
            };

            double[,] I = Matrix.Identity(3);
            double[,] T = null; // transformation matrix

            // Perform the whitening transform
            double[,] actual = Tools.Whitening(value, out T);

            // Check if covariance matrix has the identity form
            double[,] cov = Tools.Covariance(actual).Submatrix(0, 2, 0, 2);
            Assert.IsTrue(cov.IsEqual(I, 1e-10));

            // Check if we can transform the data
            double[,] result = value.Multiply(T);
            Assert.IsTrue(result.IsEqual(actual));
        }


        /// <summary>
        ///A test for PooledVariance
        ///</summary>
        [TestMethod()]
        public void PooledVarianceTest()
        {
            double[][] samples = 
            {
                new double[] { 31, 30, 29 },
                new double[] { 42, 41, 40, 39 },
                new double[] { 31, 28 },
                new double[] { 23, 22, 21, 19, 18 },
                new double[] { 21, 20, 19, 18,17 },
            };

            double expected = 2.7642857142857147;
            double actual = Tools.PooledVariance(samples);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PooledStandardDeviation
        ///</summary>
        [TestMethod()]
        public void PooledStandardDeviationTest()
        {
            double[][] samples = 
            {
                new double[] { 31, 30, 29 },
                new double[] { 42, 41, 40, 39 },
                new double[] { 31, 28 },
                new double[] { 23, 22, 21, 19, 18 },
                new double[] { 21, 20, 19, 18,17 },
            };

            double expected = System.Math.Sqrt(2.7642857142857147);
            double actual = Tools.PooledStandardDeviation(samples);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EntropyTest()
        {
            {
                int[] values = { 0, 1 };
                int classes = 2;

                double expected = 1;
                double actual = Tools.Entropy(values, classes);

                Assert.AreEqual(expected, actual);
            }
            {
                int[] values = { 0, 0 };
                int classes = 2;

                double expected = 0;
                double actual = Tools.Entropy(values, classes);

                Assert.AreEqual(expected, actual);
            }
            {
                int[] values = { 1, 1 };
                int classes = 2;

                double expected = 0;
                double actual = Tools.Entropy(values, classes);

                Assert.AreEqual(expected, actual);
            }

        }
    }
}

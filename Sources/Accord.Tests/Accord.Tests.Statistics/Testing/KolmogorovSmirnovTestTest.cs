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
    using System;
    using Accord.Statistics.Distributions.Univariate;
    using Accord.Statistics.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class KolmogorovSmirnovTestTest
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
        public void KolmogorovSmirnovTestConstructorTest()
        {
            // Test against a Uniform distribution
            // References: http://www.math.nsysu.edu.tw/~lomn/homepage/class/92/kstest/kolmogorov.pdf

            double[] sample = { 0.621, 0.503, 0.203, 0.477, 0.710, 0.581, 0.329, 0.480, 0.554, 0.382 };

            ContinuousUniformDistribution distribution = ContinuousUniformDistribution.Standard;
            var target = new KolmogorovSmirnovTest(sample, distribution);

            Assert.AreEqual(distribution, target.TheoreticalDistribution);
            Assert.AreEqual(Hypothesis.TwoTail, target.Hypothesis);

            Assert.AreEqual(0.29, target.Statistic, 1e-16);
            Assert.AreEqual(0.3067, target.PValue, 1e-4);
            Assert.IsFalse(Double.IsNaN(target.Statistic));
        }

        [TestMethod()]
        public void KolmogorovSmirnovTestConstructorTest2()
        {
            // Test against a Normal distribution

            double[] sample = { 0.621, 0.503, 0.203, 0.477, 0.710, 0.581, 0.329, 0.480, 0.554, 0.382 };

            NormalDistribution distribution = NormalDistribution.Standard;
            var target = new KolmogorovSmirnovTest(sample, distribution);

            Assert.AreEqual(distribution, target.TheoreticalDistribution);
            Assert.AreEqual(Hypothesis.TwoTail, target.Hypothesis);

            Assert.AreEqual(0.580432, target.Statistic, 1e-5);
            Assert.AreEqual(0.000999, target.PValue, 1e-5);
            Assert.IsFalse(Double.IsNaN(target.Statistic));
        }

        [TestMethod()]
        public void KolmogorovSmirnovTestConstructorTest3()
        {
            // Test if the sample's distribution is greater than a Normal distribution, 

            double[] sample = { 0.621, 0.503, 0.203, 0.477, 0.710, 0.581, 0.329, 0.480, 0.554, 0.382 };

            NormalDistribution distribution = NormalDistribution.Standard;
            var target = new KolmogorovSmirnovTest(sample, distribution, KolmogorovSmirnovTestHypothesis.SampleIsGreater);

            Assert.AreEqual(distribution, target.TheoreticalDistribution);
            Assert.AreEqual(Hypothesis.OneUpper, target.Hypothesis);

            Assert.AreEqual(0.238852, target.Statistic, 1e-5);
            Assert.AreEqual(0.275544, target.PValue, 1e-5);
            Assert.IsFalse(Double.IsNaN(target.Statistic));
        }

        [TestMethod()]
        public void KolmogorovSmirnovTestConstructorTest4()
        {
            // Test if the sample's distribution is smaller than a Normal distribution, 

            double[] sample = { 0.621, 0.503, 0.203, 0.477, 0.710, 0.581, 0.329, 0.480, 0.554, 0.382 };

            NormalDistribution distribution = NormalDistribution.Standard;
            var target = new KolmogorovSmirnovTest(sample, distribution, KolmogorovSmirnovTestHypothesis.SampleIsSmaller);

            Assert.AreEqual(distribution, target.TheoreticalDistribution);
            Assert.AreEqual(Hypothesis.OneLower, target.Hypothesis);

            Assert.AreEqual(0.580432, target.Statistic, 1e-5);
            Assert.AreEqual(0.000499, target.PValue, 1e-5);
            Assert.IsFalse(Double.IsNaN(target.Statistic));
        }


        [TestMethod()]
        public void EmpiricalDistributionTest()
        {
            double[] sample = { 1, 5, 3, 1, 5, 2, 1 };
            UnivariateContinuousDistribution distribution = NormalDistribution.Standard;

            var target = new KolmogorovSmirnovTest(sample, distribution);

            EmpiricalDistribution actual = target.EmpiricalDistribution;

            Assert.AreNotSame(sample, actual.Samples);

            Array.Sort(sample);

            for (int i = 0; i < sample.Length; i++)
                Assert.AreEqual(sample[i], actual.Samples[i]);
        }

        [TestMethod()]
        public void TheoreticalDistributionTest()
        {
            double[] sample = { 1, 5, 3, 1, 5, 2, 1 };
            UnivariateContinuousDistribution distribution = NormalDistribution.Standard;

            var target = new KolmogorovSmirnovTest(sample, distribution);

            UnivariateContinuousDistribution actual = target.TheoreticalDistribution;
            Assert.AreEqual(distribution, actual);
        }
    }
}
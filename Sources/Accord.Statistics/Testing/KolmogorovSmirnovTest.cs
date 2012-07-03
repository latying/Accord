﻿// Accord Statistics Library
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

namespace Accord.Statistics.Testing
{
    using System;
    using Accord.Math;
    using Accord.Statistics.Distributions.Univariate;

    /// <summary>
    ///   Hypothesis for the one-sample Kolmogorov-Smirnov test.
    /// </summary>
    /// 
    public enum KolmogorovSmirnovTestHypothesis
    {
        /// <summary>
        ///   Tests whether the sample's distribution is
        ///   different from the reference distribution.
        /// </summary>
        /// 
        SampleIsDifferent,

        /// <summary>
        ///   Tests whether the distribution of one sample is greater
        ///   than the reference distribution, in a statistical sense.
        /// </summary>
        /// 
        SampleIsGreater,

        /// <summary>
        ///   Tests whether the distribution of one sample is smaller
        ///   than the reference distribution, in a statistical sense.
        /// </summary>
        /// 
        SampleIsSmaller,
    }

    /// <summary>
    ///   One-sample Kolmogorov-Smirnov (KS) test.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The Kolmogorov-Smirnov test tries to determine if a sample differs significantly
    ///   from an hypothesized theoretical probability distribution. The Kolmogorov-Smirnov
    ///   test has an interesting advantage in which it does not requires any assumptions
    ///   about the data. The distribution of the K-S test statistic does not depend on
    ///   which distribution is being tested.</para>
    /// <para>
    ///   The K-S test has also the advantage of being an exact test (other tests, such as the
    ///   chi-square goodness-of-fit test depends on an adequate sample size). One disadvantage
    ///   is that it requires a fully defined distribution which should not have been estimated
    ///   from the data. If the parameters of the theoretical distribution have been estimated
    ///   from the data, the critical region of the K-S test will be no longer valid.</para>
    /// <para>
    ///   This class uses an efficient and high-accuracy algorithm based on work by Richard
    ///   Simard (2010). Please see <see cref="KolmogorovSmirnovDistribution"/> for more details.</para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Kolmogorov%E2%80%93Smirnov_test">
    ///       Wikipedia, The Free Encyclopedia. Kolmogorov-Smirnov Test. 
    ///       Available on: http://en.wikipedia.org/wiki/Kolmogorov%E2%80%93Smirnov_test </a></description></item>
    ///     <item><description><a href="http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm">
    ///       NIST/SEMATECH e-Handbook of Statistical Methods. Kolmogorov-Smirnov Goodness-of-Fit Test.
    ///       Available on: http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm </a></description></item>
    ///     <item><description><a href="http://www.iro.umontreal.ca/~lecuyer/myftp/papers/ksdist.pdf">
    ///       Richard Simard, Pierre L’Ecuyer. Computing the Two-Sided Kolmogorov-Smirnov Distribution.
    ///       Journal of Statistical Software. Volume VV, Issue II. Available on:
    ///       http://www.iro.umontreal.ca/~lecuyer/myftp/papers/ksdist.pdf </a></description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    [Serializable]
    public class KolmogorovSmirnovTest : HypothesisTest, IHypothesisTest<KolmogorovSmirnovDistribution>
    {


        /// <summary>
        ///   Gets the theoretical, hypothesized distribution for the samples,
        ///   which should have been stated <i>before</i> any measurements.
        /// </summary>
        /// 
        public UnivariateContinuousDistribution TheoreticalDistribution { get; private set; }

        /// <summary>
        ///   Gets the empirical distribution measured from the sample.
        /// </summary>
        /// 
        public EmpiricalDistribution EmpiricalDistribution { get; private set; }

        /// <summary>
        ///   Gets the distribution for the test statistic.
        /// </summary>
        /// 
        public KolmogorovSmirnovDistribution StatisticDistribution { get; private set; }

        /// <summary>
        ///   Creates a new One-Sample Kolmogorov test.
        /// </summary>
        /// 
        /// <param name="sample">The sample we would like to test as beloging to the <paramref name="hypothesizedDistribution"/>.</param>
        /// <param name="hypothesizedDistribution">A fully specified distribution (which must NOT have been estimated from the data).</param>
        /// 
        public KolmogorovSmirnovTest(double[] sample, UnivariateContinuousDistribution hypothesizedDistribution)
            : this(sample, hypothesizedDistribution, KolmogorovSmirnovTestHypothesis.SampleIsDifferent)
        {
        }

        /// <summary>
        ///   Creates a new One-Sample Kolmogorov test.
        /// </summary>
        /// 
        /// <param name="sample">The sample we would like to test as beloging to the <paramref name="hypothesizedDistribution"/>.</param>
        /// <param name="hypothesizedDistribution">A fully specified distribution (which must NOT have been estimated from the data).</param>
        /// <param name="hypothesis">
        ///   The kind of hypothesis to test. If a two-sided hypothesis will test if the sample belongs to the
        ///   distribution. A lower side hypothesis will test if the sample's distribution is smaller than the
        ///   theoretical distribution. A upper side hypothesis will test if the sample's distribution is greater
        ///   than the theoretical distribution.
        /// </param>
        /// 
        public KolmogorovSmirnovTest(double[] sample, UnivariateContinuousDistribution hypothesizedDistribution,
            KolmogorovSmirnovTestHypothesis hypothesis)
        {
            double N = sample.Length;

            // Create the test statistic distribution with given degrees of freedom
            StatisticDistribution = new KolmogorovSmirnovDistribution(sample.Length);


            // Create a copy of the samples to prevent altering the
            // constructor's original arguments in the sorting step 
            double[] Y = (double[])sample.Clone();
            double[] D = new double[sample.Length];

            // Sort sample
            Array.Sort(Y);

            // Create the thoretical and empirical distributions
            this.TheoreticalDistribution = hypothesizedDistribution;
            this.EmpiricalDistribution = new EmpiricalDistribution(Y, smoothing: 0);
            
            Func<double, double> F = TheoreticalDistribution.DistributionFunction;


            // Finally, compute the test statistic and perform actual testing.
            if (hypothesis == KolmogorovSmirnovTestHypothesis.SampleIsDifferent)
            {
                // Test if the sample's distribution is just significantly
                //   "different" than the given theoretical distribution.

                // This is a correction on the common formulation found in many places
                //  such as in Wikipedia. Please see the Engineering Statistics Handbook,
                //  section "1.3.5.16. Kolmogorov-Smirnov Goodness-of-Fit Test" for more
                //  details: http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm

                for (int i = 0; i < sample.Length; i++)
                    D[i] = Math.Max(Math.Abs(F(Y[i]) - i / N), Math.Abs((i + 1) / N - F(Y[i])));

                Statistic = D.Max(); // This is the two-sided "Dn" statistic.
                PValue = StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.TwoTail;
            }
            else if (hypothesis == KolmogorovSmirnovTestHypothesis.SampleIsGreater)
            {
                // Test if the sample's distribution is "larger" than the
                // given theoretical distribution, in a statistical sense.

                for (int i = 0; i < sample.Length; i++)
                    D[i] = Math.Max(i / N - F(Y[i]), (i + 1) / N - F(Y[i]));

                Statistic = D.Max(); // This is the one-sided "Dn+" statistic.
                PValue = StatisticDistribution.OneSideDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneUpper;
            }
            else
            {
                // Test if the sample's distribution is "smaller" than the
                // given theoretical distribution, in a statistical sense.

                for (int i = 0; i < sample.Length; i++)
                    D[i] = Math.Max(F(Y[i]) - i / N, F(Y[i]) - (i + 1) / N);

                Statistic = D.Max(); // This is the one-sided "Dn-" statistic.
                PValue = StatisticDistribution.OneSideDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneLower;
            }
        }

    }
}
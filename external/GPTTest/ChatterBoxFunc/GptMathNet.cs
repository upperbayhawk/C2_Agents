// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;




namespace ChatterBox
{
    public class GptMathNet
    {


//        public static string compute_regression(double[] x, double[] y)
        public static string compute_regression(string s)
        {

            double[] xdata = new double[] { 1, 2, 3, 4, 5 };
            double[] ydata = new double[] { 2, 3, 4, 5, 6 };

            (double intercept, double slope) p = Fit.Line(xdata, ydata);
            double a = p.Item1; // intercept
            double b = p.Item2; // slope

            return (b.ToString());
        }

    }
}

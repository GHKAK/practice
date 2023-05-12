using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MultithreadTask {
    internal static class PayloadMethod {
        internal static double CalcFactorial() {
            BigInteger k = 1;
            double c = 0;

            for (var i = 2; i < 1000; i++) {
                k = i;
                var rand = new Random(i);
                var a = Math.Sin((double)k);
                var b = Math.Pow(Math.Sqrt(Math.Abs(a*i)), 3) * rand.NextDouble();
                c += b;
            }

            c += (double)k;

            return c;
        }
    }
}
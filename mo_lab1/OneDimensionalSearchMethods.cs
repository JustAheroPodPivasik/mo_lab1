using System;
using static System.Math;

namespace mo_lab1 {
    public static class OneDimensionalSearchMethods {
        private const double A = -2, B = 20;

        private static double CalculateFunction(double x) {
            return (x - 7) * (x - 7);
        }

        public static void Dichotomy(double eps) {
            var a = A;
            var b = B;
            var iter = 0;
            var delta = eps / 2;
            while (Math.Abs(b - a) > eps) {
                iter++;
                var x1 = (a + b - delta) / 2;
                var x2 = (a + b + delta) / 2;
                if (CalculateFunction(x1) < CalculateFunction(x2)) {
                    b = x2;
                }
                else {
                    a = x1;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dichotomy:");
            Console.ResetColor();
            Console.WriteLine("iter: {0}", iter);
            Console.WriteLine("x = {0}, f(x) = {1}\n", (a + b) / 2, CalculateFunction((a + b) / 2));
        }

        public static void GoldenSectionSearch(double eps) {
            var a = A;
            var b = B;
            var iter = 0;
            var phi1 = ((3 - Sqrt(5.0)) / 2);
            var phi2 = ((Sqrt(5.0) - 1) / 2);
            var x1 = a + (b - a) * phi1;
            var x2 = a + (b - a) * phi2;
            var f1 = CalculateFunction(x1);
            var f2 = CalculateFunction(x2);
            iter += 2;
            while (Math.Abs(a - b) > eps) {
                
                if (f1 > f2) {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + phi2 * (b - a);
                    f2 = CalculateFunction(x2);
                }
                else {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = a + phi1 * (b - a);
                    f1 = CalculateFunction(x1);
                }

                iter++;
            }

            

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GoldenSectionSearch:");
            Console.ResetColor();
            Console.WriteLine("iter: {0}", iter);
            Console.WriteLine("x = {0}, f(x) = {1}\n", (a + b) / 2, CalculateFunction((a + b) / 2));
        }

        public static void SearchInterval(double x0, double delta) {
            double[] x = new double[3];
            x[0] = x0;
            double h;
            if (CalculateFunction(x[0]) > CalculateFunction(x[0] + delta)) {
                x[1] = x[0] + delta;
                h = delta;
            }
            else {
                x[1] = x[0] - delta;
                h = -delta;
            }

            h *= 2;
            x[2] = x[1] + h;

            while (CalculateFunction(x[1]) > CalculateFunction(x[2])) {
                x[0] = x[1];
                x[1] = x[2];
                h *= 2;
                x[2] = x[1] + h;
            }

            if (x[0] > x[2]) {
                (x[0], x[2]) = (x[2], x[0]);
            }

            Console.WriteLine("from {0} to {1}", x[0], x[2]);
        }

        private static int Binet(int n) {
            var phi1 = (1 + Sqrt(5)) / 2;
            var phi2 = (1 - Sqrt(5)) / 2;
            return (int) ((Pow(phi1, n) - Pow(phi2, n)) / (Sqrt(5)));
        }

        public static void Fibonacci(double eps) {
            var a = A;
            var b = B;

            var n = 1;
            while ((b - a) / eps > Binet(n + 2)) {
                n++;
            }

            double fib2 = Binet(n + 2);
            var iter = 0;

            var fib = new int[n + 1];
            for (var i = 1; i <= n; i++) {
                fib[i] = Binet(i);
            }

            var len = b - a;
            var x1 = a + len * fib[n] / fib2;
            var x2 = a + b - x1;
            var f1 = CalculateFunction(x1);
            var f2 = CalculateFunction(x2);
            iter += 2;
            for(int i = 2; i < n+2; i++) {
                if (f1 > f2) {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + (fib[n - i + 2] / fib2) * len; 
                    f2 = CalculateFunction(x2);
                } else {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
		
                    x1 = a + (fib[n - i + 1] / fib2) * len;
                    f1 = CalculateFunction(x1);
                }

                iter++;
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fibonacci:");
            Console.ResetColor();
            Console.WriteLine("iter: {0}", iter);
            Console.WriteLine("x = {0}, f(x) = {1}\n", (x1 + x2) / 2, CalculateFunction((x1 + x2) / 2));
        }
    }
}
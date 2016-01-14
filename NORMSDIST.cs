using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalCDF
{
    class Program
    {
        public static double NORMSDIST(double a)
        {
            //近似计算正态分布
            double p = 0.2316419;
            double b1 = 0.31938153;
            double b2 = -0.356563782;
            double b3 = 1.781477937;
            double b4 = -1.821255978;
            double b5 = 1.330274429;
            double x = Math.Abs(a);
            double t = 1 / (1+p*x);
            double val =1- (1/(Math.Sqrt(2*Math.PI))  * Math.Exp(-1*Math.Pow(a,2)/2)) * (b1*t + b2 * Math.Pow(t,2) + b3*Math.Pow(t,3) + b4 * Math.Pow(t,4) + b5 * Math.Pow(t,5) );
            if( a < 0)
            {
                val =1 - val;
            }
            return val;
        }
        static void Main(string[] args)
        {
            double i = 2.9;
            Console.WriteLine("F({0}) = {1}",i,NORMSDIST(i));
            
            
            Console.ReadKey();
        }
    }
}

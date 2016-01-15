using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static double NORMDIST(double erf_x)
        {
            double[] a = new double[7];
            a[0] = 1;
            a[1] = 0.0705230784;
            a[2] = 0.0422820123;
            a[3] = 0.0092705272;
            a[4] = 0.0001520143;
            a[5] = 0.0002765672;
            a[6] = 0.0000430638;
            erf_x = erf_x / Math.Sqrt(2);
            double multi = 0;
            for (int i = 0; i < 7; i++)
            {
                multi += a[i] * Math.Pow(erf_x, i);
            }
            double erf = 1 - 1 / (Math.Pow(multi, 16));
            return 0.5 + 0.5 * erf;
        }
        static double TDIST(double x, double v)
        {
            //maximum absolute error at v = 3 is 0.002501 observed at x = 0.9
            double g = (v - 1.5 - 0.1 / v + 0.5825 / Math.Pow(v, 2)) / (Math.Pow(v - 1, 2));
            double z = Math.Sqrt(Math.Log(1 + Math.Pow(x, 2) / v) / g);
            return NORMDIST(z);
        }
        static double TINV(double p, double v,int MaxLoop)
        {
            //这种方法估计的t分位数精度较低，之后会再作改进。
            double Value_approx = Math.Pow((Math.Pow(4*v-1,2)/(-25.5*v*v*Math.Log(4*p*(1-p))))-1/(2*v),-0.5);
            return Value_approx;
        }
        static void Main(string[] args)
        {
            double x = 1.3406,v = 25;
            double p = 0.05;
            Console.WriteLine("P(X<={0})={1}", x, TDIST(x, v));
            Console.WriteLine("T-score(p = {0},v = {1}):{2}", p,v, TINV(p, v,30));
            Console.ReadKey();
        }
    }
}

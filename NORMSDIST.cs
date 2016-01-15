using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalCDF
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] a = new double [7];
            a[0] = 1;
            a[1] = 0.0705230784;
            a[2] = 0.0422820123;
            a[3] = 0.0092705272;
            a[4] = 0.0001520143;
            a[5] = 0.0002765672;
            a[6] = 0.0000430638;
            double erf_x = 1.645;
            //erf_x为x的值
            erf_x = erf_x / Math.Sqrt(2);
            double multi = 0;
            for (int i = 0; i < 7; i++)
            {
                multi += a[i] * Math.Pow(erf_x, i);
            }
            double erf = 1 - 1 / (Math.Pow(multi, 16));
            double Result = 0.5 + 0.5 * erf;
            Console.WriteLine("Result = {0}",Result);
            //Result的值为该点处正态分布的累积密度值。
            //(maximum error: 3×10−7)
            Console.ReadKey();
        }
    }
}

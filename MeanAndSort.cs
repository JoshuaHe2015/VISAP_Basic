using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeanAndSort
{
    class Program
    {
        static BigNumber Mean(BigNumber[] NumberSeries)
        {
            BigNumber sum = new BigNumber("0");
            foreach (BigNumber SingleNumber in NumberSeries)
            {
                sum += SingleNumber;
            }
            int len = NumberSeries.Length;
            BigNumber len_bignumber = new BigNumber(len.ToString());
            return sum / len_bignumber;
        }
        static string round(string number, int digits)
        {
            int NumOriginLen = number.Length;
            char[] digit_dot = { '.' };
            string[] NumberBroken;
            NumberBroken = number.Split(digit_dot);
            if (digits < 0)
            {
                digits = 0;
            }
            if (NumberBroken[0].Length == NumOriginLen)
            {
                return NumberBroken[0] + ".".PadRight(digits + 1, '0');
            }
            else
            {
                string decimal_part = " ";
                BigNumber zero_point_one = new BigNumber("0.1");
                BigNumber one = new BigNumber("1");
                if (NumberBroken[1].Length > digits)
                {
                    if (Convert.ToInt32(NumberBroken[1].Substring(digits, 1)) > 4)
                    {
                        BigNumber carry = new BigNumber(digits.ToString());
                        carry = zero_point_one.Power(carry, 200);
                        BigNumber number_changed = new BigNumber(number);
                        number_changed = number_changed + carry;
                        return number_changed.ToString().Substring(0, NumOriginLen - (NumberBroken[1].Length - digits));
                    }
                    else
                    {
                        decimal_part = NumberBroken[1].Substring(0, digits);
                    }

                }
                else
                {
                    decimal_part = NumberBroken[1].PadRight(digits, '0');
                }
                return NumberBroken[0] + '.' + decimal_part;
            }
        }
        static void Sort(int n, BigNumber[] NumberSeries)
        {
            BigNumber temp = new BigNumber("0");
            if (n <= 1){
                return;
            }
            for (int i = 0;i < n - 1;i++){
                if (CompareNumber.Compare(NumberSeries[i] , NumberSeries[i+1]) == 1){
                    temp = NumberSeries[i +1];
                    NumberSeries[i + 1]= NumberSeries[i];
                    NumberSeries[i]=temp;
                }
                	Sort(n - 1, NumberSeries);
            }
        }
        static BigNumber Quantile(BigNumber[] NumberSeries, double quan)
        {
            int len = NumberSeries.Length;
            double position = quan * (double)len;
            position = Convert.ToDouble(round(position.ToString(), 0));
            return NumberSeries[Convert.ToInt32(position - 1)];
        }
        static void Main(string[] args)
        {
            
            Console.WriteLine("请输入你要输入数字个数：");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入一列数，用逗号分隔：");
            string number_series = Console.ReadLine();
            char[] separator = {','};
            string[] numbers = number_series.Split(separator);
            BigNumber[] x = new BigNumber[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = new BigNumber(numbers[i]);
            }
            Console.WriteLine(Mean(x).ToString());
            Sort(x.Length, x);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0}",x[i]);
            }
            Console.WriteLine("数列的中位数是：{0}", Quantile(x, 0.5).ToString());
            Console.ReadKey();
        }
    }
}

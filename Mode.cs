using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMode
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
        static string round(string number, int digits, int type)
        {
            //type为0时四舍五入，1为ground，2为ceiling
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
                if (digits <= 0)
                {
                    return number;
                }
                else
                {
                return NumberBroken[0] + ".".PadRight(digits + 1, '0');
                }
            }
            else
            {
                string decimal_part = " ";
                BigNumber zero_point_one = new BigNumber("0.1");
                BigNumber one = new BigNumber("1");
                if (NumberBroken[1].Length > digits)
                {
                    if (type == 1)
                    {
                        decimal_part = NumberBroken[1].Substring(0, digits);
                    }
                    else if (type == 2)
                    {
                        BigNumber carry = new BigNumber(digits.ToString());
                        carry = zero_point_one.Power(carry, 200);
                        BigNumber number_changed = new BigNumber(number);
                        number_changed = number_changed + carry;
                        if (digits <= 0)
                        {
                            return number_changed.ToString().Substring(0, NumberBroken[0].Length);
                        }
                        return number_changed.ToString().Substring(0, NumOriginLen - (NumberBroken[1].Length - digits));
                    }
                    else
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
                }
                else
                {
                    decimal_part = NumberBroken[1].PadRight(digits, '0');
                }
                if (decimal_part == "")
                {
                    return NumberBroken[0];
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
            double position = quan * (double)(len + 1);
            if ((position - Convert.ToDouble((int)position)) != 0)
            {
                int position_low = Convert.ToInt32(round(position.ToString(), 0, 1));
                BigNumber multi_1 = new BigNumber((position - Convert.ToDouble(position_low)).ToString());
                BigNumber multi_2 = new BigNumber((1 - (position - Convert.ToDouble(position_low))).ToString());
                BigNumber quan_num = new BigNumber("0");
                quan_num = multi_1 * NumberSeries[position_low - 1] + multi_2 * NumberSeries[position_low];
                return quan_num;
            }
            else
            {
                BigNumber quan_num = NumberSeries[(int)position - 1];
                return quan_num;
            }
        }
        static string Mode(BigNumber[] NumberSeries)
        {
            //仅限于寻找有序数列中的众数
            //多个众数时返回最小的众数
            double MaxCount = 0;
            double CurrentCount = 0;
            BigNumber MaxNumber = new BigNumber("0");
            BigNumber CurrentNumber = new BigNumber("0");
            int len = NumberSeries.Length;
            for (int i = 1; i < len; i++)
            {
                if (CompareNumber.Compare(NumberSeries[i - 1], NumberSeries[i]) == 0)
                {
                    CurrentCount++;
                    if (CurrentCount > MaxCount)
                    {
                        MaxCount = CurrentCount;
                        MaxNumber = NumberSeries[i];
                    }
                }
                else
                {
                    CurrentCount = 0;
                }
            }
            if (MaxCount == 0)
            {
                return "NA";
            }
            else
            {
                return MaxNumber.ToString();
            }
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
            Console.WriteLine("数列的众数是：{0}", Mode(x));
            Console.ReadKey();
        }
    }
}

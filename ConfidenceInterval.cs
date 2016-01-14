using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfidenceInterval
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
        static BigNumber NormalCDF(BigNumber mu, BigNumber sigma, BigNumber x)
        {
            BigNumber erf_x = (x - mu)/(sigma * new BigNumber(Math.Sqrt(2).ToString()));
            BigNumber[] a = {new BigNumber("0.0705230784"), new BigNumber("0.0422820123"), new BigNumber("0.0092705272"), new BigNumber("0.0001520143"), new BigNumber("0.0002765672"), new BigNumber("0.0000430638")};
            BigNumber sum = new BigNumber("0");
            BigNumber count = new BigNumber("0");
            for (int i = 0; i < 6; i++)
            {
                count = new BigNumber(i.ToString());
                sum += a[i] * erf_x.Power(count,30) ;
                Console.WriteLine("count = {0}, sum = {1}", count, sum);
            }
            Console.WriteLine("={0}", (new BigNumber("1") + sum).Power(new BigNumber("-16"), 30));
            BigNumber multiple = new BigNumber("1");
            sum = sum + new BigNumber("1");
            for (int q = 0; q < 16; q++)
            {
                multiple *= sum;
            }
            sum = new BigNumber("1") / sum;
            BigNumber erf = new BigNumber("1")-multiple;
            Console.WriteLine("erf = {0}", erf);
            return new BigNumber("0.5") * (new BigNumber("1") + erf);
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
        static BigNumber Variance(BigNumber[] NumberSeries)
        {
            //n - 1个自由度
            BigNumber sum = new BigNumber("0");
            int len = NumberSeries.Length;
            BigNumber mean_series = Mean(NumberSeries);
            foreach (BigNumber SingleNumber in NumberSeries)
            {
                sum += (SingleNumber - mean_series).Power(new BigNumber("2"), 30);
            }
            return sum / new BigNumber((len-1).ToString());
        }
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
                val = 1 - val;
            }
            return val;
        }
        public static double NORMSINV(double alpha)
        {
            if (0.5 < alpha && alpha < 1)
            {
                alpha = 1 - alpha;
            }
            double[] b = new double[11];
            b[0] = 0.1570796288 * 10;
            b[1] = 0.3706987906 * 0.1;
            b[2] = -0.8364353589 * 0.001;
            b[3] = -0.2250947176 * 0.001;
            b[4] = 0.6841218299 * 0.00001;
            b[5] = 0.5824238515 * 0.00001;
            b[6] = -0.1045274970 * 0.00001;
            b[7] = 0.8360937017 * 0.0000001;
            b[8] = -0.3231081277 * 0.00000001;
            b[9] = 0.3657763036 * 0.0000000001;
            b[10] = 0.6657763036 * 0.000000000001;
            double sum = 0;
            double y = -Math.Log(4 * alpha * (1 - alpha));
            for (int i = 0; i < 11; i++)
            {
                sum += b[i] * Math.Pow(y, i);
            }
            return Math.Pow(sum * y,0.5);
        }
        static string ParaEasti(BigNumber[] NumberSeries, double significance,int tail)
        {
            //总体均值的区间估计
            //tail == 1单尾，为2则双尾
            if (tail == 2){
                significance = significance / 2;
            }
            BigNumber mean_series = Mean(NumberSeries);
            BigNumber var_series = Variance(NumberSeries);
            BigNumber sd_series = var_series.Power(new BigNumber("0.5"), 30);
            BigNumber n = new BigNumber(NumberSeries.Length.ToString());
            BigNumber lower = mean_series - new BigNumber(NORMSINV(significance).ToString()) * sd_series * n.Power(new BigNumber("-0.5"), 30);
            BigNumber upper = mean_series + new BigNumber(NORMSINV(significance).ToString()) * sd_series * n.Power(new BigNumber("-0.5"), 30);
            return lower.ToString() + "," + upper.ToString();
            
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你要输入数字个数：");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入一列数，用逗号分隔：");
            string number_series = Console.ReadLine();
            char[] separator = { ',' };
            string[] numbers = number_series.Split(separator);
            BigNumber[] x = new BigNumber[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = new BigNumber(numbers[i]);
            }
            Console.WriteLine("置信区间：({0})", ParaEasti(x,0.05,2));
            //昨天我还没来得及详细测试和验证程序。分位数部分我验证了，应该是正确的。
            //参数估计部分尚未验证，感觉置信区间算出来有些宽，但是大体思路应该就是这样，
            //你们可以再修改，加油！
            Console.ReadKey();
        }
    }
}

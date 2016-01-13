using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoundForDecimal
{
    class Program
    {
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
        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("输入一个数字：");
                string number = Console.ReadLine();
                Console.WriteLine("要保留的位数：");
                int digits = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("{0}保留前{1}位小数的结果是{2}", number, digits, round(number, digits));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjustDigits
{
    class Program
    {
        static void Main(string[] args)
        {
            //目标为控制位数在10位
            string Number;
            Console.WriteLine("请输入数字");
            Number = Console.ReadLine();
            Number = Number.Trim();
            int CountDecimalPoint = 0;
            int ScientificNumber;
            string ScientificNotation = "";
            int digits_part;
            BigNumber NumberProcess = new BigNumber("0");
            string ProcessedNumber;
            char[] separator = { '.' };
            //用于分割小数点
            string[] NumberParts = new string[2];
            int IsNegative = 0;
            //用来判定数字是否为负
            int zero_count = 0;
            //用于计算小数点后有多少个零
            
            if (Number.Length > 10)
            {
                if (Number[0] == '-'){
                    IsNegative = 1;
                }
                foreach (char FindDecimalPoint in Number)
                {
                    if (FindDecimalPoint == '.')
                    {
                        CountDecimalPoint = 1;
                        break;
                    }
                }
                if (CountDecimalPoint == 0)
                {
                    //无小数点则只有整数部分
                        ScientificNumber = Number.Length - IsNegative - 1;
                        //转换为科学计数法
                        //Console.WriteLine("ScientificNumber = {0}", ScientificNumber);
                        if (ScientificNumber <= 9 && ScientificNumber > 0) 
                        {
                            ScientificNotation = "0" + ScientificNumber.ToString();
                            //如果科学计数法为个位要补零
                        }
                        else
                        {
                            ScientificNotation = ScientificNumber.ToString();
                        }
                            //数字正负无需考虑
                        if (ScientificNotation.Length + 1 + 2 <= 9)
                        {
                            
                            //例如：-3.1415E+05
                            //数字负号占一位，E和科学计数法的符号占两位
                            //大于9的情况不做考虑，数字太大。
                            digits_part = 9 - (ScientificNotation.Length + 1 + 2);
                            //小数位由此算出
                            NumberProcess = new BigNumber (Number.ToString()) / (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()),30);
                            //Console.WriteLine("ScientificNotation = {0},digits_part = {1},NumberProcess = {2}", ScientificNotation, digits_part, NumberProcess.ToString());
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), digits_part, 0) + "E+"+ScientificNotation ;
                            //Console.WriteLine("ProcessedNumber ={0}", ProcessedNumber);
                        }
                        else
                        {
                            //如果已经大于已经大于九位了，则不作处理
                            //这种情况极为罕见，不考虑
                            NumberProcess = new BigNumber(Number.ToString()) / (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()), 30);
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), 0, 0) + "E+" + ScientificNotation;
                        }
                    
                }
                else
                {
                    //有小数点的情况
                    NumberParts = Number.Split(separator);
                    if (NumberParts[0] != "0")
                    {
                        if (NumberParts[0].Length + 1 + 4 >= 9)
                        {
                         //如果保留四位小数之后依旧总长度超过九，则进行如下操作
                        //判断整数部分是否为0
                        //如果不为0，继续科学计数法的处理，舍去小数部分
                        ScientificNumber = NumberParts[0].Length - IsNegative - 1;
                        //转换为科学计数法
                        if (ScientificNumber <= 9 && ScientificNumber > 0)
                        {
                            ScientificNotation = "0" + ScientificNumber.ToString();
                            //如果科学计数法为个位要补零
                        }
                        else
                        {
                            ScientificNotation = ScientificNumber.ToString();
                        }
                        //数字正负无需考虑
                        if (ScientificNotation.Length + 1 + 2 <= 9)
                        {
                            //例如：-3.1415E+05
                            //数字负号占一位，E和科学计数法的符号占两位
                            //大于9的情况不做考虑，数字太大。
                            digits_part = 9 - (ScientificNotation.Length + 1 + 2);
                            //小数位由此算出
                            NumberProcess = new BigNumber(Number.ToString()) / (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()), 30);
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), digits_part, 0) + "E+" + ScientificNotation;
                        }
                        else
                        {
                            //如果已经大于已经大于九位了，则不作处理
                            //这种情况极为罕见，不考虑
                            NumberProcess = new BigNumber(Number.ToString()) / (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()), 30);
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), 0, 0) + "E+" + ScientificNotation;
                        }
                        }
                        else
                        {
                            ProcessedNumber = MathV.round(Number, 4, 0);
                        }
                    }
                    else
                    {
                        zero_count = 0;
                        foreach (char EachDigit in NumberParts[1])
                        {
                            if (EachDigit == '0')
                            {
                                zero_count++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        ScientificNumber = zero_count + 1;
                        //科学计数法的数字为零的个数加1
                        if (ScientificNumber <= 9 && ScientificNumber > 0)
                        {
                            ScientificNotation = "0" + ScientificNumber.ToString();
                            //如果科学计数法为个位要补零
                        }
                        else
                        {
                            ScientificNotation = ScientificNumber.ToString();
                        }
                        if (ScientificNotation.Length + 1 + 2 <= 9)
                        {
                            //例如：-2.654E-05
                            //数字负号占一位，E和科学计数法的符号占两位
                            //大于9的情况不做考虑，数字太大。
                            digits_part = 9 - (ScientificNotation.Length + 1 + 2);
                            //小数位由此算出
                            NumberProcess = new BigNumber(Number.ToString()) * (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()), 30);
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), digits_part, 0) + "E-" + ScientificNotation;
                        }
                        else
                        {
                            //如果已经大于已经大于九位了，则不作处理
                            //这种情况极为罕见，不考虑
                            NumberProcess = new BigNumber(Number.ToString()) * (new BigNumber("10")).Power(new BigNumber(ScientificNumber.ToString()), 30);
                            ProcessedNumber = MathV.round(NumberProcess.ToString(), 0, 0) + "E-" + ScientificNotation;
                        }

                    }
                }
            }
            else {
                ProcessedNumber = MathV.round(Number,4,0);
                //如果本身长度就在十个单位以内，则不作处理
                //保留四位小数
            }
            if (ProcessedNumber.Length < 10)
            {
                ProcessedNumber = ProcessedNumber.PadLeft(10, ' ');
            }
            Console.WriteLine("ProcessedNumber={0}", ProcessedNumber);
            Console.ReadKey();
        }
    }
}

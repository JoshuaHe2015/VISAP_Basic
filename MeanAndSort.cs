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
            BigNumber SignleNumber = new BigNumber("0");
            BigNumber sum = new BigNumber("0");
            foreach (BigNumber SingleNumber in NumberSeries)
            {
                sum += SingleNumber;
            }
            int len = NumberSeries.Length;
            BigNumber len_bignumber = new BigNumber(len.ToString());
            return sum / len_bignumber;
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
            //Console.WriteLine(Mean(x).ToString());
            Sort(x.Length, x);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0}",x[i]);
            }
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication12
{
    class Program
    {
        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("Write a sentence: ");
                string Str = Console.ReadLine();
                string KeyWord = "sqrt(";
                string OriginWord = "Math.Sqrt(";
                char[] separator = { ',' };
                string AllQuotation = "";
                string FinalSentence = "";
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i] == '"')
                    {
                        if (i != 0)
                        {
                            if (Str[i - 1] != '\\')
                            {
                                if (i + 1 != Str.Length)
                                {
                                    if (Str[i - 1] != '\'' || Str[i + 1] != '\'')
                                    {
                                        AllQuotation += "," + i.ToString();
                                    }
                                }
                                else
                                {
                                    AllQuotation += "," + i.ToString();
                                }

                            }
                        }
                        else
                        {
                            AllQuotation += "," + i.ToString();
                        }
                    }
                }
                if (AllQuotation.Trim() == "" || AllQuotation.Trim() == null)
                {
                    FinalSentence = Str.ToLower().Replace(KeyWord, OriginWord);;
                }
                else
                {
                    string[] Marks = AllQuotation.Substring(1, AllQuotation.Length - 1).Split(separator);
                    string[] EveryPart = new string[Marks.Length + 1];
                    int LastIndex = 0;
                    int Counts = 0;
                    
                    //按照规律，假设句子中引号成对出现，则每数到偶数引号，则前一段为引号内内容。
                    //每数到单数次引号，则截取前段，索引号为0则不截取
                    //最后加上最后一段（如果不为空的话）
                    foreach (string SingleIndex in Marks)
                    {
                        if (Convert.ToInt32(SingleIndex) >= 0)
                        {
                            Counts++;
                            if (Counts % 2 == 1)
                            {
                                if (Convert.ToInt32(SingleIndex) != 0)
                                {
                                    FinalSentence += Str.Substring(LastIndex, Convert.ToInt32(SingleIndex) - LastIndex).ToLower().Replace(KeyWord, OriginWord);
                                }
                            }
                            else
                            {
                                FinalSentence += Str.Substring(LastIndex - 1, Convert.ToInt32(SingleIndex) - LastIndex + 2);
                            }
                            LastIndex = Convert.ToInt32(SingleIndex) + 1;
                        }

                    }
                    if (Str.Length - LastIndex != 0)
                    {
                        FinalSentence += Str.Substring(LastIndex, Str.Length - LastIndex).ToLower().Replace(KeyWord, OriginWord);
                    }
                   
                }
 Console.WriteLine(FinalSentence);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace long_arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            Arithmetics tempNum = new Arithmetics();
            Arithmetics num1 = new Arithmetics("100");
            Arithmetics num2 = new Arithmetics("200");
            tempNum = num2 + num1;
            tempNum.Write();
            tempNum = num2 - num1;
            tempNum.Write();
            tempNum = num2 * num1;
            tempNum.Write();
            tempNum = num2 / num1;
            tempNum.Write();
            tempNum = num2 % num1;
            tempNum.Write();
            tempNum = Arithmetics.Pow(num1, 2);
            tempNum.Write();
            tempNum = Arithmetics.IntSqrt(num1);
            tempNum.Write();
            Console.WriteLine(num1 > num2);
            Console.WriteLine(num1 < num2);
            Console.WriteLine(num1 == num2);
            Console.WriteLine(num1 >= num2);
            Console.WriteLine(num1 <= num2);
            Console.WriteLine(num1 != num2);
            int a = 10;
            List<Arithmetics> list1 = new List<Arithmetics>();
            list1.Add(num1);
            list1.Add(num1);
            List<Arithmetics> list2 = new List<Arithmetics>();
            list1.Add(num2);
            list1.Add(num2);
            Arithmetics.inputModSys(ref a, ref list1, ref list2);
        }
        
        private class Arithmetics
        {
           private List<int> Num { get; set; }
           private const int Base = 10000;
           private int Sign { get; set; }
           private int _length;

           public Arithmetics(string str)
           {
               if (str[0] == '-')
               {
                   Sign = -1;
               }else
               {
                   Sign = 1;
               }
               Num = new List<int>();
               _length = 0;
               
               for (int i = str.Length; i >= 1; i-=4)
               {
                   if (i > 4)
                   {
                       string num4 = str.Substring(i-4, 4);
                        Num.Add(Int32.Parse(num4));
                        _length += 4;
                   }
                   else
                   {
                       string num4 = String.Empty;
                       if (Sign.Equals(-1))
                       {
                           _length += i - 1;
                            num4 = str[1..i];
                       }
                       else
                       {
                           _length += i;
                            num4 = str[0..i];
                       }
                       Num.Add(Int32.Parse(num4));
                       
                   }
               }
           }

           public Arithmetics(List<int> num, int sing)
           {
               Num = num;
               if (sing == -1 || sing == 1) Sign = sing;
           }

           public Arithmetics()
           {
               Sign = 1;
               Num = new List<int>();
           }

           public Arithmetics(Arithmetics a)
           {
                Sign = a.Sign;
                Num = a.Num;
           }

           private void setNum(int a, int i)
           {
               while (Num.Count <= i)
               {
                   Num.Add(0);
               }
               Num[i] = a;
           }

           public static List<int> NumLongList(int a)
           {

               List<int> numList = new List<int>();
               for (int i = 0; i < a; i++)
               {
                   numList.Add(0);
               }

               return numList;
           }

           public void Write()
           {
               string nout = Sign.Equals(-1) ? "-" : string.Empty;
               bool first = false;
                for (int i = Num.Count-1; i >= 0; i--)
                {
                  string numI = Num[i].ToString();
                  nout += first? numI.PadLeft(4, '0') : numI;
                  first = true;
                }
               Console.WriteLine(nout);
           }

           public void AddNum(Arithmetics b)
           {
               if (Sign * b.Sign == 1)
               {
                   Arithmetics plusNum = new Arithmetics(Num, Sign);
                   plusNum = Plus(plusNum, b);
                   Num = plusNum.Num;
                   Sign = plusNum.Sign;
               }
               else 
               {
                   Arithmetics minusNum = new Arithmetics(Num, Sign);
                   minusNum = Minus(minusNum, b);
                   Num = minusNum.Num;
                   Sign = minusNum.Sign;
                }
           }

           public void SubNum(Arithmetics b)
           {
               Arithmetics minusNum = new Arithmetics(Num, Sign);
               minusNum = Minus(minusNum, b);
               Num = minusNum.Num;
               Sign = minusNum.Sign;
            }

           private static Arithmetics Plus(Arithmetics a, Arithmetics b)
           {
                List<int> num = a.Num;
                List<int> num2 = b.Num;
                bool carry = false;
                for (int i = 0; i < Math.Max(num.Count, num2.Count) || carry; i++) 
                {
                    if (i.Equals(num.Count))
                    {
                        num.Add(0);
                    }

                    num[i] += carry ? 1 + (i < num2.Count ? num2[i] : 0) : 0 + (i < num2.Count ? num2[i] : 0);
                    carry = num[i] > Base ? true : false;
                    if (carry) num[i] -= Base;
                }
                Arithmetics ans = new Arithmetics(num, a.Sign);
                return ans;

           }

           private static Arithmetics Minus(Arithmetics a, Arithmetics b)
           {
                List<int> num = a.Num;
                List<int> num2 = b.Num;
                bool carry = false;
                if (num2.Count > num.Count)
                {
                    a.Sign = a.Sign * (-1);
                    List<int> temp = num;
                    num = num2;
                    num2 = temp;
                }

                for (int i = 0; i < num2.Count || carry; i++)
                {
                    num[i] -= carry ? 1 + (i < num2.Count ? num2[i] : 0) : 0 + (i < num2.Count ? num2[i] : 0);
                    carry = num[i] < 0 ? true : false;
                    if (carry) num[i] += Base;
                }

                num = DelPrimeNulls(num);
                Arithmetics ans = new Arithmetics(num, a.Sign);
                return ans;
           }

           public static List<int> DelPrimeNulls(List<int> num)
           {
               for (int i = num.Count - 1; num.Count > 1 && num[i].Equals(0); i--)
               {
                   num.RemoveAt(i);
               }

               return num;
           }

           public static Arithmetics MultShort(Arithmetics a, int b)
           {
               int carry = 0;
               List<int> num = a.Num;
               a.Sign = b > 0 ? a.Sign : a.Sign * (-1);
                b *= b > 0 ? 1 : -1;

                for (int i = 0; i < num.Count || carry != 0; i++)
                {
                   if (i == num.Count) num.Add(0);
                   long cur = carry  + num[i] * b;
                   num[i] = Convert.ToInt32(cur % Base);
                   carry = Convert.ToInt32(cur / Base);
                }

                num = DelPrimeNulls(num);
                Arithmetics ans = new Arithmetics(num, a.Sign);
                return ans;
           }

           public static Arithmetics MultLong(Arithmetics a, Arithmetics b)
           {
               List<int> num1 = a.Num;
               List<int> num2 = b.Num; 
               List<int> mult = NumLongList(num1.Count + b.Num.Count);
               for (int i = 0; i < num1.Count; i++)
               {
                   for (int j = 0, carry = 0; j < num2.Count ||carry != 0; j++)
                   {
                       long cur = mult[i + j] + num1[i] * (j < num2.Count ? num2[j] : 0) + carry;
                       mult[i + j] = Convert.ToInt32(cur % Base);
                        carry = Convert.ToInt32(cur / Base);
                   }
               }

               a.Sign = a.Sign * b.Sign;
               num1 = DelPrimeNulls(mult);
               Arithmetics ans = new Arithmetics(num1, a.Sign);
               return ans;
           }

           private static Arithmetics UpperRank(int a, int i)
           {
                Arithmetics ret = new Arithmetics();
                ret.setNum(a,i);
                DelPrimeNulls(ret.Num);
                return ret;
           }

           public static Arithmetics DivModLong(Arithmetics a, Arithmetics b, bool mode)
           {
               Arithmetics ans = new Arithmetics();
               Arithmetics curVal = new Arithmetics();
                int aS = a.Sign;
                int bS = b.Sign;
                a.Sign = 1;
                b.Sign = 1;
               for (int i = a.Num.Count - 1; i >= 0; i--)
               {

                   curVal += UpperRank(a.Num[i], i);
                    var x = 0;
                   var lLimit = 0;
                   var hLimit = Base;
                   while (lLimit <= hLimit)
                   {
                       int m = (lLimit + hLimit) >> 1; //div2
                       var approx = b * UpperRank(m,i);
                       if (approx <= curVal)
                       {
                           x = m;
                           lLimit = m + 1;
                       }
                       else
                       {
                           hLimit = m - 1;  
                       }
                   }
                   ans.setNum(x,i);
                   curVal -= b * UpperRank(x, i);
               }
               ans.Sign = a.Sign * b.Sign;
               DelPrimeNulls(ans.Num);
               if (mode) 
               {
                    int signAns = aS * bS;
                    return ans*signAns;}
               else
               if(aS == 1 && bS == 1 || aS == 1 && bS == -1)
               {
                    return curVal;
               }
               else
               {
                    return b - curVal;
               }
           }

           public static Arithmetics IntSqrt(Arithmetics a)
           {
             Arithmetics ans = new Arithmetics();
             Arithmetics lLimit = new Arithmetics();
             Arithmetics hLimit = a;
             Arithmetics d = new Arithmetics();
             Arithmetics c = new Arithmetics();
             c.setNum(2, 0);
                d.setNum(1, 0);
                while (lLimit <= hLimit)
                {
                    Arithmetics x = (hLimit + lLimit)/c;
                     if (x * x <= a)
                     { 
                         ans = x;
                         lLimit = x + d;
                     }
                     else
                     {
                         hLimit = x - d;
                     }
                     
                }
                return ans - d;
            }

            public static Arithmetics Pow(Arithmetics a, int i)
            {
                Arithmetics ans = new Arithmetics(a);
                for(; i > 1; i--)
                {
                    ans = ans * a;
                }
                return ans;
            }

            public static Arithmetics PlusMod(Arithmetics a, Arithmetics b, Arithmetics c)
            {
                a = Arithmetics.DivModLong(a, c, false);
                b = Arithmetics.DivModLong(b, c, false);
                a = a + b;
                return Arithmetics.DivModLong(a, c, false);
            }

            public static Arithmetics MinusMod(Arithmetics a, Arithmetics b, Arithmetics c)
            {
                a = Arithmetics.DivModLong(a, c, false);
                b = Arithmetics.DivModLong(b, c, false);
                a = a - b;
                return Arithmetics.DivModLong(a, c, false);
            }

            public static Arithmetics MultMod(Arithmetics a, Arithmetics b, Arithmetics c)
            {
                a = Arithmetics.DivModLong(a, c, false);
                b = Arithmetics.DivModLong(b, c, false);
                a = a * b;
                return Arithmetics.DivModLong(a, c, false);
            }

            public static Arithmetics PowMod(Arithmetics a, int b, Arithmetics c)
            {
                a = Arithmetics.DivModLong(a, c, false);
                a = Arithmetics.Pow(a, b);
                return Arithmetics.DivModLong(a, c, false);
            }

            public static void inputModSys(ref int x, ref List<Arithmetics> C,ref List<Arithmetics> M)
            {
                Console.WriteLine("Type the number of relations");
                int num = Convert.ToInt32(Console.ReadLine());
                x = num > 1 ? num : 0;
                Console.WriteLine();
                for(int i = 1; i <= x; i++)
                {
                    Console.WriteLine("type valuse of C" + i + " and M" + i);
                    C.Add(new Arithmetics(Console.ReadLine()));
                    M.Add(new Arithmetics(Console.ReadLine()));
                    Console.WriteLine();
                }
            }

            private static Arithmetics exeModSys( int x,  List<Arithmetics> C,  List<Arithmetics> M)
            {
                Arithmetics sum = new Arithmetics();
                Arithmetics D = new Arithmetics();
                List < Arithmetics > Mi= new List<Arithmetics>();
                List<Arithmetics> Im = new List<Arithmetics>();
                D.setNum(1, 0);
                foreach (var m in M)
                {
                    D = D * m;
                }
                for(int i = 0; i < x; i ++)
                {
                    Arithmetics temp = D / M[i];
                    Mi.Add(temp);
                    temp = Mi[i] % M[i];
                    Im.Add(temp);
                }
                for (int i = 0; i < x; i++)
                {
                    sum += C[i] * Mi[i] * Im[i];
                }
                sum = DivModLong(sum, D, false);
                return sum;

            }

            public static Arithmetics ModSys()
            {
                int X = 0;
                List<Arithmetics> C = new List<Arithmetics>();
                List<Arithmetics> M = new List<Arithmetics>();
                inputModSys(ref X, ref C, ref M);
                Arithmetics ans = exeModSys( X, C, M);
                return ans;
            }

            private static int Compare(Arithmetics a, Arithmetics b)
           {
               return CompareSign(a, b);
           }

           private static int CompareSign(Arithmetics a, Arithmetics b)
           {
               
                   if (a.Sign < b.Sign)
                   {
                       return -1;
                   }
                   else if (a.Sign > b.Sign)
                   {
                       return 1;
                   }
                   
               return CompareSize(a, b);
           }

           private static int CompareSize(Arithmetics a, Arithmetics b)
           {
               if (a.Num.Count < b.Num.Count)
               {
                   return -1;
               }
               else if (a.Num.Count > b.Num.Count)
               {
                   return 1;
               }

               return CompareDigits(a, b);
           }

           private static int CompareDigits(Arithmetics a, Arithmetics b)
           {
               for (var i = a.Num.Count-1; i >= 0; i--)
               {
                   if (a.Num[i] < b.Num[i])
                   {
                       return -1;
                   }
                   else if (a.Num[i] > b.Num[i])
                   {
                       return 1;
                   }
               }

               return 0;
           }

           public static bool operator <(Arithmetics a, Arithmetics b) => Compare(a, b) < 0;

           public static bool operator >(Arithmetics a, Arithmetics b) => Compare(a, b) > 0;

           public static bool operator <=(Arithmetics a, Arithmetics b) => Compare(a, b) <= 0;

           public static bool operator >=(Arithmetics a, Arithmetics b) => Compare(a, b) >= 0;

           public static bool operator ==(Arithmetics a, Arithmetics b) => Compare(a, b) == 0;

           public static bool operator !=(Arithmetics a, Arithmetics b) => Compare(a, b) != 0;

           public override bool Equals(object obj) => !(obj is Arithmetics) ? false : this == (Arithmetics)obj;

           public static Arithmetics operator -(Arithmetics a)
           {
                Arithmetics ret = new Arithmetics(a);
               ret.Sign = ret.Sign == 1 ? -1 : 1;
               return ret;
           }

           public static Arithmetics operator +(Arithmetics a, Arithmetics b) => a.Sign == b.Sign
               ? Plus(a, b)
               : Minus(a, b);

           public static Arithmetics operator -(Arithmetics a, Arithmetics b) => a + -b;

           public static Arithmetics operator *(Arithmetics a, Arithmetics b) => MultLong(a, b);

           public static Arithmetics operator *(Arithmetics a, int b) => MultShort(a, b);

           public static Arithmetics operator /(Arithmetics a, Arithmetics b) => DivModLong(a, b, true);

           public static Arithmetics operator %(Arithmetics a, Arithmetics b) => DivModLong(a, b, false);

        
        }
    }
}

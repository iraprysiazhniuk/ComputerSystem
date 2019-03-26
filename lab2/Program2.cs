using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть перше число: ");
            string num1 = Console.ReadLine();
            Console.WriteLine("Введіть друге число: ");
            string num2 = Console.ReadLine();
            division(num1, num2);
            Console.ReadKey();
        }
        public static void division(string num1, string num2)
        {
            Int64 divisor, remainderAndQuotient, product = 0;
            try
            {
                divisor = Int32.Parse(num1);
                remainderAndQuotient = Int32.Parse(num2);
            }
            catch
            {
                Console.WriteLine("Пропускаємо елементи, які не є int32.");
                return;
            }
            divisor <<= 32;

            bool setRemLSBToOne = false; //встановити найменший значущий біт false
            for (int i = 0; i <= 32; ++i)
            {
                Console.WriteLine("Step № " + (i + 1) + ":\n");

                Console.Write("Divisor is "); // дільник
                if (divisor <= remainderAndQuotient)
                {
                    remainderAndQuotient -= divisor;
                    setRemLSBToOne = true;
                    Console.Write("менше");
                }
                else
                    Console.Write("більше");

                Console.WriteLine(" ніж remainder.");
                Console.WriteLine("Зсуваємо remainder вліво на один біт.");

                remainderAndQuotient <<= 1;

                if (setRemLSBToOne)
                {
                    setRemLSBToOne = false;
                    remainderAndQuotient |= 1; //lsb - 1
                    Console.WriteLine("Встановлюємо remainder lsb to 1");//встановлення найменшого значущого біта
                }
                Console.WriteLine();

                Console.WriteLine("Divisor:\n" + FinishStringWithZeros(Convert.ToString(divisor, 2)) +
                    "\nRemainder and quotient:\n" + FinishStringWithZeros(Convert.ToString(remainderAndQuotient, 2)) + "\n");
            }
            long quotient = remainderAndQuotient & ((long)Math.Pow(2, 33) - 1);
            long remainder = remainderAndQuotient >> 33;
            Console.WriteLine("Quotient:\n" + FinishStringWithZeros(Convert.ToString(quotient, 2)) +
                " ( " + quotient + " )\n");

            Console.WriteLine("Remainder:\n" + FinishStringWithZeros(Convert.ToString(remainder, 2)) +
               " ( " + remainder + " )");
        }
        static string FinishStringWithZeros(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}

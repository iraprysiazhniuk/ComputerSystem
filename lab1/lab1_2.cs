using System;
using System.Collections.Generic;
using System.IO;

namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

            int fileLength = 0;
            using (StreamReader r = new StreamReader(@"C:\Users\Iryna\Desktop\3 курс\CS\CS_lab1\texts\1.txt.gz"))
            {
                string read = r.ReadToEnd();
                fileLength = read.Length;
            }

            Byte[] bytes;
            using (BinaryReader f1 = new BinaryReader(File.Open(@"C:\Users\Iryna\Desktop\3 курс\CS\CS_lab1\texts\1.txt.gz", FileMode.Open)))
            {
                bytes = f1.ReadBytes(fileLength);
            }
            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.WriteLine("************************************************");

            string output = "";
            int b;
            for (int i = 0; i < bytes.Length; i += 3)
            {
                b = (bytes[i] & 0xFC) >> 2;//множимо на 252, здвигаємо на 2 разряди, отримання першого 6-бітового байта
                output += alphabet[b];//кодування байта
                b = (bytes[i] & 0x03) << 4;//множимо на 3, відтягуємо вліво на 4 разряди, отримали 2 біти другого байта
                if (i + 1 < bytes.Length)
                {
                    b |= (bytes[i + 1] & 0xF0) >> 4;//отримали з другого 8-бітового байту 4 біти 6-бітового байту, потім додали з двома, отриманими вище
                    output += alphabet[b];//кодування байта
                    b = (bytes[i + 1] & 0x0F) << 2;//множимо на 15, відтягуємо вліво на 2 розряди, отримали 4 біти третього байта
                    if (i + 2 < bytes.Length)
                    {
                        b |= (bytes[i + 2] & 0xC0) >> 6;//отримали з третього 8-бітового байта 2 біти 6-бітового байту, потім додали з чотирма, отриманими вище
                        output += alphabet[b];//кодування байта
                        b = bytes[i + 2] & 0x3F;//множимо на 63, отримуємо 6 біт четвертого 6-бітового байта
                        output += alphabet[b];//кодування байта
                    }
                    else//якщо в кінці залишається 2 байти, а не 3
                    {
                        output += alphabet[b];
                        output += '=';
                    }
                }
                else//якщо в кінці залишається 1 байт, а не 3
                {
                    output += alphabet[b];
                    output += "==";
                }
            }
            Console.WriteLine(output);
            using (StreamWriter wr = new StreamWriter(@"C:\Users\Iryna\Desktop\3 курс\CS\CS_lab1\texts\1Base64.txt", true))
            {
                wr.Write(Convert.ToBase64String(bytes));
            }
            Console.ReadKey();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            double entropy = 0; //ентропія
            double quantity_information = 0.0; //кількість інформації
            string path = @"C:\Users\Iryna\Desktop\3 курс\CS\CS_lab1\texts\1.txt"; //шлях до файлу
            string text = ""; //записується зміст файлу
            double length = 0.0; //довжина файлу

            //Console.OutputEncoding = Encoding.UTF8;

            using (StreamReader a = new StreamReader(path, Encoding.UTF8))
            {
                text = a.ReadToEnd();
                a.Close();
            }
            //виведення тексту
            //text = File.ReadAllText(path, Encoding.UTF8);
            //Console.WriteLine(text);

            length = text.Length;

            Dictionary<char, int> pair = new Dictionary<char, int>(); //колекція, key i value

            foreach( char symbol in text)//визначення частоти появи символу у тексті
            {
                if (pair.ContainsKey(symbol))
                {
                    pair[symbol] += 1;
                }
                else
                { 
                    pair.Add(symbol, 1);
                }
            }
            foreach (KeyValuePair<char, int> kvp in pair)
            {
                entropy -= (kvp.Value / length) * Math.Log(kvp.Value / length, 2);
                Console.WriteLine("Symbol: " + kvp.Key + " Frequency: " + kvp.Value + " Probability: " + kvp.Value / length);
            }

            quantity_information = entropy * length / 8;
            Console.WriteLine("Entropy bit: " + entropy + "\n" + "Info quantity byte: " + quantity_information);
            FileInfo file = new FileInfo(path);
            Console.WriteLine("File name: " + file.Name + " File size byte: " + file.Length);
            Console.ReadKey();
        }
    }
}

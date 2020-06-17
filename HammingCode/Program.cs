using System;
using System.IO;
using System.Collections.Generic;


namespace HammingCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowInfo();
            string s;
            string decoded, encoded = "";
            while(true)
            {
                s = Console.ReadLine();
                switch(s)
                {
                    case "0":  return;
                    case "1":
                        Console.WriteLine("Enter nums");
                        decoded = Console.ReadLine();
                        encoded = Encode(decoded);
                        Console.WriteLine(encoded);
                        Console.WriteLine("Result");
                        break;
                    case "2":
                        if (encoded == null || encoded.Length < 1)
                        {
                            Console.WriteLine("Use 1 first");
                            break;
                        }
                        decoded = Decode(encoded);
                        Console.WriteLine("Encoded");
                        Console.WriteLine(encoded);
                        Console.WriteLine(decoded);
                        Console.WriteLine("Decoded");
                        break;
                    case "3":
                        if (encoded == null || encoded.Length < 1)
                        {
                            Console.WriteLine("Use 1 first");
                            break;
                        }
                        Console.WriteLine();
                        Console.WriteLine(encoded);
                        Console.WriteLine("Enter position");
                        int n;
                        int.TryParse(Console.ReadLine(), out n);
                        if (n == default)
                        {
                            Console.WriteLine("Try again");
                            break;
                        }
                        string temp1 = encoded.Substring(0, n - 1);
                        string temp2 = encoded.Substring(n);
                        if (encoded[n] == '0') encoded = temp1 + "1" + temp2;
                        else encoded = temp1 + "0" + temp2;
                        Console.WriteLine("Result");
                        Console.WriteLine(encoded);
                        break;                       
                }
                ShowInfo();
            }
            
        }

        static void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Закодировать строку");
            Console.WriteLine("2. Декодировать строку (доступно после использования 1)");
            Console.WriteLine("3. Выбрать позицию для ошибки (доступно после использования 1)");
            Console.WriteLine("0. Выход");
            Console.WriteLine();
            Console.WriteLine();
        }

        static string Encode(string nums)
        {
            List<Bnum> n = new List<Bnum>();   //массив двоичных чисел
            for (int i = 0; i < nums.Length; i++)
            {
                //if (bittemp == i + 1)
                //{
                //    n.Add(new Bnum(0));
                //    bittemp *= 2;
                //    kbcount++;
                //    continue;
                //}
                n.Add(new Bnum(int.Parse(nums.Substring(i, 1))));
            }
            int bittemp = 1;
            int kbcount = 0;
            for (int i = 0; i < n.Count; i++)   //вставляем к.биты
            {
                if (i == bittemp - 1)
                {
                    n.Insert(i, new Bnum(0));
                    bittemp *= 2;
                    kbcount++;
                }
            }
            bittemp = 1;
            for (int i = 0; i < kbcount; i++)   //для каждого к.б.
            {
                Bnum sum = new Bnum(0);
                int step = bittemp;
                bool skip = false;
                for (int j = bittemp - 1; j < n.Count; j++)  // вычисляем значение
                {
                    if (step == 0)
                    {
                        step = bittemp;
                        skip = !skip;
                    }
                    if (skip)
                    {
                        step--;
                        continue;
                    }
                    else
                    {
                        sum = sum + n[j];
                        step--;
                    }
                }
                n[bittemp - 1] = sum;
                bittemp *= 2;
            }

            string temp = "";
            foreach(var i in n)
            {
                temp += i.ToString();
            }
            return temp;
        }

        static string Decode(string nums)
        {
            List<Bnum> n = new List<Bnum>();   
            for (int i = 0; i < nums.Length; i++)
            {
                n.Add(new Bnum(int.Parse(nums.Substring(i, 1))));
            }
            int kbcount = 0; int bittemp = 1;
            while (bittemp < n.Count) { bittemp *= 2; kbcount++; }

            bittemp = 1;
            List<int> ind = new List<int>(); // массив несовпадающих битов

            for (int i = 0; i < kbcount; i++)   
            {
                Bnum sum = new Bnum(0);
                int step = bittemp;
                bool skip = false;
                for (int j = bittemp - 1; j < n.Count; j++) 
                {
                    if (step == 0)
                    {
                        step = bittemp;
                        skip = !skip;
                    }
                    if (skip)
                    {
                        step--;
                        continue;
                    }
                    else
                    {
                        sum = sum + n[j];
                        step--;
                    }
                }
                if (0 != sum) ind.Add(bittemp);    //запоминаем несовпадающие биты
                bittemp *= 2;
            }
            if (ind.Count != 0)
            {
                int wrongbit = 0;
                foreach (var i in ind) wrongbit += i;
                wrongbit--;                 //превращаем в индекс
                if (n[wrongbit] == 0) n[wrongbit] = new Bnum(1);
                else n[wrongbit] = new Bnum(0);
            }
            bittemp = (int)Math.Pow(2, kbcount - 1);
            for (int i = 0; i < kbcount; i++)
            {
                n.RemoveAt(bittemp - 1);
                bittemp /= 2;
            }

            string temp = "";
            foreach (var i in n)
                temp += i.ToString();
            return temp;
        }
    }
}

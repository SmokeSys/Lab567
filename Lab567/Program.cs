using System;

namespace Lab567
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            ShowInfo();
            LZ77 lz = new LZ77();
            while (true)
            {
                s = Console.ReadLine();
                switch (s)
                {
                    case "0":
                        return;
                    case "1":
                        Console.WriteLine("Введите строку");
                        try
                        {
                            lz.Encode(Console.ReadLine());
                        }
                        catch { Console.WriteLine("Try again"); break; }
                        Console.WriteLine("Получившиеся метки:");
                        Console.WriteLine();
                        foreach (var i in lz.encodedWord)
                        {
                            Console.Write(i.ToString());
                        }
                        break;

                    case "2":
                        if (lz.encodedWord.Count == 0)
                        {
                            break;
                        }
                        Console.WriteLine("Text");
                        Console.WriteLine(lz.searchBuf);
                        
                        Console.WriteLine(lz.Decode());
                        Console.WriteLine("Decoded text");
                        break;

                    case "3":
                        if (lz.encodedWord.Count == 0)
                        {
                            break;
                        }
                        Console.WriteLine(lz.CompressionRatio());
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
            Console.WriteLine("3. Вычислить коэффициент сжатия (доступно после использования 1)");
            Console.WriteLine("0. Выход");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

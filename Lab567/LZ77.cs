using System;
using System.Collections.Generic;
using System.Text;

namespace Lab567
{
    class Mark
    {
        public int Offset;
        public int Length;
        public char NextChar;
        public Mark(int o, int l, char c)
        {
            Offset = o;
            Length = l;
            NextChar = c;
        }

        public override string ToString()
        {
            return String.Format("( " + Offset + ", " + Length + ", " + NextChar + " )");
        }
    }
    class LZ77
    {
        public List<Mark> encodedWord;
        public string searchBuf;
        private string actBuf;
        public LZ77()
        {
            encodedWord = new List<Mark>();
        }

        public List<Mark> Encode(string text)
        {
            actBuf = text.ToLower();
            searchBuf = actBuf[0].ToString();
            actBuf = actBuf.Substring(1);
            encodedWord.Add(new Mark(0, 0, searchBuf[0]));

            while (actBuf.Length != 0) 
            {
                char temp = actBuf[0];  

                Mark tempMark = new Mark(0, 0, '-');
                for (int i = searchBuf.Length - 1; i >= 0; i--) //смотрим поисковый буффер справа налево
                {
                    if (searchBuf[i] == temp)   //совпадение нашлось
                    {
                        int tempcount = 1;
                        int v = 1;

                        for (int j = i + 1; j < searchBuf.Length && v < actBuf.Length; j++, v++)  //вычисляем метку для этого случая
                        {
                            if (searchBuf[j] == actBuf[v])
                            {
                                tempcount++;
                            }
                            else break;
                        }

                        if (tempcount >= tempMark.Length)   //если случай превосходит предыдущий
                        {
                            tempMark.Offset = searchBuf.Length - i ;
                            tempMark.Length = tempcount;
                        }
                    }
                }  

                int ii;
                for (ii = 0; ii < tempMark.Length; ii++)    // перемещаем совпадающие символы в поисковый буффер
                {
                    searchBuf += actBuf[ii].ToString();
                }

                try
                {
                    tempMark.NextChar = actBuf[ii];
                    searchBuf += actBuf[ii].ToString();
                }
                catch { tempMark.NextChar = '-'; }
                encodedWord.Add(tempMark);  // запоминаем метку

                try
                {
                    actBuf = actBuf.Substring((ii == 0) ? 1 : ii + 1);  // готовимся к следующему шагу
                }
                catch { break; }
            }
            return encodedWord;
        }

        public double CompressionRatio()
        {
            int k1 = searchBuf.Length * 8;
            int k2 = 16 * encodedWord.Count;
            return (double)k1 / k2;
        }

        public string Decode()
        {
            string encword = "";

            while (encodedWord.Count != 0)
            {
                Mark temp = encodedWord[0];
                encodedWord.RemoveAt(0);

                if (temp.Offset == 0)
                {
                    encword += temp.NextChar.ToString();
                    continue;
                }
                int tind = encword.Length - temp.Offset + temp.Length; 
                for (int i = tind - temp.Length; i < tind; i++)
                {
                    encword += encword[i].ToString();
                }
                encword += temp.NextChar.ToString();
            }
            if (encword[encword.Length - 1] == '-')
                encword = encword.Substring(0, encword.Length - 1);
            return encword;
        }
    }
}

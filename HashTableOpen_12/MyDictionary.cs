using System;
using System.Collections.Generic;
using System.Text;

namespace HashTableOpen_12
{
    class MyDictionary
    {
        Cell[] table;
        int size = 0;
        public MyDictionary(int size)
        {
            table = new Cell[size];
            this.size = size;
        }
        public void Add(int value)
        {
            int j = 1;
            int key = FindHash(value);
            while (true)
            {
                while (key > size)
                {
                    key -= size;
                }

                try
                {
                    if (table[key] != null)
                        throw new Exception("Cell not null");

                    table[key] = new Cell(value);
                    break;
                }
                catch
                {
                   key += (int)Math.Pow(j, 2);
                    j++;
                }
            }
        }

        int FindHash(int key)
        {
            double value = 0.59756416 * key;
            var trunc = Math.Truncate(value);
            return (int)(size * (value - trunc));
        }

    }

    class Cell
    {
        int value;
        public Cell(int value)
        {
            this.value = value;
        }
    }
}

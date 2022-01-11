using System;
using System.Collections.Generic;
using System.Text;

namespace HashTableChain_11
{
    class MyDictionary
    {
        List<Cell>[] table;
        int size = 0;
        public MyDictionary(int size)
        {
            table = new List<Cell>[size];
            this.size = size;
        }
        public void Add(int value)
        {
            int key = FindHash(value);

            if (table[key] == null)
                table[key] = new List<Cell>();

            table[key].Add(new Cell(value));
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

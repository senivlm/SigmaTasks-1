using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public delegate int Comparer(object obj1, object obj2);
    public class Sorting
    {
        private Sorting() { }

        public static void Sort(object[] objects, Comparer comparer)
        {
            for (int i = 0; i < objects.Length - 1; ++i)
            {
                for (int j = 0; j < objects.Length - i - 1; ++j)
                {
                    if (comparer(objects[j], objects[j + 1]) > 0)
                    {
                        (objects[j], objects[j + 1]) = (objects[j + 1], objects[j]);
                    }
                }
            }
        }
        
    }
}

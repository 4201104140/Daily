using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.ProgrammingGuide
{
    public class FastTest
    {
        public static void Example()
        {
            int[] values = { 2, 4, 6, 8 };
            DoubleValues(values);
            foreach (var value in values)
                Console.Write("{0}  ", value);
        }

        public static void DoubleValues(int[] arr)
        {
            for (int ctr = 0; ctr <= arr.GetUpperBound(0); ctr++)
                arr[ctr] = arr[ctr] * 2;
        }
    }
}

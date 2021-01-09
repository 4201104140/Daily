using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Samples.System.ComponentModel
{
    public static class TypeConverter_Sample
    {

        public static void Example()
        {
            Color c = Color.Red;
            Console.WriteLine(TypeDescriptor.GetConverter(typeof(Color)).ConvertToString(c));
        }

        [TypeConverter()]
        public class MyClass
        {

        }
    }
}

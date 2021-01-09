using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples
{
    public class ParallelGrep
    {
        public static void Example()
        {
            string[] args = new string[] { "/s", "/i" };

            // Parse command-line switches
            bool recursive = args.Contains("/s");
            bool ignoreCase = args.Contains("/i");


        }


    }
}

using System;
using System.Reflection;

namespace Samples
{
    public static class Program
    {
        public static void Main(string[] args) => RunAll(args == null || args.Length == 0 ? null : args[0]);

        internal static void RunAll(string name = null)
        {
            int samples = 0;
            
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (name == null || name.Equals(type.Name))
                {
                    var sample = type.GetMethod("Example", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                    if (sample != null)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Sample in namespace: {type.Namespace}");
                        Console.WriteLine($"Class: {type.Name}");
                        Console.WriteLine($"Result:");
                        Console.ResetColor();
                        Console.WriteLine();
                        sample.Invoke(null, null);
                        samples++;
                    }
                }
            }
            Console.WriteLine(".............................................................");
            Console.WriteLine("Number of samples that ran without any exception: " + samples);
        }
    }
}

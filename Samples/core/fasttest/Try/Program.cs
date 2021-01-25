using System;
using System.Collections.Generic;
using System.Linq;

namespace Try
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            names.Add("Phan");
            names.Add("Tan");
            names.Add("Tai");

            names.ForEach(x =>
            {
                Console.WriteLine(x);
            });

            Dictionary<int, string> dics = new Dictionary<int, string>();
            dics.Add(1, "Phan");
            dics.Add(2, "Tan");
            dics.Add(3, "Tai");
            

            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
            Name test = new Name(AppDomain.CurrentDomain.FriendlyName);
            ShowValue show = test.DisplayName;

            Action show1 = () => test.DisplayName();
            show1();
            show();



        }
    }

    public delegate void ShowValue();

    public class Name
    {
        private string instanceName;

        public Name(string name)
        {
            instanceName = name;
        }

        public void DisplayName()
        {
            Console.WriteLine(instanceName);
        }
    }
}

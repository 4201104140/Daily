﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

//namespace Demo1
//{
//    class MyCompanyEventSource : EventSource
//    {
//        public static MyCompanyEventSource Log = new MyCompanyEventSource();

//        public void Startup() { WriteEvent(1); }
//        public void OpenFileStart(string fileName) { WriteEvent(2, fileName); }
//        public void OpenFileStop() { WriteEvent(3); }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string name = MyCompanyEventSource.GetName(typeof(MyCompanyEventSource));
//            IEnumerable<EventSource> eventSources = MyCompanyEventSource.GetSources();
//            MyCompanyEventSource.Log.Startup();

//            MyCompanyEventSource.Log.OpenFileStart("SomeFile");

//            MyCompanyEventSource.Log.OpenFileStop();
//        }
//    }
//}

namespace Demo2
{
    enum MyColor { Red, Yellow, Blue };

    [EventSource(Name = "MyCompany")]
    class MyCompanyEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Page = (EventKeywords)1;
            public const EventKeywords DataBase = (EventKeywords)2;
            public const EventKeywords Diagnostic = (EventKeywords)4;
            public const EventKeywords Perf = (EventKeywords)8;
        }

        public class Tasks
        {
            public const EventTask Page = (EventTask)1;
            public const EventTask DBQuery = (EventTask)2;
        }

        [Event(1, Message = "Application Failure: {0}", Level = EventLevel.Error, Keywords = Keywords.Diagnostic)]
        public void Failure(string message) { WriteEvent(1, message); }

        [Event(2, Message = "Starting up.", Keywords = Keywords.Perf, Level = EventLevel.Informational)]
        public void Startup() { WriteEvent(2); }

        [Event(3, Message = "loading page {1} activityID={0}", Opcode = EventOpcode.Start,
            Task = Tasks.Page, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        public void PageStart(int ID, string url) { if (IsEnabled()) WriteEvent(3, ID, url); }

        [Event(4, Opcode = EventOpcode.Stop, Task = Tasks.Page, Keywords = Keywords.Page, Level = EventLevel.Informational)]
        public void PageStop(int ID) { if (IsEnabled()) WriteEvent(4, ID); }

        [Event(5, Opcode = EventOpcode.Start, Task = Tasks.DBQuery, Keywords = Keywords.DataBase, Level = EventLevel.Informational)]
        public void DBQueryStart(string sqlQuery) { WriteEvent(5, sqlQuery); }

        [Event(6, Opcode = EventOpcode.Stop, Task = Tasks.DBQuery, Keywords = Keywords.DataBase, Level = EventLevel.Informational)]
        public void DBQueryStop() { WriteEvent(6); }

        [Event(7, Level = EventLevel.Verbose, Keywords = Keywords.DataBase)]
        public void Mark(int ID) { if (IsEnabled()) WriteEvent(7, ID); }

        [Event(8)]
        public void LogColor(MyColor color) { WriteEvent(8, (int)color); }

        public static MyCompanyEventSource Log = new MyCompanyEventSource();
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyCompanyEventSource.Log.Startup();
            Console.WriteLine("Starting up");

            MyCompanyEventSource.Log.DBQueryStart("Select * from MYTable");
            var url = "http://localhost";
            for (int i = 0; i < 10; i++)
            {
                MyCompanyEventSource.Log.PageStart(i, url);
                MyCompanyEventSource.Log.Mark(i);
                MyCompanyEventSource.Log.PageStop(i);
            }
            MyCompanyEventSource.Log.DBQueryStop();
            MyCompanyEventSource.Log.LogColor(MyColor.Blue);

            MyCompanyEventSource.Log.Failure("This is a failure 1");
            MyCompanyEventSource.Log.Failure("This is a failure 2");
            MyCompanyEventSource.Log.Failure("This is a failure 3");
        }
    }
}

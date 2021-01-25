using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string path = Directory.GetParent(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)).FullName;
        Task<string[]> task1 = Task<string[]>.Factory.StartNew(() => GetAllFiles(path));
        //Task task1 = Task.Factory.StartNew(() => { throw new IndexOutOfRangeException(); });
        try
        {
            await task1;
        }
        catch (UnauthorizedAccessException ae)
        {
            Console.WriteLine("Caught unauthorized access exception-await behavior" + ae.Message);
        }
        catch (AggregateException ae)
        {
            Console.WriteLine("Caught aggregate axception-Task.Wait behavior");
            ae.Handle((x) =>
            {
                if (x is UnauthorizedAccessException)
                {
                    Console.WriteLine("You do not have permission to access all folders in this path.");
                    Console.WriteLine("See your network administrator or try another path.");
                    return true;
                }
                return false;
            });
        }

        Console.WriteLine("task1 Status: {0}{1}", task1.IsCompleted ? "Completed" : "", task1.Status);

    }

    static string[] GetAllFiles(string str)
    {
        // Should throw an UnauthorizedAccessException exception.
        return System.IO.Directory.GetFiles(str, "*.txt", System.IO.SearchOption.AllDirectories);
    }
}

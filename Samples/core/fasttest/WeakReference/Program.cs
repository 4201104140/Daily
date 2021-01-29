using System;
using System.Collections.Generic;

// Create the cache.
int cacheSize = 50;
Random r = new Random();
Cache c = new Cache(cacheSize);

string DataName = "";
GC.Collect(0);

// Randomly access objects in the cache.
for (int i = 0; i < c.Count; i++)
{
    int index = r.Next(c.Count);

    // Access the object by getting a property value.
    DataName = c[index].Name;
}
// Show results.
double regenPercent = c.RegenerationCount / (double)c.Count;
Console.WriteLine("Cache size: {0}, Regenerated: {1:P2}%", c.Count, regenPercent);


public class Cache
{
    // Dictionary to contain the cache.
    static Dictionary<int, WeakReference> _cache;

    // Track the number of times an object is regenerated.
    int regenCount = 0;

    public Cache(int count)
    {
        _cache = new Dictionary<int, WeakReference>();

        // Add objects with a short weak reference to the cache.
        for (int i = 0; i < count; i++)
        {
            _cache.Add(i, new WeakReference(new Data(i), false));
        }
    }

    public int Count
    {
        get => _cache.Count;
    }

    public int RegenerationCount
    {
        get => regenCount;
    }

    // Retrieve a data object from the cache.
    public Data this[int index]
    {
        get
        {
            Data d = _cache[index].Target as Data;
            if (d == null)
            {
                // If the object was reclaimed, generate a new one.
                Console.WriteLine("Regenerate object at {0}: Yes", index);
                d = new Data(index);
                _cache[index].Target = d;
                regenCount++;
            }
            else
            {
                // Object was obtained with the weak reference.
                Console.WriteLine("Regenerate object at {0}: No", index);
            }

            return d;
        }
    }
}
public record Data
{
    private byte[] _data;
    private string _name;

    public Data(int size) => (_data, _name) = (new byte[size * 1024], size.ToString());
    //{
    //    _data = new byte[size * 1024];
    //    _name = size.ToString();
    //}

    public string Name
    {
        get => _name;
    }
} 

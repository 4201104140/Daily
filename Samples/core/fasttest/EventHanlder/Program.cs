using System;

class Counter
{
    public ThresholdReachedEventHandler ThresholdReached;

    public void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        ThresholdReachedEventHandler handler = ThresholdReached;
        handler?.Invoke(this, e);
    }
}

public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);

public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}

class Program
{
    static void Main()
    {
        Counter c = new Counter();

        for (int i = 0; i < 10; i++)
        {
            c.ThresholdReached += c_ThresholdReached;
        }

        c.OnThresholdReached(new ThresholdReachedEventArgs { Threshold = 0 });
    }

    static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
    {
        e.TimeReached = DateTime.Now;
        Console.WriteLine($"The threshold {e.Threshold++} was reached at {e.TimeReached}");
    }
}


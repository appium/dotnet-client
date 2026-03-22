using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        var pointerInputExtraAttributes = new Dictionary<string, object>();
        for (int i = 0; i < 100; i++)
        {
            pointerInputExtraAttributes[$"key{i}"] = $"value{i}";
        }

        int iterations = 100000;
        var sw = new Stopwatch();

        sw.Start();
        for (int i = 0; i < iterations; i++)
        {
            var toReturn = new Dictionary<string, object>();
            pointerInputExtraAttributes.ToList().ForEach(x => toReturn[x.Key] = x.Value);
        }
        sw.Stop();
        Console.WriteLine($"LINQ ToList().ForEach: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        for (int i = 0; i < iterations; i++)
        {
            var toReturn = new Dictionary<string, object>();
            foreach (var kvp in pointerInputExtraAttributes)
            {
                toReturn[kvp.Key] = kvp.Value;
            }
        }
        sw.Stop();
        Console.WriteLine($"Standard foreach: {sw.ElapsedMilliseconds} ms");
    }
}

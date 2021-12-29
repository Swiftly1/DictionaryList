using Benchmark;
using BenchmarkDotNet.Running;

namespace ListDictionary.Benchmark;

public class Program
{
    static void Main()
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
    }
}
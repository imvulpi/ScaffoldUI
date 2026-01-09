using BenchmarkDotNet.Running;

namespace ScaffoldUI.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
        }
    }
}

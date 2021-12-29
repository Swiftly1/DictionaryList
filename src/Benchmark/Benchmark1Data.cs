
namespace Benchmark
{
    internal static class Benchmark1Data1
    {
        public static List<(List<int> List, int Value)> GetData()
        {
            var lines = File.ReadAllLines("data1.txt");

            var list = new List<(List<int> List, int Value)>();
            for (int i = 0; i < lines.Length; i += 2)
            {
                var data = lines[i].Split(",").Select(x => Convert.ToInt32(x)).ToList();
                var val = Convert.ToInt32(lines[i + 1]);
                list.Add((data, val));
            }

            return list;
        }
    }
}
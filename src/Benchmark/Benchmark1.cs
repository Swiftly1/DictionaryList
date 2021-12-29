using BenchmarkDotNet.Attributes;
using DictionaryList;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class Benchmark1
    {
        private DictionaryList<int, int> dlist;
        private List<(List<int> List, int Value)> Lists = new List<(List<int> List, int Value)>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            dlist = new DictionaryList<int, int>();
            Lists = Benchmark1Data.GetData().ToList();
        }

        [Benchmark]
        public int Test()
        {
            foreach (var list in Lists)
                dlist.Add(list.List, list.Value);

            var sum = 0;
            for (int i = 0; i < Lists.Count; i += 100)
            {
                dlist.TryGet(Lists[i].List, out var val);
                sum += val;
            }

            return sum;
        }
    }
}

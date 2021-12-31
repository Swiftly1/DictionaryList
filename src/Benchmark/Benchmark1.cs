using BenchmarkDotNet.Attributes;
using DictionaryList;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class Benchmark1
    {
        private List<(List<int> List, int Value)> Lists1 = new List<(List<int> List, int Value)>();
        private List<(List<int> List, int Value)> Lists2 = new List<(List<int> List, int Value)>();

        private List<(ListKey<int> List, int Value)> OptimisticListKey = new List<(ListKey<int> List, int Value)>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            Lists1 = Benchmark1Data1.GetData().Take(100).ToList();
            Lists2 = Benchmark1Data2.GetData().ToList();

            foreach (var item in Lists1)
            {
                OptimisticListKey.Add((new ListKey<int>(item.List), item.Value));
            }
        }

        [Benchmark]
        public int Test1_DictionaryList()
        {
            var dlist = new DictionaryList<int, int>();

            foreach (var list in Lists1)
                dlist.Add(list.List, list.Value);

            var sum = 0;
            for (int i = 0; i < Lists1.Count; i += 100)
            {
                dlist.TryGet(Lists1[i].List, out var val);
                sum += val;
            }

            return sum;
        }

        //[Benchmark]
        //public int Test2_DictionaryList()
        //{
        //    var dlist = new DictionaryList<int, int>(false);

        //    foreach (var list in Lists2)
        //        dlist.Add(list.List, list.Value);

        //    var sum = 0;
        //    for (int i = 0; i < Lists2.Count; i += 100)
        //    {
        //        dlist.TryGet(Lists2[i].List, out var val);
        //        sum += val;
        //    }

        //    return sum;
        //}

        [Benchmark]
        public int Test1_OptimisticListKey()
        {
            var dict = new Dictionary<ListKey<int>, int>();

            foreach (var entry in OptimisticListKey)
                dict.Add(entry.List, entry.Value);

            var sum = 0;
            for (int i = 0; i < OptimisticListKey.Count; i += 100)
            {
                dict.TryGetValue(OptimisticListKey[i].List, out var val);
                sum += val;
            }

            return sum;
        }

        [Benchmark]
        public int Test1_PesimisticListKey()
        {
            var dict = new Dictionary<ListKey<int>, int>();
            var pesimistic = new List<(ListKey<int> List, int Value)>();

            foreach (var entry in Lists1)
                pesimistic.Add((new ListKey<int>(entry.List), entry.Value));

            foreach (var entry in pesimistic)
                dict.Add(entry.List, entry.Value);

            var sum = 0;

            for (int i = 0; i < pesimistic.Count; i += 100)
            {
                dict.TryGetValue(pesimistic[i].List, out var val);
                sum += val;
            }

            return sum;
        }
    }
}

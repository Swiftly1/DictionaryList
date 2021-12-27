using Xunit;
using DictionaryList;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;

namespace Tests
{
    public class TestSet1
    {
        [Fact]
        public void Test000()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 1, 3, 4 };
            var list2 = new List<int> { 1, 3 };

            dict.Add(list1, 15);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.False(dict.TryGet(list2, out var b));
        }

        [Fact]
        public void Test001()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 1, };
            var list2 = new List<int> { 1, 2 };
            var list3 = new List<int> { 1, 2, 3 };

            dict.Add(list1, 123);
            dict.Add(list2, 234);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.True(dict.TryGet(list2, out var b));

            Assert.False(dict.TryGet(list3, out _));

            Assert.Equal(123, a);
            Assert.Equal(234, b);
        }

        [Fact]
        public void Test002()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 1 };
            var list2 = new List<int> { 1 };

            dict.Add(list1, 5);

            Assert.Throws<ArgumentException>(() =>
            {
                dict.Add(list1, 6);
            });
        }

        [Fact]
        public void Test003()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var list2 = new List<int> { 1, 2, 3, 4, 5 };

            dict.Add(list1, 5);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(5, a);

            Assert.Throws<ArgumentException>(() =>
            {
                dict.Add(list1, 6);
            });

            Assert.True(dict.TryGet(list1, out a));
            Assert.Equal(5, a);

            dict.Add(list1, 5);

            Assert.True(dict.TryGet(list1, out a));
            Assert.Equal(5, a);
        }
    }
}
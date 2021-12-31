using Xunit;
using DictionaryList;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using Benchmark;

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

        [Fact]
        public void Test004()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 1, 2, 10 };
            var list2 = new List<int> { 1, 2, 3 };
            var list3 = new List<int> { 1, 2, 3, 3 };
            var list4 = new List<int> { 1, 2, 3, 3, 5 };
            var list5 = new List<int> { 1, 2, 3, 1 };

            dict.Add(list1, 30);
            dict.Add(list2, 50);
            dict.Add(list3, 70);
            dict.Add(list4, 80);
            dict.Add(list5, 100);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.True(dict.TryGet(list2, out var b));
            Assert.True(dict.TryGet(list3, out var c));
            Assert.True(dict.TryGet(list4, out var d));
            Assert.True(dict.TryGet(list5, out var e));

            Assert.Equal(30, a);
            Assert.Equal(50, b);
            Assert.Equal(70, c);
            Assert.Equal(80, d);
            Assert.Equal(100, e);
        }

        [Fact]
        public void Test005()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 0, 0, 0 };
            var list2 = new List<int> { 0, 0 };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test006()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 0, 0, };
            var list2 = new List<int> { 0, 0, 0 };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test007()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 0 };
            var list2 = new List<int> { 0, 0 };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test008()
        {
            var dict = new DictionaryList<int, int>();

            var list1 = new List<int> { 0, 0 };
            var list2 = new List<int> { 0 };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test009()
        {
            var dict = new DictionaryList<TestObject, int>();

            var list1 = new List<TestObject> { new TestObject(), new TestObject() };
            var list2 = new List<TestObject> { new TestObject() };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test010()
        {
            var dict = new DictionaryList<TestObject, int>();

            var list1 = new List<TestObject> { null, new TestObject() };
            var list2 = new List<TestObject> { new TestObject() };

            Assert.Throws<ArgumentException>(() =>
            {
                dict.Add(list1, 15);
                dict.Add(list2, 16);
            });

            Assert.False(dict.TryGet(list1, out var a));
            Assert.False(dict.TryGet(list2, out var b));
        }

        [Fact]
        public void Test011()
        {
            var dict = new DictionaryList<TestObject, int>();

            var list1 = new List<TestObject> { null, null };
            var list2 = new List<TestObject> { null };

            Assert.Throws<ArgumentException>(() =>
            {
                dict.Add(list1, 15);
                dict.Add(list2, 16);
            });

            Assert.False(dict.TryGet(list1, out var a));
            Assert.False(dict.TryGet(list2, out var b));
        }

        [Fact]
        public void Test013()
        {
            var dict = new DictionaryList<TestObject, int>();
            dict.AllowNULLsInKeys = true;

            var list1 = new List<TestObject> { null, new TestObject() };
            var list2 = new List<TestObject> { new TestObject() };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void Test014()
        {
            var dict = new DictionaryList<TestObject, int>();
            dict.AllowNULLsInKeys = true;

            var list1 = new List<TestObject> { null, null };
            var list2 = new List<TestObject> { null };

            dict.Add(list1, 15);
            dict.Add(list2, 16);

            Assert.True(dict.TryGet(list1, out var a));
            Assert.Equal(15, a);

            Assert.True(dict.TryGet(list2, out var b));
            Assert.Equal(16, b);
        }

        [Fact]
        public void TestListKey()
        {
            var dict = new Dictionary<ListKey<int>, int>();

            var list1 = new ListKey<int>( new List<int> { 1, 2, 10 });
            var list2 =  new ListKey<int>(new List<int> { 1, 2, 3 });
            var list3 =  new ListKey<int>(new List<int> { 1, 2, 3, 3 });
            var list4 =  new ListKey<int>(new List<int> { 1, 2, 3, 3, 5 });
            var list5 = new ListKey<int>(new List<int> { 1, 2, 3, 1 });

            dict.Add(list1, 30);
            dict.Add(list2, 50);
            dict.Add(list3, 70);
            dict.Add(list4, 80);
            dict.Add(list5, 100);

            Assert.True(dict.TryGetValue(list1, out var a));
            Assert.True(dict.TryGetValue(list2, out var b));
            Assert.True(dict.TryGetValue(list3, out var c));
            Assert.True(dict.TryGetValue(list4, out var d));
            Assert.True(dict.TryGetValue(list5, out var e));

            Assert.Equal(30, a);
            Assert.Equal(50, b);
            Assert.Equal(70, c);
            Assert.Equal(80, d);
            Assert.Equal(100, e);
        }
        public class TestObject
        {
            public Guid Id { get; set; } = new Guid();

            public override string ToString()
            {
                return Id.ToString();
            }
        }
    }
}
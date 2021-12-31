# DictionaryList
 
The purpose of this code is to implement something like Dictionary<T, U> where T is List<>

I've seen hashcode calculation based implementations, 
but they were too slow for me

and I decided to use tree-based approach, which's explained below.

# Installation

.NET CLI: `dotnet add package DictionaryList.Core --version 1.0.0`

# Example 1
	var dict = new DictionaryList<int, int>();

	var list1 = new List<int> { 1, 2, 3, 4, 5 };
	var list2 = new List<int> { 1, 2, 3, 4, 5, 5, 6 };

	dict.Add(list1, 5);
	dict.Add(list2, 10);

	if (dict.TryGet(list1, out var value1))
		Console.WriteLine(value1);

	dict.TryGet(list2, out var value2);
	Console.WriteLine(value2);
	
# Example 2
	var dict = new DictionaryList<int, int>();

	var list1 = new List<int> { 1, 2 };
	var list2 = new List<int> { 1, 2 };

	dict.Add(list1, 5);
	dict.Add(list2, 5); // It's fine since Key and Value are the same
	
	dict.Add(list2, 123); // Throws an ArgumentException since Value is different for the same key

# Configuration

DictionaryList's constructor accepts a boolean `allow_keys_with_nulls` that indicates whether you want to have NULLs inside keys (list).

So basically if you want to work with NULLs like this: `new List<User> { new User(), null, new User() }`, then you should use either:

	var dict = new DictionaryList<Test, int>(true);
or

	var dict = new DictionaryList<Test, int>();
	dict.AllowNULLsInKeys = true;
	
If you don't allow NULLs in keys and insert one with NULL then there'll be an exception thrown. 

# How does it work?

Basically it represents all those lists as a tree and stores pairs of list's elements and **NULL or if that was the last element, then the value** 

For example, for those lists, the tree will look like:

	Key: { 1, 2, 10 }; Value: 30
	Key: { 1, 2, 3 }; Value: 50
	Key: { 1, 2, 3, 3 }; Value: 70
	Key: { 1, 2, 3, 3, 5 }; Value: 80
	Key: { 1, 2, 3, 1 }; Value: 100

![obraz](https://user-images.githubusercontent.com/77643169/147508102-27fa17dd-baf7-49aa-8755-d997f37dfb5b.png)

# todo

Benchmarks

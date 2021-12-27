# DictionaryList
 
Purpose of this code to implement something like Dictionary<T, U> where T is List<>

I've seen hashcode based implementations, but they were too slow for me

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
	
	
	

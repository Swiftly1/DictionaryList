using DictionaryList;

var dict = new DictionaryList<int, int>();

var list1 = new List<int> { 1, 2, 3, 4, 5 };
var list2 = new List<int> { 1, 2, 3, 4, 5 };

dict.Add(list1, 5);
dict.Add(list2, 5);
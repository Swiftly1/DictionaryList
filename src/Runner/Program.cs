using DictionaryList;

var dict = new DictionaryList<Test, int>();
dict.AllowNULLsInKeys = true;

var list1 = new List<Test> { new Test() };
var list2 = new List<Test> { null };

dict.Add(list1, 5);
dict.Add(list2, 5);

public class Test
{

}
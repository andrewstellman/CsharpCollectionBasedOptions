# CsharpCollectionBasedOptions
Provides a C# Option type that extends `List<T>`.

Examples:
```
var v1 = Option.Some("xyz").Get; // sets v1 to "xyz"

try {
    var v2 = Option.None.Get; // throws an InvalidOperationException
} catch { }

Console.WriteLine(Option.Some("xyz")); // prints Some(xyz)
Console.WriteLine(Option.None); // prints None

var o1 = Option.Some(1) + Option.Some("a"); // sets o1 to Option.Some(1)
var o2 = Option.None + Option.Some("a"); // sets o2 to Option.Some("a")
var o3 = Option.None + Option.None; // sets o3 to Option.None

var l = new List<int>() { 1, 2, 3, 4, 5 };
var o4 = l.FirstOption(); // returns Option.Some(1)
var o5 = l.LastOption(); // returns Option.Some(5)
var o6 = new List<int>().FirstOption(); // returns Option.None
var o7 = l.FindOption(e => e > 3); // returns Option.Some(4)
var o8 = l.FindOption(e => e > 8); // returns Option.None

var d = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" }, { 3, "c" } };
var o9 = d.GetOrElse(2); // returns Option.Some("b")
var o10 = d.GetOrElse(9); // returns Option.None

var o11 = (d.GetOrElse(1) + d.GetOrElse(2)).GetOrElse("z"); // returns "a"
var o12 = (d.GetOrElse(3) + d.GetOrElse(4)).GetOrElse("z"); // returns "c"
var o13 = (d.GetOrElse(5) + d.GetOrElse(6)).GetOrElse("z"); // returns "z"
```

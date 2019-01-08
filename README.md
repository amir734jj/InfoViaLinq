# InfoViaLinq

This is a rewrite of my other project [GetPropertyInfoViaLinq](https://github.com/amir734jj/PropertyInfoViaLinq). The goal of re-write to enable getting both 1) `PropertyInfo` 2) `MethodInfo`all using Linq.

[Nuget link](https://www.nuget.org/packages/InfoViaLinq/)

Examples to get `PropertyInfo` via Linq:

```csharp
IInfoViaLinq<Person> _utility = InfoViaLinq<Person>.New();

// returns: "Parents.GreatParents.Parents.FatherName"
string.Join('.', _utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName).Members().Select(x => x.Name));

// returns custom attributes via linq
_utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName).Members().Last().GetAttribute<DisplayAttribute>();

// returns PropertyInfo of "FatherName" via linq
_utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName).Members().Last();
```

Examples to get `MethodInfo` via Linq:

```csharp
var str = InfoViaLinq<string>.New().FuncLambda<string>(x => x.ToString).GetMethodInfo().Name;
// "ToString"

var str = InfoViaLinq<string>.New().FuncLambda<int>(x => x.CompareTo).GetMethodInfo().Name;
// "CompareTo"
```

Notes:
- To get `MethodInfo` code supports void methods and methods with up-to three parameters.

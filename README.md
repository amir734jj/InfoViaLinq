# InfoViaLinq

This is a rewrite of my other project [GetPropertyInfoViaLinq](https://github.com/amir734jj/PropertyInfoViaLinq). The goal of re-write to enable getting both 1) `PropertyInfo` 2) `MethodInfo`all using Linq.

Examples to get `PropertyInfo` via Linq:

```csharp
IInfoViaLinq<Person> _utility = InfoViaLinq<Person>.New();

// returns: "Parents.GreatParents.Parents.FatherName"
_utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName).GetPropertyName();

// returns custom attributes via linq
_utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName).GetAttribute<DisplayAttribute>();

// returns PropertyInfo of "FatherName" via linq
_utility.PropLambda(x => x.Parents.GreatParents.Parents.FatherName);
```

Examples to get `MethodInfo` via Linq:

```csharp
var str = InfoViaLinq<string>.New().FuncLambda<string>(x => x.ToString).GetMethodInfo().Name;
// "ToString"

var str = InfoViaLinq<string>.New().FuncLambda<int>(x => x.CompareTo).GetMethodInfo().Name;
// "CompareTo"
```

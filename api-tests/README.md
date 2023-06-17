# Unit Tests

run `dotnet test .\api-tests.csproj` to excute the tests in the command line

produce a test resultfile `dotnet test  -- NUnit.TestOutputXml=yourfoldername` make sure to add this folder to gitingore

Since I don't really know ay 'should'-conventions my tests are inspired by [Structure and Interpretation of Test Cases - Kevlin Henney - GOTO 2022](https://www.youtube.com/watch?v=MWsk1h8pv2Q)

Short Summary:

- a test is more like a (sentence readable) statement of the domain.
- for better readability `use_underscore_to_write_your_methods` and not `PascalCase`.
- same applies for classes and namespaces. make use of them, as the framework allows you.

Exmaple from Kevlin Henney in the talk linked above (he shows how he makes use of the class name for DRY):

```csharp
namespace Leap_year_specifications;

public sealed class A_year_is_a_leap_year
{
    [Test]
    public void if_it_is_divisible_by_4_but_not_by_100(
        [Values(2020, 2016, 1984, 4)] int year) {}

    [Test]
    public void if_it_is_divisible_by_400(
        [Range(400, 4000, 400)] int year) {}
}

public sealed class A_year_is_not_a_leap_year
{
    [Test]
    public void if_it_is_not_divisible_by_4(
        [Values(2020, 2019, 1999, 1)] int year) {}

    [Test]
    public void if_it_is_divisible_by_100_but_not_by_400(
        [Values(2100, 1900, 100)] int year) {}
}

public sealed class A_year_is_supported
{
    [Test]
    public void if_it_is_positive([Values(1, int.MaxValue)] int year)
    {
        Assert.DoesNotThrow(() => IsLeapYear(year));
    }
}

public sealed class A_year_is_not_supported
{
    [Test]
    public void if_it_is_0()
    {
        Assert.Catch<ArgumentException>(() => IsLeapYear(0));
    }

    [Test]
    public void if_it_is_negative(
        [Values(-1, -4 -100, -400, int.MinValue)] int year)
    {
        Assert.Catch<ArgumentException>(() => IsLeapYear(year));
    }
}
```

>For tests to drive development they must do more than just test code performs its required functionality: they must clearly express that required functionality to the reader.
>
> -Nat Pryce & Steve Freeman *Are your tests really driving your development?*

---

> *Any fool can write code that a computer can understand. Good programmers write code that humans can understand*
>
> -Martin Fowler

Notes to get started:

- Simple Cases
- Common Cases
- Error Cases
- Boundary Cases

## Guidelines

- Create a folder per class you test.
  - Create a class per "Thing" you are testing. A "Thing" can be a method, or a behavior, anything really. Make sure express yourself.
- Prefer `[InlineData]` (when you can use constants and dont need reuseability) over `[MemberData]`.
- Prefer `[MemberData]` (when the test data is class scoped, or you need runtime initilization) over `[ClassData]`.
- Prefer `[ClassData]` when you need to reuse testdata across multiple classes.

The main reason for this unconventional structure is, to make use of tests running in parallel.
By Default (in xunit 2.x) tests are executed in parallel, per class, using the number of logical processor your device offers.
(There is currently no way of executing tests in parallel per method)

Exmaple:

- Folder: `EntityId`
  - `Constrcutor.cs`
  - `ToString.cs`

> If you need classes to run sequentially (for whatever reason) use the `[Collection("CollecName")]`-Attribute for the two classes and give them the same name.
>
> This is subject to change, because I trying out a lot of stuff. (Like swapping from nunit to xunit, ...)

---

> Note: With only `EntityId` and `IsoDateTime` tests the benchmarks performed on avrg. 6ms better after splitting up the classes using 8 logical processors.
>
> Means a total of 88 tests performed better in 10 classes (avrg. 42ms), compared to 3 classes (avrg. 47.833ms). (`IsoDateTime` was already split up in 2 classes)

I was thinking, "how much better would this perform on my laptop?", which runs on 16 logical processors. My conclusion was, that it wasnt much faster, because there were only 10 classes, meaning they untilize at max 10 out of 16 possible logical processors xD

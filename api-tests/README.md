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

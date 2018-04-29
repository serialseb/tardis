using System;
using Shouldly;
using Tardis.FluentTime;
using Xunit;

namespace Tests
{
  public class timespans
  {
    [Fact]
    public void milliseconds()
    {
      2.5.Milliseconds().ShouldBe(TimeSpan.FromMilliseconds(2.5));
      2.Milliseconds().ShouldBe(TimeSpan.FromMilliseconds(2));
    }

    [Fact]
    public void seconds()
    {
      2.5.Seconds().ShouldBe(TimeSpan.FromSeconds(2.5));
      2.Seconds().ShouldBe(TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void minutes()
    {
      2.5.Minutes().ShouldBe(TimeSpan.FromMinutes(2.5));
      2.Minutes().ShouldBe(TimeSpan.FromMinutes(2));
    }

    [Fact]
    public void hours()
    {
      2.5.Hours().ShouldBe(TimeSpan.FromHours(2.5));
      2.Hours().ShouldBe(TimeSpan.FromHours(2));
    }

    [Fact]
    public void days()
    {
      2.5.Days().ShouldBe(TimeSpan.FromDays(2.5));
      2.Days().ShouldBe(TimeSpan.FromDays(2));
    }
  }
}
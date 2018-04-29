using System;
using Shouldly;
using Tardis.FluentTime;
using Xunit;

namespace Tests
{
  public class datetimes
  {
    [Fact]
    public void date()
    {
      12.January(1900).ShouldBe(new DateTimeOffset(1900, 1, 12, 0, 0, 0, TimeSpan.Zero));
      12.February(1900).ShouldBe(new DateTimeOffset(1900, 2, 12, 0, 0, 0, TimeSpan.Zero));
      12.March(1900).ShouldBe(new DateTimeOffset(1900, 3, 12, 0, 0, 0, TimeSpan.Zero));
      12.April(1900).ShouldBe(new DateTimeOffset(1900, 4, 12, 0, 0, 0, TimeSpan.Zero));
      12.May(1900).ShouldBe(new DateTimeOffset(1900, 5, 12, 0, 0, 0, TimeSpan.Zero));
      12.June(1900).ShouldBe(new DateTimeOffset(1900, 6, 12, 0, 0, 0, TimeSpan.Zero));
      12.July(1900).ShouldBe(new DateTimeOffset(1900, 7, 12, 0, 0, 0, TimeSpan.Zero));
      12.August(1900).ShouldBe(new DateTimeOffset(1900, 8, 12, 0, 0, 0, TimeSpan.Zero));
      12.September(1900).ShouldBe(new DateTimeOffset(1900, 9, 12, 0, 0, 0, TimeSpan.Zero));
      12.October(1900).ShouldBe(new DateTimeOffset(1900, 10, 12, 0, 0, 0, TimeSpan.Zero));
      12.November(1900).ShouldBe(new DateTimeOffset(1900, 11, 12, 0, 0, 0, TimeSpan.Zero));
      12.December(1900).ShouldBe(new DateTimeOffset(1900, 12, 12, 0, 0, 0, TimeSpan.Zero));
    }

    [Fact]
    public void time()
    {
      1.January(1900).At("21:05").ShouldBe(new DateTimeOffset(1900, 1, 1, 21, 05, 0, 0, TimeSpan.Zero));
      1.January(1900).At("21:05:01").ShouldBe(new DateTimeOffset(1900, 1, 1, 21, 05, 1, 0, TimeSpan.Zero));
      1.January(1900).At("21:05:01.005").ShouldBe(new DateTimeOffset(1900, 1, 1, 21, 05, 1, 5, TimeSpan.Zero));
      1.January(1900).At("21:05:01.5").ShouldBe(new DateTimeOffset(1900, 1, 1, 21, 05, 1, 500, TimeSpan.Zero));
    }
  }
}
using System;
using System.Threading.Tasks;
using Shouldly;
using Tardis;
using Tardis.FluentTime;
using Xunit;

namespace Tests
{
  public class time
  {
    [Fact]
    public void freeze_time_to_date()
    {
      var clock = new Clock();
      clock.Freeze(23.November(1963));

      clock.UtcNow().ShouldBe(23.November(1963).At("00:00"));
    }

    [Fact]
    public async Task freeze_time_now()
    {
      var clock = new Clock();
      clock.Freeze();

      var now = clock.UtcNow();
      (now - DateTimeOffset.UtcNow).ShouldBeLessThan(10.Milliseconds());
      await Task.Delay(1.Seconds());
      clock.UtcNow().ShouldBe(now);
    }

    [Fact]
    public async Task move_forward()
    {
      var clock = new Clock();
      clock.Freeze(23.November(1963).At("12:00"));

      await clock.MoveForward(2.Hours());

      clock.UtcNow().ShouldBe(23.November(1963).At("14:00"));
    }
  }
}
using System;
using System.Threading.Tasks;
using Shouldly;
using Tardis;
using Tardis.FluentTime;
using Xunit;

namespace Tests
{
  public class stopwatch
  {
    [Fact]
    public void elapsed()
    {
      var clock = new Clock();

      clock.Freeze(23.November(1963));
      var sw = clock.Stopwatch();

      sw.elapsed().ShouldBe(TimeSpan.Zero);

      clock.MoveForward(2.Minutes());

      sw.elapsed().ShouldBe(TimeSpan.Zero);
    }

    [Fact]
    public async Task started()
    {
      var clock = new Clock();

      clock.Freeze(23.November(1963));
      var sw = clock.Stopwatch();
      sw.start();

      await Task.Delay(1.Seconds());

      sw.elapsed().ShouldBe(TimeSpan.Zero);

      await clock.MoveForward(2.Minutes());

      sw.elapsed().ShouldBe(2.Minutes());
    }

    [Fact]
    public async Task stopped()
    {
      var clock = new Clock();
      clock.Freeze(23.November(1963));
      var sw = clock.Stopwatch();
      sw.start();

      await clock.MoveForward(2.Minutes());

      sw.elapsed().ShouldBe(2.Minutes());

      sw.stop();

      await clock.MoveForward(5.Minutes());

      sw.elapsed().ShouldBe(2.Minutes());
    }

    [Fact]
    public async Task stopped_then_started()
    {
      var clock = new Clock();
      clock.Freeze(23.November(1963));
      var sw = clock.Stopwatch();
      sw.start();

      await clock.MoveForward(2.Minutes());

      sw.stop();

      await clock.MoveForward(5.Minutes());

      sw.start();

      await clock.MoveForward(10.Minutes());

      sw.elapsed().ShouldBe(12.Minutes());
    }
  }
}
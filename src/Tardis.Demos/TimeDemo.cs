using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tardis.Demos
{
  class TimeDemo
  {
    static async Task Main()
    {
      var clock = new Clock();
      clock.Freeze(DateTimeOffset.Parse("23 Nov 1963 +0000"));

      Console.WriteLine(clock.UtcNow());
      // prints 23/11/1963 00:00:00 +01:00

      await clock.MoveForward(TimeSpan.FromDays(1));
      
      Console.WriteLine(clock.UtcNow());
      // prints 24/11/1963 00:00:00 +01:00
    }

    class TimeConsumer
    {
      readonly Func<DateTimeOffset> _now = () => DateTimeOffset.UtcNow;

      public TimeConsumer(Func<DateTimeOffset> now = null)
      {
        if (now != null)
          _now = now;
      }
    }
  }
}
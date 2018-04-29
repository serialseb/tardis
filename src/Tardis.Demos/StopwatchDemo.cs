using System;
using System.Threading.Tasks;

namespace Tardis.Demos
{
  class StopwatchDemo
  {
    static async Task Main()
    {
      var clock = new Clock();
      clock.Freeze();

      var stopWatch = clock.Stopwatch();
      stopWatch.start();

      Console.WriteLine(stopWatch.elapsed());
      // Prints 00:00:00

      await clock.MoveForward(TimeSpan.FromMinutes(1));
      Console.WriteLine(stopWatch.elapsed());
      // Prints 00:01:00
    }

    class StopwatchConsumer
    {
      Func<(Func<TimeSpan> elapsed, Action start, Action stop)> _stopwatch = () =>
      {
        var sw = new System.Diagnostics.Stopwatch();
        return (() => sw.Elapsed, sw.Start, sw.Stop);
      };

      public StopwatchConsumer(Func<(Func<TimeSpan> elapsed, Action start, Action stop)> stopwatch = null)
      {
        if (stopwatch != null)
          _stopwatch = stopwatch;
      }
    }
  }
}
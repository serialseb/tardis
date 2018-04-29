using System;
using System.Diagnostics;

namespace Tardis
{
  public partial class Clock
  {
    class SystemClock
    {
      public static DateTimeOffset UtcNow()
      {
        return DateTimeOffset.UtcNow;
      }

      public static (Func<TimeSpan> elapsed, Action start, Action stop) Stopwatch()
      {
        var sw = new Stopwatch();
        return (() => sw.Elapsed, sw.Start, sw.Stop);
      }
    }
  }
}
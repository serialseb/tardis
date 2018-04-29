using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Tardis
{
  public partial class Clock
  {
    SimulatorClock _clock;

    public DateTimeOffset UtcNow()
    {
      return _clock?.UtcNow() ?? DateTimeOffset.UtcNow;
    }

    public (Func<TimeSpan> elapsed, Action start, Action stop) Stopwatch()
    {
      return _clock?.Stopwatch() ?? SystemClock.Stopwatch();
    }

    public Task Delay(TimeSpan delay)
    {
      return _clock?.Delay(delay) ??  Task.Delay(delay);
    }

    public void Freeze(DateTimeOffset? time = null)
    {
      EnsureSimulator();
      _clock.Freeze(time);
    }

    void EnsureSimulator()
    {
      _clock = _clock ?? new SimulatorClock();
    }

    public Task MoveForward(TimeSpan time)
    {
      EnsureSimulator();
      return _clock.MoveForward(time);
    }
  }
}
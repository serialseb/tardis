using System;

namespace Tardis
{
  public partial class Clock
  {
    class SimulatorStopwatch
    {
      SimulatorClock _clock;
      DateTimeOffset _startTime;
      TimeSpan? _elapsed;

      public SimulatorStopwatch(SimulatorClock clock)
      {
        _clock = clock;
        _elapsed = TimeSpan.Zero;
      }

      public void Start()
      {
        _startTime = _clock.UtcNow() - (_elapsed ?? TimeSpan.Zero);
        _elapsed = null;
      }

      public void Stop()
      {
        _elapsed = _clock.UtcNow() - _startTime;
      }

      public void Reset()
      {
        _startTime = _clock.UtcNow();
        _elapsed = null;
      }

      public TimeSpan Elapsed()
      {
        return _elapsed ?? _clock.UtcNow() - _startTime;
      }
    }
  }
}
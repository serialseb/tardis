using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tardis
{
  public partial class Clock
  {
    class SimulatorDelay
    {
      readonly TimeSpan _delay;
      public static AsyncLocal<DateTimeOffset?> ForwardDelay = new AsyncLocal<DateTimeOffset?>();

      public DateTimeOffset Expiration { get; }
      readonly TaskCompletionSource<int> _taskSource = new TaskCompletionSource<int>();

      public SimulatorDelay(SimulatorClock clock, TimeSpan delay)
      {
        _delay = delay;
        var now = clock.UtcNow();
        Expiration = now + delay;
      }

      public void Complete()
      {
        _taskSource.SetResult(1);
      }

      public Task Task => _taskSource.Task;
    }
  }
}
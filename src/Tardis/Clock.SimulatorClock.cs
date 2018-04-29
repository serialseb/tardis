using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Tardis
{
  public partial class Clock
  {
//    class SimulatorDelayCollection
//    {
//      SemaphoreSlim _lock = new SemaphoreSlim(1);
//      List<SimulatorDelay> _store = new List<SimulatorDelay>();
//      
//
//      public void Add(SimulatorDelay delay)
//      {
//        _lock.Wait();
//        _store.Add(delay);
//        _lock.Release();
//      }
//      
//      
//    }
    class SimulatorClock
    {
      DateTimeOffset _now;

      public SimulatorClock()
      {
        _now = DateTimeOffset.UtcNow;
      }

      public DateTimeOffset UtcNow()
      {
        return _now;
      }

      public (Func<TimeSpan> elapsed, Action start, Action stop) Stopwatch()
      {
        var sw = new SimulatorStopwatch(this);
        return (sw.Elapsed, sw.Start, sw.Stop);
      }

      public List<SimulatorDelay> PendingDelays { get; } = new List<SimulatorDelay>();
      TimeSpan _lastMoveForward;


      public Task Delay(TimeSpan delay)
      {
        var timeAtStartOfExectionContext = SimulatorDelay.ForwardDelay.Value;
        if (timeAtStartOfExectionContext == null)
          timeAtStartOfExectionContext = SimulatorDelay.ForwardDelay.Value = _now;
        
        SimulatorDelay.ForwardDelay.Value += delay;
        
        if (timeAtStartOfExectionContext + delay <= _now)
        {
          return Task.CompletedTask;
        }
        
        var simulatorDelay = new SimulatorDelay(this, delay);
        PendingDelays.Add(simulatorDelay);
        return simulatorDelay.Task;
      }

      public void Freeze(DateTimeOffset? time)
      {
        _now = time ?? DateTimeOffset.UtcNow;
      }

      public async Task MoveForward(TimeSpan time)
      {
        var endTime = _now + time;
        while (true)
        {
          var delayCopy = PendingDelays
            .ToList()
            .Where(d => d.Expiration <= endTime)
            .OrderBy(d => d.Expiration)
            .ToList();

          if (delayCopy.Any() == false) break;

          foreach (var task in delayCopy)
          {
            _now = task.Expiration;
            task.Complete();
            PendingDelays.Remove(task);
          }
        }

        _now = endTime;
      }
    }
  }
}
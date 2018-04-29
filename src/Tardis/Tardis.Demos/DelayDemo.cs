using System;
using System.Threading.Tasks;

namespace Tardis.Demos
{
  class DelayDemo
  {
    static async Task Main()
    {
      var clock = new Clock();
      clock.Freeze();

      var delay = clock.Delay(TimeSpan.FromMinutes(1));
      await clock.MoveForward(TimeSpan.FromMinutes(1));

      await delay;
      Console.WriteLine("Task was delayed by one minute, but returned immediately after the clock move forward");
    }

    class DelayConsumer
    {
      readonly Func<TimeSpan, Task> _delayer = Task.Delay;

      public DelayConsumer(Func<TimeSpan,Task> delayer = null)
      {
        if (delayer != null)
          _delayer = delayer;
      }
    }
  }
}
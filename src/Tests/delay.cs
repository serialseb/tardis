using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Tardis;
using Tardis.FluentTime;
using Xunit;

namespace Tests
{
  public class delay
  {
    Clock clock;
    public delay()
    {
      clock = new Clock();
      clock.Freeze();
    }
    [Fact]
    public async Task not_completed()
    {
      var simulatorDelay = clock.Delay(500.Milliseconds());
      
      await Task.Delay(1.Seconds());

      simulatorDelay.IsCompleted.ShouldBeFalse();
      simulatorDelay.Status.ShouldBe(TaskStatus.WaitingForActivation);
    }
    
    [Fact]
    public async Task completed()
    {
      var simulatorDelay = clock.Delay(500.Milliseconds());

      await clock.MoveForward(500.Milliseconds());
      
      simulatorDelay.IsCompleted.ShouldBeTrue();
      simulatorDelay.Status.ShouldBe(TaskStatus.RanToCompletion);
    }


    [Fact]
    public async Task chained_at_intervals()
    {
      bool firstWaitCalled = false, secondWaitCalled = false;
      var clock = new Clock();
      clock.Freeze();

      Task firstDelay = null, secondDelay = null;
      var task = Task.Run(async () =>
      {
        await (firstDelay = clock.Delay(5.Seconds()));
        firstWaitCalled = true;
          
        await (secondDelay = clock.Delay(5.Seconds()));
        secondWaitCalled = true;
      });

      WaitForTask(ref firstDelay);
      
      task.IsCompleted.ShouldBeFalse();
      task.Status.ShouldBe(TaskStatus.WaitingForActivation);

      await clock.MoveForward(5.Seconds());

      WaitForTask(ref secondDelay);
      
      task.IsCompleted.ShouldBeFalse();
      firstWaitCalled.ShouldBeTrue();
      secondWaitCalled.ShouldBeFalse();

      await clock.MoveForward(5.Seconds());
      await task;
      secondWaitCalled.ShouldBeTrue();
      task.IsCompleted.ShouldBeTrue();
    }

    static void WaitForTask(ref Task secondDelay)
    {
      while (Interlocked.Exchange(ref secondDelay, null) == null)
      {
        Thread.Sleep(1.Milliseconds());
      }
    }

    [Fact]
    public async Task two_threads()
    {
      bool t1 = false, t2 = false, t3 = false, t4 = false;

      Task threadADelay1 = null;
      Task threadBDelay1 = null;
      
      var threadA = Task.Run(async () =>
      {
        // t = 0
        await (threadADelay1 = clock.Delay(1.Seconds()));
        t1 = true;

        // t = 1
        await (threadADelay1 = clock.Delay(2.Seconds()));
        // t = 3
        t3 = true;
      });
      var threadB = Task.Run(async () =>
      {
        // t = 0
        await (threadBDelay1 = clock.Delay(2.Seconds()));
        t2 = true;

        // t = 2
        await (threadBDelay1 = clock.Delay(2.Seconds()));
        // t = 4
        t4 = true;
      });
      
      WaitForTask(ref threadADelay1);
      WaitForTask(ref threadBDelay1);

      await clock.MoveForward(1.Seconds());
      WaitForTask(ref threadADelay1);
      
      t1.ShouldBeTrue();
      t3.ShouldBeFalse();
      t2.ShouldBeFalse();
      t4.ShouldBeFalse();
      
      await clock.MoveForward(1.Seconds());
      WaitForTask(ref threadBDelay1);
      
      t1.ShouldBeTrue();
      t2.ShouldBeTrue();
      
      t3.ShouldBeFalse();
      t4.ShouldBeFalse();
      
      await clock.MoveForward(1.Seconds());
      await threadA;
      
      t1.ShouldBeTrue();
      t2.ShouldBeTrue();
      
      t3.ShouldBeTrue();
      t4.ShouldBeFalse();
      
      await clock.MoveForward(1.Seconds());
      await threadB;
      
      
      t1.ShouldBeTrue();
      t2.ShouldBeTrue();
      
      t3.ShouldBeTrue();
      t4.ShouldBeTrue();
      
    }

    [Fact]
    public async Task chained()
    {
      Task firstTask = null;
      var task = Task.Run(async () =>
      {
        await (firstTask = clock.Delay(1.Hours()));
        await clock.Delay(1.Hours());
      });
      
      WaitForTask(ref firstTask);
      await clock.MoveForward(2.Hours());

      await task;
      task.IsCompleted.ShouldBeTrue();
    }
  }
}
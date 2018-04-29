# Tardis
_Time testing made easy_

Time is relative, but the dotnet platform makes writing testable code dealing with timings hard, as all members are static.

This leads to the widespread use of framework-specific abstractions, requiring implementing `ISystemClock` once per framework.

After writing an equivalent to this on each and every project over the last few years, enough is enough, I built a new library: Tardis.

## Getting started

With Tardis, you can freeze time and navigate it as you wish.

```chsarp
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
```

You can now design your libary to use lambdas to get the current time.

```csharp
class TimeConsumer
{
  readonly Func<DateTimeOffset> _now = () => DateTimeOffset.UtcNow;

  public TimeConsumer(Func<DateTimeOffset> now = null)
  {
    if (now != null)
      _now = now;
  }
}
```

Putting your classes under test becomes very easy.

```csharp
public class MyClass
{
  Func<DateTimeOffset> _clock;
  DateTimeOffset LastLogTime { get; set; }
  
  public MyClass(Func<DateTimeOffset> clock = null)
  {
    _clock = clock ?? ()=> DateTimeOffset.UtcNow;
  }
  
  public void LogTime()
  {
    LastLogTime = _clock();
  }
}

publi class MyClassTest
{
    [Fact]
    public void time_is_logged()
    {
        var myClass = new MyClass(clock.UtcNow);
        
        var clock = new Clock();
        clock.Freeze();
        var frozenTime = clock.UtcNow();
        // the clock is now frozen and will always return the same value
        
        myClass.LogTime();
        myClass.LastLogTime.ShouldBe(frozenTime);
        
        // we move the clock forward by an hour, so the clock will return the original frozen time plus an hour
        clock.MoveForward(TimeSpan.FromHours(1));
        
        myClass.LogTime();
        myClass.LastLogTime.ShouldBe(frozenTime + TimeSpan.FromHours);
    }
}
```

## Delays

It's common for classes to delay some processing until later, using `Task.Delay(TimeSpan delay)`.
Fortunately, Tardis also provides support for this.

```csharp
  class DelayProgram
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
  }
```

This also works on multipe threads. 

## Stopwatches

You can also use the lambda approach to generate stopwatches that will respect the clock time.

```csharp
  class StopwatchProgram
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
  }
```

Your library would depend on the following lambda.

```
  class StopwatchConsumer
  {
    Func<(Func<TimeSpan> elapsed, Action start, Action stop)> _stopwatch = () =>
    {
      var sw = new Stopwatch();
      return (() => sw.Elapsed, sw.Start, sw.Stop);
    };

    public StopwatchConsumer(Func<(Func<TimeSpan> elapsed, Action start, Action stop)> stopwatch = null)
    {
      if (stopwatch != null)
        _stopwatch = stopwatch;
    }
  }
```

The lambda looks a bit odd, but it makes your library dependency-free.

## Fluent time building

The Tardis.FluentTime package allow you to create dates, times and timespans in a more natural fashion.

```
// create a TimeSpan of 2 seconds
var twoSecondsTimeSpan = 2.Seconds();

// create a Date
var importantDate = 23.November(1963);

// create a Date and time
var importantDateAndTime = 23.November(1963).At("18:00");
```
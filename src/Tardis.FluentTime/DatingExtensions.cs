using System;

namespace Tardis.FluentTime
{
  public static class DatingExtensions
  {
    public static DateTimeOffset January(this int day, int year)
    {
      return new DateTimeOffset(year, 1, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset February(this int day, int year)
    {
      return new DateTimeOffset(year, 2, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset March(this int day, int year)
    {
      return new DateTimeOffset(year, 3, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset April(this int day, int year)
    {
      return new DateTimeOffset(year, 4, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset May(this int day, int year)
    {
      return new DateTimeOffset(year, 5, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset June(this int day, int year)
    {
      return new DateTimeOffset(year, 6, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset July(this int day, int year)
    {
      return new DateTimeOffset(year, 7, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset August(this int day, int year)
    {
      return new DateTimeOffset(year, 8, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset September(this int day, int year)
    {
      return new DateTimeOffset(year, 9, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset October(this int day, int year)
    {
      return new DateTimeOffset(year, 10, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset November(this int day, int year)
    {
      return new DateTimeOffset(year, 11, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset December(this int day, int year)
    {
      return new DateTimeOffset(year, 12, day, 0, 0, 0, TimeSpan.Zero);
    }

    public static DateTimeOffset At(this DateTimeOffset date, string time)
    {
      var timespan = TimeSpan.Parse(time);
      return new DateTimeOffset(date.Year, date.Month, date.Day, timespan.Hours, timespan.Minutes, timespan.Seconds,
        timespan.Milliseconds, TimeSpan.Zero);
    }
  }
}
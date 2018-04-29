using System;

namespace Tardis.FluentTime
{
  public static class TimingExtensions
  {
    public static TimeSpan Milliseconds(this double time)
    {
      return TimeSpan.FromMilliseconds(time);
    }

    public static TimeSpan Milliseconds(this int time)
    {
      return TimeSpan.FromMilliseconds(time);
    }

    public static TimeSpan Seconds(this int time)
    {
      return TimeSpan.FromSeconds(time);
    }

    public static TimeSpan Seconds(this double time)
    {
      return TimeSpan.FromSeconds(time);
    }

    public static TimeSpan Minutes(this int time)
    {
      return TimeSpan.FromMinutes(time);
    }

    public static TimeSpan Minutes(this double time)
    {
      return TimeSpan.FromMinutes(time);
    }

    public static TimeSpan Hours(this int time)
    {
      return TimeSpan.FromHours(time);
    }

    public static TimeSpan Hours(this double time)
    {
      return TimeSpan.FromHours(time);
    }

    public static TimeSpan Days(this int time)
    {
      return TimeSpan.FromDays(time);
    }

    public static TimeSpan Days(this double time)
    {
      return TimeSpan.FromDays(time);
    }
  }
}
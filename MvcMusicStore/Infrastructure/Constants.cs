using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Infrastructure
{
    public class Constants
    {
        public static List<Counters> Counters;

        static Constants()
        {
            Counters = new List<Counters>();

            Counters.Add(GetLogInCounter());
            Counters.Add(GetLogOutCounter());
            Counters.Add(GetRegisterCounter());
        }

        public static Counters GetLogInCounter()
        {
            return new Counters(
                "# Number successful LogIn",
                "Number successful LogIn",
                 PerformanceCounterType.NumberOfItems32);
        }

        public static Counters GetRegisterCounter()
        {
            return new Counters(
                "# Number of Registration",
                "Number of Registration users",
                 PerformanceCounterType.NumberOfItems32);
        }

        public static Counters GetLogOutCounter()
        {
            return new Counters(
                "# Number successful LogOut",
                "Number successful LogOut",
                 PerformanceCounterType.NumberOfItems32);
        }
    }

    public class Counters
    {
        public string CounterName { get; private set; }
        public string CounterHelp { get; private set; }
        public PerformanceCounterType CounterType { get; private set; }
        public Counters(string counterName, string counterHelp, PerformanceCounterType counterType)
        {
            this.CounterName = counterName;
            this.CounterHelp = counterHelp;
            this.CounterType = counterType;
        }
    }
}
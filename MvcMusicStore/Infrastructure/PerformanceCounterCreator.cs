using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Infrastructure
{
    public class PerformanceCounterCreator
    {
        const string CATEGORY_NAME = "MvcMusicStore";
        const string CATEGORY_HELP = "Information about different counters for MvcMusicStore";

        public static void CreateAllCounters()
        {
            try
            {
                if (PerformanceCounterCategory.Exists(CATEGORY_NAME))
                    PerformanceCounterCategory.Delete(CATEGORY_NAME);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Run Visual Studio as Administrator!");
            }

            CounterCreationDataCollection counters = new CounterCreationDataCollection();
            CounterCreationData usersAtWork;

            foreach (var counter in Constants.Counters)
            {
                usersAtWork = new CounterCreationData(counter.CounterName,
                counter.CounterHelp,
                counter.CounterType);

                counters.Add(usersAtWork);
            }

            CreateCounter(counters);
        }

        public static void SuccessfulLoginIncriment()
        {
            Incriment(Constants.GetLogInCounter());
        }

        public static void RegistrationUserIncriment()
        {
            Incriment(Constants.GetRegisterCounter());
        }

        public static void SuccessfulLogoutIncriment()
        {
            Incriment(Constants.GetLogOutCounter());
        }

        private static void Incriment(Counters counter)
        {
            using (var logInCounter = new PerformanceCounter(
                        CATEGORY_NAME, counter.CounterName, false))
            {
                logInCounter.Increment();
            }
        }

        private static void CreateCounter(CounterCreationDataCollection counters)
        {
            try
            {
                if (!PerformanceCounterCategory.Exists(CATEGORY_NAME))
                {
                    PerformanceCounterCategory.Create(CATEGORY_NAME, CATEGORY_HELP, counters);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }


    }
}
using EntiCS.Profiling;
using EntiCS.Profiling.Profilers;
using EntiCS.Ticking;
using EntiCS.World;
using EntiCS.WorldManagement;
using System;

namespace EntiCS
{
    public class EntiCSFactory
    {
        public static IWorld CreateWorld()
        {
            return new EntiCSWorld();
        }

        public static ITicker CreateTicker()
        {
            return new EntiCSTicker();
        }

        public static IEntiCSRunner CreateRunner(IWorld world, ITicker ticker)
        {
            return new EntiCSRunner(world, ticker);
        }

        public static IEntiCSRunner CreateRunner()
        {
            var world = CreateWorld();
            var ticker = CreateTicker();
            return CreateRunner(world, ticker);
        }

        public static IEntiCSRunner CreateRunner(ITicker ticker)
        {
            var world = CreateWorld();
            return CreateRunner(world, ticker);
        }

        public static IEntiCSRunner CreateRunner(IWorld world)
        {
            var ticker = CreateTicker();
            return CreateRunner(world, ticker);
        }

        public static IEntiCSProfiler CreateProfiler(
            IEntiCSRunner runner,
            IProfilerSettings settings = null)
        {
            settings ??= new ProfilerSettings();

            if (!settings.IsEnabled)
            {
                return new EntiCSProfiler(settings, Array.Empty<IProfiler>());
            }

            var profilers = new IProfiler[]
            {
                new TickProfiler(settings, runner.Ticker),
                new SystemsProfiler(settings, runner)
            };

            return new EntiCSProfiler(settings, profilers);
        }
    }
}

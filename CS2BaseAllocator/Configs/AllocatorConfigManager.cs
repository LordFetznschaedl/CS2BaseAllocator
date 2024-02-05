
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Logging;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Configs
{
    public static class AllocatorConfigManager
    {
        private static readonly DirectoryInfo? _rootDir;

        private static ILogger _logger = CoreLogging.Factory.CreateLogger("AllocatorConfigManager");

        static AllocatorConfigManager()
        {
            var location = Assembly.GetExecutingAssembly().Location;

            _logger.LogInformation($"Location: {location}");

            if(!string.IsNullOrWhiteSpace(location))
            {
                _rootDir = _rootDir = new FileInfo(location).Directory?.Parent;

                _logger.LogInformation($"Allocator Config Location: {_rootDir?.FullName}");
            }



        }

        public static T Load<T>(string allocatorName) where T : IBaseAllocatorConfig, new()
        {
            T allocatorConfig = (T)Activator.CreateInstance(typeof(T))!;



            return allocatorConfig;
        }
    }
}

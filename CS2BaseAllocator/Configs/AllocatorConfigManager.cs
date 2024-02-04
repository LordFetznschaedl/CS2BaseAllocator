
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Logging;
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
        private static readonly DirectoryInfo? _dir;

        private static ILogger _logger = CoreLogging.Factory.CreateLogger("AllocatorConfigManager");

        static AllocatorConfigManager()
        {
            _dir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

            _logger.LogInformation($"Allocator Config Location: {_dir?.FullName}");
        }

        public static T Load<T>(string allocatorName) where T : IBasePluginConfig, new()
        {
            T allocatorConfig = (T)Activator.CreateInstance(typeof(T))!;



            return allocatorConfig;
        }
    }
}

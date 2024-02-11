﻿
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Logging;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces;
using CSZoneNet.Plugin.CS2BaseAllocator.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Configs
{
    public static class AllocatorConfigManager
    {
        private static readonly DirectoryInfo? _rootDir;
        private static readonly string _allocatorConfigsDirectory;

        private static ILogger _logger = CoreLogging.Factory.CreateLogger("AllocatorConfigManager");

        static AllocatorConfigManager()
        {
            _rootDir = PathUtils.CounterStrikeSharpRootDirectoryInfo;
            _allocatorConfigsDirectory = PathUtils.AllocatorConfigDirectory;

            _logger.LogInformation($"Allocator Config Location: {_rootDir?.FullName}");
        }

        public static T Load<T>(string allocatorName, string? configName = null) where T : IBaseAllocatorConfig, new()
        {
            _logger.LogInformation($"Loading allocator config {allocatorName}...");

            string directoryPath = Path.Combine(_allocatorConfigsDirectory, allocatorName);

            string configPath = Path.Combine(directoryPath, configName == null ? $"{allocatorName}.json" : $"{configName}.json");

            T allocatorConfig = (T)Activator.CreateInstance(typeof(T))!;

            if (!File.Exists(configPath))
            {
                _logger.LogInformation($"Allocator config for {allocatorName} does not exist. Creating one...");

                try
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    StringBuilder builder = new StringBuilder();
                    builder.Append(
                        $"// This configuration was automatically generated by CSZoneNet.Plugin.CS2BaseAllocator for allocator '{allocatorName}', at {DateTimeOffset.Now:yyyy/MM/dd hh:mm:ss}\n");
                    builder.Append(JsonSerializer.Serialize<T>(allocatorConfig,
                        new JsonSerializerOptions { WriteIndented = true }));
                    File.WriteAllText(configPath, builder.ToString());
                    return allocatorConfig;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to generate config for {allocatorName}");
                }
            }

            try
            {
                allocatorConfig = JsonSerializer.Deserialize<T>(File.ReadAllText(configPath), new JsonSerializerOptions() { ReadCommentHandling = JsonCommentHandling.Skip })!;

                allocatorConfig.AllocatorConfigPath = configPath;
                allocatorConfig.AllocatorConfigDirectoryPath = directoryPath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to parse config for {allocatorName}");
            }

            _logger.LogInformation($"Loading allocator config done!");

            return allocatorConfig;
        }
    }
}

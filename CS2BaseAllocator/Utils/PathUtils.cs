using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Utils
{
    public static class PathUtils
    {
        public static DirectoryInfo? CounterStrikeSharpRootDirectoryInfo => new FileInfo(typeof(CounterStrikeSharp.API.Bootstrap).Assembly.Location).Directory?.Parent;
        public static string CounterStrikeSharpRootDirectoryPath => CounterStrikeSharpRootDirectoryInfo?.FullName ?? string.Empty;
        public static string AllocatorConfigDirectory => Path.Combine(CounterStrikeSharpRootDirectoryPath, "configs", "allocators");


    }
}

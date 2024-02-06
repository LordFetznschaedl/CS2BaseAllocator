using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Configs.Base
{
    public class BaseAllocatorConfig : IBaseAllocatorConfig
    {
        public int Version { get; set; }

        [JsonIgnore]
        public string AllocatorConfigDirectoryPath { get; set; } = string.Empty;

        [JsonIgnore]
        public string AllocatorConfigPath { get; set; } = string.Empty; 
    }
}

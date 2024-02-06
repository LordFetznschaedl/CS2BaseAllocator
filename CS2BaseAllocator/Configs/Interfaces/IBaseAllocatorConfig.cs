﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces
{
    public interface IBaseAllocatorConfig
    {
        int Version { get; set; }


        string AllocatorConfigDirectoryPath { get; set; }

        string AllocatorConfigPath { get; set; }
    }
}

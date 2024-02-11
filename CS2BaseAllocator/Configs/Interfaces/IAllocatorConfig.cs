using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces
{
    public interface IAllocatorConfig<T> where T: IBaseAllocatorConfig, new()
    {
        T Config { get; set; }

        public void OnAllocatorConfigParsed(T config);
    }
}

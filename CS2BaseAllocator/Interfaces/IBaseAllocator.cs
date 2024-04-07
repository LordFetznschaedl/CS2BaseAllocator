using CounterStrikeSharp.API.Core;
using CSZoneNet.Plugin.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Interfaces
{
    public interface IBaseAllocator
    {
        (string primaryWeapon, string secondaryWeapon, KevlarEnum kevlar, bool kit, List<GrenadeEnum> grenades) Allocate(CCSPlayerController player, RoundTypeEnum roundType = RoundTypeEnum.Undefined);

        void InitializeConfig(object instance, Type allocatorType);
        void InjectBasePluginInstance(IPlugin basePluginInstance);
        void OnGunsCommand(CCSPlayerController? player);
        void OnPlayerConnected(CCSPlayerController? player);
        void OnPlayerDisconnected(CCSPlayerController? player);
    }
}

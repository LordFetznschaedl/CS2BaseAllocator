using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Base;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces;
using CSZoneNet.Plugin.CS2BaseAllocator.Entities;
using CSZoneNet.Plugin.CS2BaseAllocator.Interfaces;
using CSZoneNet.Plugin.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator
{
    public abstract class BaseGrenadeAllocator : BaseAllocator, IBaseGrenadeAllocator
    {
        private List<GrenadeKitEntity> _grenadeKitList { get; set; } = new List<GrenadeKitEntity>();

        protected List<GrenadeEnum> AllocateGrenades(CCSPlayerController player, RoundTypeEnum roundType)
        {
            var grenadeKit = new List<GrenadeEnum>();

            if (!this._grenadeKitList.Any())
            {
                this.InitializeGrenadeConfig();
            }

            if(player == null || !player.IsValid || player.PlayerPawn == null || !player.PlayerPawn.IsValid || player.PlayerPawn.Value == null || !player.PlayerPawn.Value.IsValid)
            {
                return grenadeKit;
            }

            var team = (CsTeam)player.TeamNum;

            var availableGrenadeKitsForPlayer = this.GetGrenadeKitEntities(team, roundType);

            if (!availableGrenadeKitsForPlayer.Any())
            {
                availableGrenadeKitsForPlayer = this.GetGrenadeKitEntities(team, RoundTypeEnum.Undefined);
            }

            if (!availableGrenadeKitsForPlayer.Any())
            {
                return grenadeKit;
            }

            var random = new Random();
            var chosenGrenadeKit = availableGrenadeKitsForPlayer.OrderBy(x => random.Next()).FirstOrDefault();

            if (chosenGrenadeKit == null)
            {
                return grenadeKit;
            }

            return chosenGrenadeKit.GrenadeList;
        }

        private void InitializeGrenadeConfig()
        {
            var config = typeof(AllocatorConfigManager)
                .GetMethod("Load")!
                .MakeGenericMethod(typeof(BaseGrenadeAllocatorConfig))
                .Invoke(null, new object[] { "BaseAllocator", "GrenadeKits" }) as BaseGrenadeAllocatorConfig;

            this._grenadeKitList = config?.GrenadeKits ?? new List<GrenadeKitEntity> { new GrenadeKitEntity(new List<GrenadeEnum>() { }), };
        }

        private List<GrenadeKitEntity> GetGrenadeKitEntities(CsTeam team, RoundTypeEnum roundType) => this._grenadeKitList.Where(x => (x.Team == CsTeam.None || x.Team == team) && roundType == x.RoundType).ToList();
    }
}

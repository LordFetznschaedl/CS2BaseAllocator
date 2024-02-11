using CounterStrikeSharp.API.Modules.Utils;
using CSZoneNet.Plugin.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSZoneNet.Plugin.CS2BaseAllocator.Entities
{
    public class GrenadeKitEntity
    {
        public List<GrenadeEnum> GrenadeList { get; set; } = new List<GrenadeEnum>();

        //CsTeam.None = Both Teams
        public CsTeam Team { get; set; } = CsTeam.None;

        public RoundTypeEnum RoundType { get; set; } = RoundTypeEnum.Undefined;


        public GrenadeKitEntity() { }

        public GrenadeKitEntity(List<GrenadeEnum> grenades, CsTeam team = CsTeam.None, RoundTypeEnum roundType = RoundTypeEnum.Undefined)
        {
            this.GrenadeList = grenades;
            this.Team = team;
            this.RoundType = roundType;
        }
    }
}

using CounterStrikeSharp.API.Core;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs;
using CSZoneNet.Plugin.CS2BaseAllocator.Configs.Interfaces;
using CSZoneNet.Plugin.CS2BaseAllocator.Interfaces;
using CSZoneNet.Plugin.Utils.Enums;
using System.Reflection;

namespace CSZoneNet.Plugin.CS2BaseAllocator
{
    public abstract class BaseAllocator : IBaseAllocator
    {
        public abstract (string primaryWeapon, string secondaryWeapon, KevlarEnum kevlar, bool kit, List<GrenadeEnum> grenades) Allocate(CCSPlayerController player, RoundTypeEnum roundType = RoundTypeEnum.Undefined);

        public void InitializeConfig(object instance, Type allocatorType)
        {
            Console.WriteLine($"--- Type: {allocatorType}");

            Type[] interfaces = allocatorType.GetInterfaces();
            Console.WriteLine($"--- Interfaces: {interfaces.Count()}");

            Func<Type, bool> predicate = (i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAllocatorConfig<>));

            // if the plugin has set a configuration type (implements IAllocatorConfig<>)
            if (interfaces.Any(predicate))
            {
                // IAllocatorConfig<>
                Type @interface = interfaces.Where(predicate).FirstOrDefault()!;

                // custom config type passed as generic
                Type genericType = @interface!.GetGenericArguments().First();

                var config = typeof(AllocatorConfigManager)
                    .GetMethod("Load")!
                    .MakeGenericMethod(genericType)
                    .Invoke(null, new object[] { this.GetType().Name }) as IBaseAllocatorConfig;

                // we KNOW that we can do this "safely"
                allocatorType.GetRuntimeMethod("OnAllocatorConfigParsed", new Type[] { genericType })
                    .Invoke(instance, new object[] { config });
            }
        }

        /// <summary>
        /// Override OnGunsCommand to be able to handle the guns command.
        /// </summary>
        public virtual void OnGunsCommand(CCSPlayerController? player)
        {
            //BaseAllocator implementation: Do nothing...
            return;
        }
    }
}
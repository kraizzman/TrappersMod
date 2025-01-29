using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace TrappersMod.Patches;

[HarmonyPatch(typeof(BlockEntityBasketTrap))]
public static class BlockEntityBasketTrapPatch
{
    [HarmonyPostfix, HarmonyPatch(nameof(BlockEntityBasketTrap.IsSuitableFor)), HarmonyPriority(Priority.Last)]
    public static void IsSuitableFor(BlockEntityBasketTrap __instance, Entity entity, CreatureDiet diet, ref bool __result, ref InventoryGeneric ___inv)
    {
        if (__instance.TrapState == EnumTrapState.Ready && ___inv[0]?.Itemstack != null && diet != null && diet.Matches(___inv[0].Itemstack))
        {
            __result = true;
        }
    }
}
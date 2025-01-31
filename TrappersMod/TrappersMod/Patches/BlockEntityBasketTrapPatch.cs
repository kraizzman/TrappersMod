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
        if (__instance.TrapState == EnumTrapState.Ready && ___inv[0]?.Itemstack != null && diet != null && !diet.Matches(___inv[0].Itemstack))
        {
            __result = true;
        }
    }

    [HarmonyPostfix, HarmonyPatch("TrapAnimal"), HarmonyPriority(Priority.Last)]
    private static void TrapAnimal(BlockEntityBasketTrap __instance, Entity entity, ref InventoryGeneric ___inv)
    {
        BlockEntityAnimationUtil animUtil = __instance.GetBehavior<BEBehaviorAnimatable>().animUtil;
        ;
        if (animUtil != null)
            animUtil.StartAnimation(new AnimationMetaData()
            {
                Animation = "triggered",
                Code = "triggered"
            });
        float num1 = 1;
        if (__instance.Api.World.Rand.NextDouble() < (double)num1)
        {
            JsonItemStack jsonItemStack = __instance.Block.Attributes["creatureContainer"].AsObject<JsonItemStack>();
            jsonItemStack.Resolve(__instance.Api.World, "creature container of " + (string)__instance.Block.Code);
            ___inv[0].Itemstack = jsonItemStack.ResolvedItemstack;
            BlockBehaviorCreatureContainer.CatchCreature(___inv[0], entity);
        }

        __instance.TrapState = EnumTrapState.Trapped;
        __instance.MarkDirty(true);
        __instance.Api.World.PlaySoundAt(new AssetLocation("sounds/block/reedtrapshut"), __instance.Pos, -0.25, randomizePitch: false, range: 16f);
    }
}
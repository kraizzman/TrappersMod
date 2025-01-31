using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Common;

namespace TrappersMod;

public class TrappersModModSystem : ModSystem
{
    readonly Harmony _harmony = new Harmony("trappersmod");

    public override void Start(ICoreAPI api)
    {
        _harmony.PatchAll();
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
    }

    public override bool ShouldLoad(EnumAppSide forSide)
    {
        return forSide == EnumAppSide.Server;
    }

    public override void Dispose()
    {
        _harmony?.UnpatchAll(Mod.Info.ModID);
    }
}
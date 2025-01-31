using System.Collections.Generic;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace TrappersMod;

public class TrappersModModSystem : ModSystem
{
    readonly Harmony _harmony = new Harmony("trappersmod");
    
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        _harmony.PatchAll();
        api.Logger.Notification("Hello from template mod: " + api.Side);
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        api.Logger.Warning("Hello from template mod server side: " + Lang.Get("trappersmod:hello"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        api.Logger.Notification("Hello from template mod client side: " + Lang.Get("trappersmod:hello"));
    }

    public override bool ShouldLoad(EnumAppSide forSide)
    {
        return forSide == EnumAppSide.Server;
    }
    
    public override void Dispose() {
        _harmony?.UnpatchAll(Mod.Info.ModID);
    }
}
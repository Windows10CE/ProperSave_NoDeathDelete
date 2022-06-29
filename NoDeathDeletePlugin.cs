using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace ProperSave_NoDeathDelete;

[BepInPlugin(ModGUID, ModName, ModVer)]
[HarmonyPatch]
public class NoDeathDeletePlugin : BaseUnityPlugin
{
    public const string ModGUID = "com.Windows10CE.ProperSave_NoDeathDelete";
    public const string ModName = "ProperSave_NoDeathDelete";
    public const string ModVer = "1.0.0";

    private readonly Harmony HarmonyInstance = new(ModGUID);
    
    private void OnEnable()
    {
        HarmonyInstance.PatchAll(typeof(NoDeathDeletePlugin));
    }

    private void OnDisable()
    {
        HarmonyInstance.UnpatchSelf();
    }

    [HarmonyTargetMethod]
    private static MethodInfo GetProperSaveDeleteMethod() =>
        AccessTools.DeclaredMethod(typeof(ProperSave.ProperSavePlugin).Assembly.GetType("ProperSave.Saving"), "RunOnServerGameOver");

    [HarmonyPrefix]
    [HarmonyPatch]
    public static bool DontRun() => false;
}

using Harmony12;

namespace UITest
{
    [HarmonyPatch(typeof(ResLoader), "AddLoadedCache")]
    public static class ResLoader_AddLoadedCache_Patch
    {
        private static void Prefix(string key, UnityEngine.Object cacheObj)
        {
            Main.Logger.Log($"Load <{key}> - ({cacheObj.GetType()}){cacheObj.name} / {cacheObj}");
        }
    }
}

using Harmony12;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace UITest
{
    public class Main : MonoBehaviour
    {
        public static ModEntry Mod;
        public static ModEntry.ModLogger Logger
        {
            get { return Mod?.Logger; }
        }

        public static bool Load(ModEntry modEntry)
        {
            Mod = modEntry;
            Controller.Load();
            Logger.Log($"{Mod.Info.DisplayName} 已加载");

            //var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            //Logger.Log($"{Mod.Info.DisplayName} 已补丁");

            return true;
        }
    }
}
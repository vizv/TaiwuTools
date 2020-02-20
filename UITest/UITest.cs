using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace UITest
{
    public static class Main
    {
        public static ModEntry Mod;
        public static ModEntry.ModLogger Logger
        {
            get { return Mod?.Logger; }
        }

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            Mod = modEntry;
            Logger.Log("Hello UITest");
            Controller.Load();

            return true;
        }
    }
}
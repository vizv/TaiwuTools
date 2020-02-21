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
        public static bool Enabled
        {
            get { return Mod == null ? false : Mod.Enabled; }
        }

        public static bool Load(ModEntry modEntry)
        {
            Mod = modEntry;
            Mod.OnToggle = OnToggle;
            Logger.Log("Hello UITest");

            return true;
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Logger.Log($"Current: {Enabled}");
            Logger.Log($"Toggle: {value}");
            Logger.Log($"Camera.allCameras.Length = {Camera.allCameras.Length}");
            // if (value) Controller.Load();
            new GameObject(typeof(Main).FullName, typeof(Main));
            var GameObjectInstance = new GameObject("ControllerGameObject", typeof(Controller));
            DontDestroyOnLoad(GameObjectInstance);

            return true;
        }

        void Awake()
        {
            Logger.Log("Main#Awake()");
            DontDestroyOnLoad(this);
        }
    }
}
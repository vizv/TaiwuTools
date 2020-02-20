using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UITest
{
    class Controller : MonoBehaviour
    {
        //private static Controller Instance;
        private static GameObject GameObjectInstance;

        void Awake()
        {
            Main.Logger.Log("Controller#Awake()");
            SceneManager.sceneLoaded += onSceneLoaded;
        }

        private void onSceneLoaded(Scene scene, LoadSceneMode _mode)
        {
            Main.Logger.Log($"Controller#onSceneLoaded({scene.name})");
        }

        void Update()
        {
            // FIXME: DEBUG
            if (Input.GetKeyDown("delete"))
            {

                //Main.Logger.Log("All Scenes:");
                //for (var i = 0; i < SceneManager.sceneCount; i++)
                //{
                //    var scene = SceneManager.GetSceneAt(i);
                //    Main.Logger.Log(scene.name);
                //}
                //var activeScene = SceneManager.GetActiveScene();
                //Main.Logger.Log("Active Scene:" + activeScene.name);
                //Main.Logger.Log("Game Objects:");

                //foreach (var go in activeScene.GetRootGameObjects())
                //{
                //    dumpGameObject(go);
                //}

                var defaultScene = GameObjectInstance.gameObject.scene;
                foreach (var go in defaultScene.GetRootGameObjects())
                {
                    dumpGameObject(go);
                }

                //GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                //foreach (GameObject go in allObjects)
                //{
                //    if (go.activeInHierarchy)
                //    {
                //        Main.Logger.Log("=====");
                //        // dumpGameObject(go);
                //        // Main.Logger.Log($"{go.name}({go.transform.name}) -> {go.transform.parent.gameObject.name}");
                //        Main.Logger.Log($"[{go.scene.name}#{go.scene.GetHashCode()}] {go.transform} -> {go.transform.parent}");
                //    }
                //}
            }
        }

        private static void dumpGameObject(GameObject gameObject, int level = 0)
        {
            var prefix = string.Concat(Enumerable.Repeat("  ", level)) + "- ";
            Main.Logger.Log($"{prefix}+{gameObject.name}");

            foreach (Component component in gameObject.GetComponents<Component>())
            {
                dumpComponent(component, level + 1);
            }

            foreach (Transform child in gameObject.transform)
            {
                dumpGameObject(child.gameObject, level + 1);
            }
        }

        private static void dumpComponent(Component component, int level = 0)
        {
            var prefix = string.Concat(Enumerable.Repeat("  ", level)) + "- ";
            var name = component == null ? "(null)" : component.GetType().Name;
            Main.Logger.Log($"{prefix}{name}");
        }

        internal static void Load()
        {
            Main.Logger.Log("Controller.Load()");
            if (GameObjectInstance == null)
            {
                Main.Logger.Log("Controller.Load() -> new Controller()");
                GameObjectInstance = new GameObject("ControllerGameObject");
                GameObjectInstance.AddComponent(typeof(Controller));
                DontDestroyOnLoad(GameObjectInstance);
            }
        }
    }
}

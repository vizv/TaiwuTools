using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UITest
{
    class Controller : MonoBehaviour
    {
        //private static Controller Instance;
        private static GameObject GameObjectInstance;
        private string debugText;

        void Awake()
        {
            Main.Logger.Log("Controller#Awake()");
            SceneManager.sceneLoaded += onSceneLoaded;
        }

        bool left;
        bool bottom;

        void OnGUI()
        {
            //var positions = "";
            //var allGameObjects = FindObjectsOfType<GameObject>().Where(gameObject => gameObject.activeInHierarchy);
            //foreach (var go in allGameObjects)
            //{
            //    positions += $"{String.Join("|", go.GetComponents<Component>().Select(comp => comp.GetType().Name).ToArray())}|";
            //    //positions += $"{go.transform?.position.x},{go.transform?.position.y}|";
            //}

            var screenWidth = (float)Screen.width;
            var screenHeight = (float)Screen.height;
            var position = Input.mousePosition;

            if (position.x < screenWidth / 4) left = true;
            if (screenWidth - position.x < screenWidth / 4) left = false;
            if (position.y < screenHeight / 3) bottom = true;
            if (screenHeight - position.y < screenHeight / 3) bottom = false;

            GUIStyle boxStyle = new GUIStyle("box");
            boxStyle.normal.textColor = Color.black;
            boxStyle.normal.background = Texture2D.whiteTexture;
            boxStyle.fixedWidth = screenWidth / 4;
            boxStyle.fixedHeight = screenHeight / 3;
            boxStyle.alignment = TextAnchor.UpperRight;

            GUIStyle labelStyle = new GUIStyle("label");
            labelStyle.normal.textColor = Color.blue;

            GUIStyle resultStyle = new GUIStyle("label");
            resultStyle.normal.textColor = Color.red;

            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            //Vector2 pos = Camera.current.ScreenToWorldPoint(Input.mousePosition);

            //RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), Mathf.Infinity);
            //var output = "miss";
            //if (hits.Length > 0) output = "" + hits.Length;

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            if (left) GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            if (!bottom) GUILayout.FlexibleSpace();
            GUILayout.BeginVertical("DEBUG", boxStyle);
            GUILayout.Label("Mouse Position:", labelStyle);
            GUILayout.Label(position.ToString(), labelStyle);
            GUILayout.Label("Game Object Under Mouse:", labelStyle);
            //GUILayout.Label($"{allGameObjects.Count()}", labelStyle);
            GUILayout.Label(results.Count() + "", labelStyle);
            foreach (var result in results)
            {
                GUILayout.Label($"- /{dump(result.gameObject.transform)}", resultStyle);
            }
            //GUILayout.Label(pos.ToString(), labelStyle);
            //GUILayout.Label(output, labelStyle);
            //GUILayout.Label(positions, labelStyle);
            GUILayout.EndVertical();
            if (bottom) GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            if (!left) GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private static string dump(Transform tf)
        {
            if (tf.parent != null) {
                return $"{dump(tf.parent)}/{tf.name}";
            }
            return tf.name;
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
                // DEBUG - dump all game objects
                //var defaultScene = gameObject.scene;
                //foreach (var go in defaultScene.GetRootGameObjects())
                //{
                //    dumpGameObject(go);
                //}

                //GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                //Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;

                //if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~5))
                //{
                //    Main.Logger.Log(hit.transform.gameObject.name);
                //}
                //else
                //{
                //    Main.Logger.Log($"ray: {ray}");
                //}
            }
        }

        private static void dumpGameObject(GameObject gameObject, int level = 0)
        {
            var prefix = string.Concat(Enumerable.Repeat("  ", level)) + "- ";
            Main.Logger.Log($"{prefix}+{gameObject.name}@{gameObject.transform.position}-{gameObject.layer}");

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
                GameObjectInstance = new GameObject("ControllerGameObject", typeof(Controller));
                // GameObjectInstance.AddComponent(typeof(Controller));
                DontDestroyOnLoad(GameObjectInstance);
            }
        }
    }
}

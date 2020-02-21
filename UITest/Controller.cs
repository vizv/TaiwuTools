using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UITest
{
    class Controller : MonoBehaviour
    {
        //private static Controller Instance;
        private static GameObject GameObjectInstance;
        private int opacity = 6;
        private string debugText;

        protected void Awake()
        {
            //var allObjects = FindObjectsOfType<GameObject>().Where(it => it.name == "WelcomeDialog");
            var allObjects = GameObject.Find("/UIRoot").GetComponentsInChildren<GameObject>().Where(it => it.name == "WelcomeDialog");

            var go = GameObject.Find("WelcomeDialog");
            var go1 = GameObject.Find("UIRoot");
            var go2 = GameObject.Find("/UIRoot");
            var go3 = GameObject.Find("Canvas");
            var go4 = GameObject.Find("UIRoot/Canvas");
            var go5 = GameObject.Find("UIRoot/Canvas/UIWindow");
            var go6 = GameObject.Find("UIRoot/Canvas/UIWindow/MianMenuBack");
            var go7 = GameObject.Find("UIRoot/Canvas/UIWindow/MianMenuBack/WelcomeDialog");
            debugText = $"{go?.name}/{go1?.name}/{go2.name}/{go3?.name}/{go4?.name}/{go5?.name}/{go6?.name}/{go7?.name}?{allObjects.Count()}";
        }

        //private void Debug()
        //{
        //    var welcomeDialog = GameObject.Find("UIRoot/Canvas/UIWindow/MianMenuBack/WelcomeDialog");
        //}

        protected void OnGUI()
        {
            if (opacity == 0) return;
            GUI.color = new Color(1, 1, 1, opacity / 10f);

            // styles
            GUIStyle boxStyle = new GUIStyle("box")
            {
                fixedWidth = Screen.width,
                fixedHeight = Screen.height,
            };
            GUIStyle valueStyle = new GUIStyle("label");
            valueStyle.normal.textColor = Color.red;

            // get mouse position
            var mousePosition = Camera.current.ScreenToWorldPoint(Input.mousePosition);

            // get game object under mouse
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
                position = Input.mousePosition,
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            var result = results.FirstOrDefault();
            var level = 0;
            var resultOutput = "<nothing>";
            if (results.Count() > 0)
            {
                resultOutput = Dump(result.gameObject.transform, ref level);
            }

            GUILayout.BeginVertical(boxStyle);
            GUILayout.Label("鼠标位置：");
            GUILayout.Label($"({mousePosition.x:0.0},{mousePosition.y:0.0})", valueStyle); // FIXME: use format helper for vec3
            GUILayout.Label($"当前鼠标下的 GameObjects (共 {level} 层)：");
            GUILayout.Label(resultOutput, valueStyle);
            GUILayout.Label("调试输出：");
            GUILayout.Label(debugText, valueStyle);
            GUILayout.EndVertical();
        }

        private static string Dump(Transform tf, ref int level, Transform childTf = null)
        {
            var parentOutput = "***";
            if (tf.parent != null) parentOutput = Dump(tf.parent, ref level, tf);

            var go = tf.gameObject;

            var components = tf.GetComponents<Component>();
            var gameBehaviours = components.Where(comp => comp is MonoBehaviour && comp.GetType().Namespace == null).ToArray();
            var gameBehavioursOutput = string.Join(",", gameBehaviours.Select(comp => comp.GetType().Name));
            var builtinBehaviours = components.Where(comp => comp is MonoBehaviour && comp.GetType().Namespace != null).ToArray();
            var builtinBehavioursOutput = string.Join(",", builtinBehaviours.Select(comp => comp.GetType().Name));
            var otherComponents = components.Where(comp => !(comp is MonoBehaviour)).ToArray();
            var otherComponentsOutput = string.Join(",", otherComponents.Select(comp => comp.GetType().Name));

            // FIXME: use format helper for vec3
            var output = $"\n+ {tf.GetSiblingIndex()}:{tf.name}@({tf.position.x:0.0},{tf.position.y:0.0})";
            if (gameBehaviours.Length > 0) output += $" game=[{gameBehavioursOutput}]";
            if (builtinBehaviours.Length > 0) output += $" builtin=[{builtinBehavioursOutput}]";
            if (otherComponents.Length > 0) output += $" components=[{otherComponentsOutput}]";
            for (var i = 0; i < tf.childCount; i++)
            {
                var child = tf.GetChild(i);
                output += child == childTf ? "***" : $"\n  - {i}:{child.name}";
            }
            output = output.Replace("\n", $"\n{string.Concat(Enumerable.Repeat("  ", level))}");

            level++;
            return parentOutput.Replace("***", output);
        }

        protected void Update()
        {
            // DEBUG
            if (Input.GetKeyDown("delete"))
            {
                Awake();
            }

            if (Input.GetKeyDown("-"))
            {
                opacity--;
                if (opacity < 0) opacity = 0;
            }

            if (Input.GetKeyDown("="))
            {
                opacity++;
                if (opacity > 10) opacity = 10;
            }

            if (Input.GetKeyDown("backspace"))
            {
                opacity = opacity > 5 ? 0 : 10;
            }
        }

        internal static void Load()
        {
            if (GameObjectInstance == null)
            {
                GameObjectInstance = new GameObject("ControllerGameObject", typeof(Controller));
                DontDestroyOnLoad(GameObjectInstance);
            }
        }
    }
}

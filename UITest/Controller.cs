using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using AssetsReader;
using System.IO;
using System.Collections;
using System;
using UIKit.GameObjects;
using UIKit.Components;
using UIKit.Core;

namespace UITest
{
    class Controller : MonoBehaviour
    {
        private static GameObject GameObjectInstance;
        private int opacity = 0;
        private string debugText;

        private string indexingMessage;
        private Queue<string> indexQueue;
        private Dictionary<string, Type> resourceIndex;
        private Dictionary<Type, int> resourceStats;

        private Overlay overlay;
        private Container frame;

        private BoxModel.ComponentAttributes boxModelArgs;

        protected void Awake()
        {
            // FIXME: use a resource loader
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var cimage = dialog.GetComponent<CImage>();


            overlay = new Overlay()
            {
                Name = "VizCanvas",
                Box =
                {
                    Padding = { 80 },
                },
                Children =
                {
                    (frame = new Container()
                    {
                        Name = "VizFrame",
                        Box = (boxModelArgs = new BoxModel.ComponentAttributes()
                        {
                            Direction = Direction.Horizontal,
                            Padding = { 60 },
                        }),
                        BackgroundImage = cimage,
                        Children =
                        {
                            new Container()
                            {
                                Name = "TestBlock-1",
                                Box = { PreferredSize = { 500, 0 } },
                                BackgroundColor = Color.green,
                            },
                            new Container()
                            {
                                Name = "TestBlock-2",
                                BackgroundColor = Color.blue,
                            },
                        }
                    }),
                }
            };
        }

        private void Debug()
        {
            return;

            boxModelArgs.Direction = boxModelArgs.Direction == Direction.Horizontal
                ? Direction.Vertical
                : Direction.Horizontal;
            frame.BoxModel.Apply(boxModelArgs);

            //var vf = GameObject.Find("VizFrame");
            //if (!vf) return;

            //vf.AddComponent<Mask>();

            //var tsv = new GameObject("TestScrollView", typeof(ScrollRect));
            //var tsvRt = tsv.GetComponent<RectTransform>();
            //tsvRt.SetParent(vf.transform, false);
            //tsvRt.anchorMin = Vector2.zero;
            //tsvRt.anchorMax = Vector2.one;
            //tsvRt.sizeDelta = new Vector2(-40, -40);
            ////tsvRt.sizeDelta = vf.GetComponent<RectTransform>().sizeDelta;
            //var tsvSr = tsv.GetComponent<ScrollRect>();
            //tsvSr.horizontal = false;


            //var tv = new GameObject("TestViewport", typeof(Image), typeof(Mask));
            //var tvRt = tv.GetComponent<RectTransform>();
            //tvRt.SetParent(tsvRt, false);
            ////tvRt.sizeDelta = tsvRt.sizeDelta;
            ////tvRt.sizeDelta = new Vector2(-80, -80);
            //tvRt.anchorMin = Vector2.zero;
            //tvRt.anchorMax = Vector2.one;
            ////tvRt.anchoredPosition = new Vector2(0, 0.5f);
            ////tvRt.pivot = new Vector2(0, 0.5f);
            //var tvI = tv.GetComponent<Image>();
            //var tvM = tv.GetComponent<Mask>();
            //tvI.color = Color.red;
            ////tvM.showMaskGraphic = false;

            //var tc = new GameObject("TestContent", typeof(ContentSizeFitter), typeof(VerticalLayoutGroup));
            //var tcRt = tc.GetComponent<RectTransform>();
            //tcRt.SetParent(tvRt, false);
            //tcRt.anchoredPosition = new Vector2(0, 1);
            ////tcRt.anchorMin = new Vector2(0, 1);
            //tcRt.anchorMin = new Vector2(0, 0);
            //tcRt.anchorMax = new Vector2(0, 1);
            //tcRt.pivot = new Vector2(0, 1);
            //var tcCsf = tc.GetComponent<ContentSizeFitter>();
            //var tcVlg = tc.GetComponent<VerticalLayoutGroup>();
            //tcCsf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            //tcVlg.spacing = 5;
            //tcVlg.childControlWidth = false;
            //tcVlg.childControlHeight = false;
            //tcVlg.childForceExpandWidth = false;
            //tcVlg.childForceExpandHeight = false;
            //tcVlg.childAlignment = TextAnchor.UpperLeft;

            //tsvSr.viewport = tvRt;
            //tsvSr.content = tcRt;

            //for (var i = 0; i < 20; i++)
            //{
            //    var b = new GameObject($"block-{i}", typeof(Image));
            //    var bRt = b.GetComponent<RectTransform>();
            //    var bI = b.GetComponent<Image>();
            //    bI.color = i == 0 ? Color.green : Color.blue;
            //    bRt.sizeDelta = new Vector2(600, 600);
            //    bRt.SetParent(tsvSr.content);
            //}

            //var level = 0;
            ////debugText = Dump(tv.transform, ref level);
        }

        private void IndexResources()
        {
            if (indexQueue != null) return;
            indexQueue = new Queue<string>();
            resourceIndex = new Dictionary<string, Type>();
            resourceStats = new Dictionary<Type, int>();

            var assetPath = Path.Combine(Application.dataPath, "globalgamemanagers");
            var globalGameManagersAssetsFile = new GlobalGameManagersAssetsFile(assetPath);
            foreach (var path in new HashSet<string>(globalGameManagersAssetsFile.ResourceList))
            {
                resourceIndex[path] = null;
                indexQueue.Enqueue(path);
                //Main.Logger.Log($"{path}: {resource.name} - {resource.GetType().FullName}");
            }
            StartCoroutine(LoadResource());
        }

        IEnumerator LoadResource()
        {
            if (indexQueue.Count == 0)
            {
                indexingMessage = $"已完成对 {resourceIndex.Count} 份资源的检索！";
                yield break;
            }

            var path = indexQueue.Dequeue();
            var request = Resources.LoadAsync(path);
            while (!request.isDone)
            {
                //Main.Logger.Log($"DEBUG: Loading {path}...");
                indexingMessage = $"[{resourceIndex.Count() - indexQueue.Count()}/{resourceIndex.Count()}] 正在检索 {path}...";
                yield return null;
            }

            var resource = request.asset;
            var type = resource.GetType();

            resourceIndex[path] = type;
            if (!resourceStats.ContainsKey(type)) resourceStats[type] = 0;
            resourceStats[type]++;

            //Main.Logger.Log($"{path}: {resource.name} - {resource.GetType().FullName}");
            StartCoroutine(LoadResource());
        }

        private int DebugCount(GameObject go, HashSet<GameObject> set = null)
        {
            if (set == null) set = new HashSet<GameObject>();
            if (set.Contains(go)) return 0;
            set.Add(go);

            var count = 1;
            for (var i = 0; i < go.transform.childCount; i++)
            {
                count += DebugCount(go.transform.GetChild(i).gameObject, set);
            }
            return count;
        }

        protected void OnGUI()
        {
            if (opacity == 0) return;
            GUI.color = new Color(1, 1, 1, opacity / 10f);

            GUIStyle borderStyle = new GUIStyle();
            borderStyle.normal.background = Texture2D.whiteTexture;

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
                Vector3[] corners = new Vector3[4];
                (result.gameObject.transform as RectTransform).GetWorldCorners(corners);
                var minX = Mathf.Min(corners[0].x, corners[2].x);
                var minY = Mathf.Min(corners[0].y, corners[2].y);
                var maxX = Mathf.Max(corners[0].x, corners[2].x);
                var maxY = Mathf.Max(corners[0].y, corners[2].y);
                var minScreenPos = Camera.current.WorldToScreenPoint(new Vector3(minX, minY, 100));
                var maxScreenPos = Camera.current.WorldToScreenPoint(new Vector3(maxX, maxY, 100));
                var guiPos = new Vector2(minScreenPos.x, Screen.height - maxScreenPos.y);
                var screenSize = maxScreenPos - minScreenPos;
                var guiSize = new Vector2(Mathf.Abs(screenSize.x), Mathf.Abs(screenSize.y));
                var rect = new Rect(guiPos, guiSize);
                //GUI.Box(rect, GUIContent.none, borderStyle); // Draw box
            }

            var uiRoot = GameObject.Find("/UIRoot");
            var goCount = DebugCount(uiRoot);
            var allGoCount = FindObjectsOfType<GameObject>().Count();

            var resourceStatsOutput = resourceStats == null ? "" : string.Join("", resourceStats.OrderBy(pair => pair.Key.FullName).Select(pair => $"\n{pair.Key.FullName}: {pair.Value}"));

            GUILayout.BeginVertical(boxStyle);
            GUILayout.Label("资源统计：");
            GUILayout.Label(indexingMessage + resourceStatsOutput, valueStyle);
            GUILayout.Label("鼠标位置：");
            GUILayout.Label($"({mousePosition.x:0.0},{mousePosition.y:0.0})", valueStyle); // FIXME: use format helper for vec3
            GUILayout.Label($"当前 GameObjects 总数：UIRoot 中 {goCount} 个 / 总共 {allGoCount} 个");
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
            var rc = go.GetComponent<RectTransform>();

            var components = tf.GetComponents<Component>();
            var gameBehaviours = components.Where(comp => comp is MonoBehaviour && comp.GetType().Namespace == null).ToArray();
            var gameBehavioursOutput = string.Join(",", gameBehaviours.Select(comp => comp.GetType().Name));
            var builtinBehaviours = components.Where(comp => comp is MonoBehaviour && comp.GetType().Namespace != null).ToArray();
            var builtinBehavioursOutput = string.Join(",", builtinBehaviours.Select(comp => comp.GetType().Name));
            var otherComponents = components.Where(comp => !(comp is MonoBehaviour)).ToArray();
            var otherComponentsOutput = string.Join(",", otherComponents.Select(comp => comp.GetType().Name));

            // FIXME: use format helper for vec3
            var output = $"\n+ {tf.GetSiblingIndex()}:{tf.name}({rc?.sizeDelta.x},{rc?.sizeDelta.y})@({rc?.position.x:0.0},{rc?.position.y:0.0},{rc?.position.z:0.0})/{go.layer}";
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
                Debug();
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

            //debugText = $"{frame.Created}";
            if (Input.GetKeyDown("f10"))
            {
                // FIXME: use utility
                var parent = GameObject.Find("/UIRoot").transform;

                if (overlay.Created)
                    overlay.Destroy();
                else
                {
                    overlay.SetParent(parent);
                }
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

using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    class Frame : ManagedGameObject
    {
        public Image BackgroundImage => ManagedObject.GetComponent<Image>();

        public new Arguments Default;
        public Frame() : this(new Arguments()) { }
        public Frame(Arguments arguments) : base(arguments) => Default = arguments;

        public override void Create()
        {
            base.Create();
            ManagedObject.AddComponent<Image>();

            var pivot = new Vector2(0.5f, 0.5f);
            var anchorMin = pivot;
            var anchorMax = pivot;
            var sizeDelta = new Vector2(Default.Width, Default.Height);
            var anchorPosition = (pivot - new Vector2(0.5f, 0.5f)) * 2 * -Default.Margin;

            if (Default.Width == 0) (anchorMin.x, anchorMax.x, sizeDelta.x) = (0, 1, -Default.Margin * 2);
            if (Default.Height == 0) (anchorMin.y, anchorMax.y, sizeDelta.y) = (0, 1, -Default.Margin * 2);

            RectTransform.pivot = pivot;
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;
            RectTransform.sizeDelta = sizeDelta;
            RectTransform.anchoredPosition = anchorPosition;

            //UITest.Main.Logger.Log($"{RectTransform.rect}@{RectTransform.position}");

            //UITest.Main.Logger.Log($"pivot: {RectTransform.pivot}");
            //UITest.Main.Logger.Log($"anchorMin: {RectTransform.anchorMin}");
            //UITest.Main.Logger.Log($"anchorMax: {RectTransform.anchorMax}");
            //UITest.Main.Logger.Log($"sizeDelta: {RectTransform.sizeDelta}");
            //UITest.Main.Logger.Log($"anchoredPosition: {RectTransform.anchoredPosition}");

            //UITest.Main.Logger.Log($"offsetMin: {RectTransform.offsetMax}");
            //UITest.Main.Logger.Log($"offsetMax: {RectTransform.offsetMax}");
            //UITest.Main.Logger.Log($"rect: {RectTransform.rect}");
            //UITest.Main.Logger.Log($"top: {RectTransform.rect.yMax}");
            //UITest.Main.Logger.Log($"right: {RectTransform.rect.xMax}");
            //UITest.Main.Logger.Log($"bottom: {RectTransform.rect.yMin}");
            //UITest.Main.Logger.Log($"left: {RectTransform.rect.xMin}");

            // FIXME: use utility
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var cimage = dialog.GetComponent<CImage>();
            BackgroundImage.type = cimage.type;
            BackgroundImage.sprite = cimage.sprite;
            BackgroundImage.color = cimage.color;
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        [Serializable()]
        public new class Arguments : ManagedGameObject.Arguments
        {
            public int Width = 0;
            public int Height = 0;
            public int Margin = 0;
        }
    }
}

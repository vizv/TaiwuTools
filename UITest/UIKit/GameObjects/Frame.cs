using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    class Frame : ManagedGameObject
    {
        public Image BackgroundImage => ManagedObject.GetComponent<Image>();

        public new Arguments DefaultArguments;
        public Frame() : this(new Arguments()) { }
        public Frame(Arguments arguments) : base(arguments) => DefaultArguments = arguments;

        public override void Create()
        {
            base.Create();
            ManagedObject.AddComponent<Image>();

            (var width, var height, var margin) = (DefaultArguments.Width, DefaultArguments.Height, DefaultArguments.Margin);
            if (width == 0) width = Screen.width - margin * 2;
            if (height == 0) height = Screen.height - margin * 2;
            RectTransform.sizeDelta = new Vector2(width, height);

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

        public new class Arguments : ManagedGameObject.Arguments
        {
            public int Width = 0;
            public int Height = 0;
            public int Margin = 0;
        }
    }
}

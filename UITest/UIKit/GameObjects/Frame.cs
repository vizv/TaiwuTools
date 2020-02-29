using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Frame : BoxModelGameObject
    {
        public Image Background => Get<Image>();

        public new Arguments Default;
        public Frame() : this(new Arguments()) { }
        public Frame(Arguments arguments) : base(arguments) => Default = arguments;

        public override void Create()
        {
            base.Create();

            if (Default.Background)
            {
                var image = Default.Background;

                Background.type = image.type;
                Background.sprite = image.sprite;
                Background.color = image.color;
            }
        }

        public new class Arguments : BoxModelGameObject.Arguments
        {
            public Image Background = null;
        }
    }
}

using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Container : BoxModelGameObject
    {
        public Image Background => Get<Image>();

        public new Arguments Default;
        public Container() : this(new Arguments()) { }
        public Container(Arguments arguments) : base(arguments) => Default = arguments;

        public override void Create()
        {
            base.Create();

            if (Default.BackgroundImage)
            {
                var image = Default.BackgroundImage;

                Background.type = image.type;
                Background.sprite = image.sprite;
                Background.color = image.color;
            }

            if (Default.BackgroundColor.HasValue)
            {
                Background.color = Default.BackgroundColor.Value;
            }
        }

        public new class Arguments : BoxModelGameObject.Arguments
        {
            public Image BackgroundImage = null;
            public Color? BackgroundColor = null;
        }
    }
}

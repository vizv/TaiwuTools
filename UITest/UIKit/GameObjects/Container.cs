using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public partial class Container : BoxModelGameObject
    {
        // FIXME: Add SerializableField Tag
        public Image BackgroundImage = null;
        // FIXME: Add SerializableField Tag
        public Color? BackgroundColor = null;

        public Image Background => Get<Image>();

        public override void Create(bool active = true)
        {
            base.Create(active);

            if (BackgroundImage)
            {
                var image = BackgroundImage;

                Background.type = image.type;
                Background.sprite = image.sprite;
                Background.color = image.color;
            }

            if (BackgroundColor.HasValue)
            {
                Background.color = BackgroundColor.Value;
            }
        }
    }
}

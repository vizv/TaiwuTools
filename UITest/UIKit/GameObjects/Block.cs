using UIKit.Components;
using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Block : BoxElementGameObject
    {
        // FIXME: Add SerializableField Tag
        public Color? BackgroundColor = null;
        public Image Background => Get<Image>();

        public override void Create(bool active = true)
        {
            base.Create(active);

            if (BackgroundColor.HasValue)
            {
                Background.color = BackgroundColor.Value;
            }

            BoxElement.Apply(Element);
        }
    }
}

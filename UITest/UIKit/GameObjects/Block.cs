using UIKit.Components;
using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Block : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public Color? BackgroundColor = null;

        // FIXME: Add SerializableField Tag
        public BoxElement.ComponentAttributes Element = new BoxElement.ComponentAttributes();
        public BoxElement BoxElement => Get<BoxElement>();

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

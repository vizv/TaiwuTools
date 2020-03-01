using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UITest.UIKit.GameObjects
{
    public class Block : ManagedGameObject
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
        }
    }
}

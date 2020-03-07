using UIKit.Components;
using UIKit.Core;

namespace UIKit.GameObjects
{
    public class Label : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public TextControl.ComponentAttributes Text = new TextControl.ComponentAttributes();
        public TextControl TextControl => Get<TextControl>();

        public override void Create(bool active = true)
        {
            base.Create(active);

            TextControl.Apply(Text);
        }
    }
}

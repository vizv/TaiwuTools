using UIKit.Components;

namespace UIKit.Core.GameObjects
{
    public class BoxElementGameObject : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public BoxElement.ComponentAttributes Element = new BoxElement.ComponentAttributes();
        public BoxElement BoxElement => Get<BoxElement>();

        public override void Create(bool active = true)
        {
            base.Create(active);

            BoxElement.Apply(Element);
        }
    }
}

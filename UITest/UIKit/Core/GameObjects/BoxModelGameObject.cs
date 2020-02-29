using UIKit.Components;
using UnityEngine.UI;

namespace UIKit.Core.GameObjects
{
    public class BoxModelGameObject : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public BoxModel.Arguments Box = new BoxModel.Arguments();

        public BoxModel BoxModel => Get<BoxModel>();
        public HorizontalOrVerticalLayoutGroup LayoutGroup => BoxModel.LayoutGroup;

        public override void Create(bool active = true)
        {
            base.Create(active);

            BoxModel.Apply(Box);
        }
    }
}

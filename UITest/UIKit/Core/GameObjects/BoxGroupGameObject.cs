using UIKit.Components;
using UnityEngine.UI;

namespace UIKit.Core.GameObjects
{
    public class BoxGroupGameObject : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public BoxGroup.ComponentAttributes Group = new BoxGroup.ComponentAttributes();
        public BoxGroup BoxGroup => Get<BoxGroup>();

        public HorizontalOrVerticalLayoutGroup LayoutGroup => BoxGroup.LayoutGroup;

        public override void Create(bool active = true)
        {
            base.Create(active);

            BoxGroup.Apply(Group);
        }
    }
}

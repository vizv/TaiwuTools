﻿using UIKit.Components;
using UnityEngine.UI;

namespace UIKit.Core.GameObjects
{
    public class BoxModelGameObject : ManagedGameObject
    {
        // FIXME: Add SerializableField Tag
        public BoxGroup.ComponentAttributes Group = new BoxGroup.ComponentAttributes();
        public BoxGroup BoxGroup => Get<BoxGroup>();

        // FIXME: Add SerializableField Tag
        public BoxElement.ComponentAttributes Element = new BoxElement.ComponentAttributes();
        public BoxElement BoxElement => Get<BoxElement>();

        public HorizontalOrVerticalLayoutGroup LayoutGroup => BoxGroup.LayoutGroup;

        public override void Create(bool active = true)
        {
            base.Create(active);

            BoxGroup.Apply(Group);
            BoxElement.Apply(Element);
        }
    }
}

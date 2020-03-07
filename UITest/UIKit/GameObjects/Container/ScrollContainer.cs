using System.Collections.Generic;
using UIKit.Components;
using UIKit.Core;
using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public partial class Container
    {
        public class ScrollContainer : Container
        {
            // Override group field as layout group for Content
            // FIXME: Add SerializableField Tag
            public new BoxGroup.ComponentAttributes Group = new BoxGroup.ComponentAttributes();

            // FIXME: Add SerializableField Tag
            public Dictionary<string, ManagedGameObject> ContentChildren = new Dictionary<string, ManagedGameObject>();

            public ScrollRect ScrollRect => Get<ScrollRect>();

            private Content content;

            public override void Create(bool active = true)
            {
                base.Group.Padding = Group.Padding;
                base.Create(active);

                ScrollRect.horizontal = Group.Direction != Direction.Vertical;
                ScrollRect.vertical = Group.Direction != Direction.Horizontal;
                LayoutGroup.childForceExpandWidth = true;
                LayoutGroup.childForceExpandHeight = true;

                // FIXME - orphan on destroy
                var viewport = new Viewport() {
                    Name = $"{Name}:Viewport",
                    Group =
                    {
                        ForceExpandChildWidth = true,
                        ForceExpandChildHeight = true,
                    }
                };

                // FIXME - orphan on destroy
                content = new Content() {
                    Name = $"{Name}:Content",
                    Group = {
                        Direction = Group.Direction,
                        Spacing = Group.Spacing,
                        ControlChildHeight = true,
                        ControlChildWidth = true,
                        ForceExpandChildWidth = Group.Direction == Direction.Vertical,
                        ForceExpandChildHeight = Group.Direction == Direction.Horizontal,
                    },
                };

                foreach (var contentChild in ContentChildren) contentChild.Value.SetParent(content);
                content.SetParent(viewport);
                viewport.SetParent(this);

                ScrollRect.viewport = viewport.RectTransform;
                ScrollRect.content = content.RectTransform;
            }

            public void Add(string key, ManagedGameObject gameObject)
            {
                ContentChildren[key] = gameObject;
                gameObject.SetParent(content);
            }

            protected class Viewport : Container
            {
                public Mask Mask => Get<Mask>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    // color doesn't matter here, as long as there is a valid image
                    Get<Image>().color = Color.black;
                    Mask.showMaskGraphic = false;
                }
            }

            protected class Content : BoxGroupGameObject
            {
                public ContentSizeFitter ContentSizeFitter => Get<ContentSizeFitter>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    RectTransform.pivot = new Vector2(0, 1);
                    LayoutGroup.childAlignment = TextAnchor.UpperLeft;

                    ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }
        }
    }
}

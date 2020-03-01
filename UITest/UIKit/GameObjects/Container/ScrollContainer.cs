using System.Collections.Generic;
using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public partial class Container
    {
        public class ScrollContainer : Container
        {
            // FIXME: Add SerializableField Tag
            public Direction Direction = Direction.Vertical;

            // FIXME: Add SerializableField Tag
            public List<ManagedGameObject> ContentChildren = new List<ManagedGameObject>();

            public ScrollRect ScrollRect => Get<ScrollRect>();

            public override void Create(bool active = true)
            {
                base.Create(active);

                ScrollRect.horizontal = Direction != Direction.Vertical;
                ScrollRect.vertical = Direction != Direction.Horizontal;
                LayoutGroup.childForceExpandWidth = true;
                LayoutGroup.childForceExpandHeight = true;

                var viewport = new Viewport()
                {
                    Name = $"{Name}:Viewport",
                };
                ScrollRect.viewport = viewport.RectTransform;

                var content = new Content()
                {
                    Name = $"{Name}:Content"
                };
                ScrollRect.content = content.RectTransform;

                foreach (var contentChild in ContentChildren)
                {
                    contentChild.SetParent(content);
                    //UITest.Main.Logger.Log($"{contentChild.Name}:{contentChild.LayoutElement.preferredHeight}");
                }
                UITest.Main.Logger.Log($"{content.Name}:{content.LayoutElement.preferredHeight}");
                //Get<GraphicRaycaster>();

                content.SetParent(viewport);
                viewport.SetParent(this);
                //Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            public class Viewport : Container
            {
                public Mask Mask => Get<Mask>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    LayoutGroup.childForceExpandWidth = true;
                    LayoutGroup.childForceExpandHeight = true;

                    // FIXME: debug - remove
                    Get<Image>().color = Color.cyan;
                    Mask.showMaskGraphic = true;
                }
            }

            public class Content : ManagedGameObject
            {
                // FIXME: hack
                private HorizontalOrVerticalLayoutGroup LayoutGroup => Get<VerticalLayoutGroup>();

                public ContentSizeFitter ContentSizeFitter => Get<ContentSizeFitter>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    RectTransform.pivot = new Vector2(0, 1);
                    LayoutGroup.spacing = 5;
                    LayoutGroup.childControlHeight = true;
                    LayoutGroup.childForceExpandWidth = true;
                    LayoutGroup.childForceExpandHeight = false;
                    LayoutGroup.childAlignment = TextAnchor.UpperLeft;

                    //ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }
        }
    }
}

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

                var viewport = new Viewport();
                viewport.SetParent(this);
                ScrollRect.viewport = viewport.RectTransform;

                var content = new Content();
                content.SetParent(viewport);
                ScrollRect.content = content.RectTransform;

                foreach (var contentChild in ContentChildren)
                {
                    contentChild.SetParent(content);
                }
                //Get<GraphicRaycaster>();

                //Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            public class Viewport : Container
            {
                public Mask Mask => Get<Mask>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    Get<Image>().color = Color.cyan;
                    Mask.showMaskGraphic = true;
                }
            }

            public class Content : Container
            {
                public ContentSizeFitter ContentSizeFitter => Get<ContentSizeFitter>();

                public override void Create(bool active = true)
                {
                    base.Create(active);

                    ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }
        }
    }
}

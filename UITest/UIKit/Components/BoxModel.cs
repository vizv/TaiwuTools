using System;
using System.Collections.Generic;
using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.Components
{
    public class BoxModel : ManagedComponent
    {
        // FIXME: Add SerializableField Tag
        private Direction Direction;

        public LayoutElement LayoutElement => Get<LayoutElement>();
        public HorizontalOrVerticalLayoutGroup LayoutGroup {
            get
            {
                var layoutGroup = (HorizontalOrVerticalLayoutGroup)GameObject.GetComponent<HorizontalLayoutGroup>()
                    ?? GameObject.GetComponent<VerticalLayoutGroup>();

                switch (Direction)
                {
                    case Direction.Horizontal:
                        if (layoutGroup is VerticalLayoutGroup) DestroyImmediate(layoutGroup);
                        layoutGroup = Get<HorizontalLayoutGroup>();
                        break;
                    case Direction.Vertical:
                        if (layoutGroup is HorizontalLayoutGroup) DestroyImmediate(layoutGroup);
                        layoutGroup = Get<VerticalLayoutGroup>();
                        break;
                    default:
                        throw new ArgumentException($"BoxModel with {Direction} is not supported");
                }

                return layoutGroup;
            }
        }

        public override void Apply(ManagedComponent.ComponentAttributes componentAttributes)
        {
            var attributes = componentAttributes as ComponentAttributes;
            if (!attributes) return;

            Direction = attributes.Direction;
            LayoutGroup.padding = attributes.RectOffset;
            LayoutGroup.childAlignment = attributes.ChildrenAlignment;
            LayoutGroup.childForceExpandWidth = false;
            LayoutGroup.childForceExpandHeight = false;

            LayoutElement.minWidth = attributes.MinimalWidth;
            LayoutElement.minHeight = attributes.MinimalHeight;
            LayoutElement.preferredWidth = attributes.PreferredWidth;
            LayoutElement.preferredHeight = attributes.PreferredHeight;
            LayoutElement.flexibleWidth = attributes.FlexibleWidth;
            LayoutElement.flexibleHeight = attributes.FlexibleHeight;
        }

        public new class ComponentAttributes : ManagedComponent.ComponentAttributes
        {
            public Direction Direction = Direction.Vertical;
            public TextAnchor ChildrenAlignment = TextAnchor.MiddleCenter;

            public float MinimalWidth => MinimalSize.Count > 0 ? MinimalSize[0] : 0;
            public float MinimalHeight => MinimalSize.Count > 1 ? MinimalSize[1] : MinimalWidth;
            public List<float> MinimalSize = new List<float>();

            public float PreferredWidth => PreferredSize.Count > 0 ? PreferredSize[0] : 0;
            public float PreferredHeight => PreferredSize.Count > 1 ? PreferredSize[1] : PreferredWidth;
            public List<float> PreferredSize = new List<float>();

            public float FlexibleWidth => FlexibleSize.Count > 0 ? FlexibleSize[0] : (PreferredWidth > 0 ? 0 : 1);
            public float FlexibleHeight => FlexibleSize.Count > 1 ? FlexibleSize[1] : (PreferredHeight > 0 ? 0 : 1);
            public List<float> FlexibleSize = new List<float>();

            public RectOffset RectOffset
            {
                get
                {
                    var (top, right, bottom, left) = (0, 0, 0, 0);

                    if (Padding.Count > 0)
                    {
                        top = Padding[0];
                        right = top;
                        bottom = top;
                        left = top;
                    }

                    if (Padding.Count > 1)
                    {
                        right = Padding[1];
                        left = right;
                    }

                    if (Padding.Count > 2)
                    {
                        bottom = Padding[2];
                    }

                    if (Padding.Count > 3)
                    {
                        left = Padding[3];
                    }

                    return new RectOffset(left, right, top, bottom);
                }
            }
            public List<int> Padding = new List<int>();
        }
    }
}

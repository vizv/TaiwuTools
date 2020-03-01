﻿using System;
using System.Collections.Generic;
using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.Components
{
    public class BoxGroup : ManagedComponent
    {
        // FIXME: Add SerializableField Tag
        private Direction Direction;

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
            LayoutGroup.spacing = attributes.Spacing;
            LayoutGroup.childControlHeight = attributes.ControlChildHeight;
            LayoutGroup.childControlWidth = attributes.ControlChildWidth;
            LayoutGroup.childForceExpandHeight = attributes.ForceExpandChildHeight;
            LayoutGroup.childForceExpandWidth = attributes.ForceExpandChildWidth;
        }

        public new class ComponentAttributes : ManagedComponent.ComponentAttributes
        {
            // FIXME: Add SerializableField Tag
            public Direction Direction = Direction.Vertical;

            // FIXME: Add SerializableField Tag
            public TextAnchor ChildrenAlignment = TextAnchor.MiddleCenter;

            // FIXME: Add SerializableField Tag
            public List<int> Padding = new List<int>();
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

            // FIXME: Add SerializableField Tag
            public float Spacing = 0;

            // FIXME: Add SerializableField Tag
            public bool ControlChildHeight = true;

            // FIXME: Add SerializableField Tag
            public bool ControlChildWidth = true;

            // FIXME: Add SerializableField Tag
            public bool ForceExpandChildHeight = false;

            // FIXME: Add SerializableField Tag
            public bool ForceExpandChildWidth = false;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.Components
{
    public class BoxModel : ManagedComponent
    {
        private Direction direction;

        public HorizontalOrVerticalLayoutGroup LayoutGroup {
            get
            {
                var layoutGroup = (HorizontalOrVerticalLayoutGroup)ManagedObject.GetComponent<HorizontalLayoutGroup>()
                    ?? ManagedObject.GetComponent<VerticalLayoutGroup>();

                switch (direction)
                {
                    case Direction.Horizontal:
                        if (layoutGroup is VerticalLayoutGroup) Destroy(layoutGroup);
                        layoutGroup = Get<HorizontalLayoutGroup>();
                        break;
                    case Direction.Vertical:
                        if (layoutGroup is HorizontalLayoutGroup) Destroy(layoutGroup);
                        layoutGroup = Get<VerticalLayoutGroup>();
                        break;
                }

                return layoutGroup;
            }
        }

        public override void Apply(ManagedComponent.Arguments arguments)
        {
            var args = arguments as Arguments;
            if (!args) return;

            direction = args.Direction;
            LayoutGroup.padding = args.RectOffset;
            LayoutGroup.childAlignment = args.ChildrenAlignment;
        }

        public enum Direction { Horizontal, Vertical }

        public new class Arguments : ManagedComponent.Arguments
        {
            public Direction Direction = Direction.Vertical;
            public TextAnchor ChildrenAlignment = TextAnchor.MiddleCenter;

            public RectOffset RectOffset
            {
                get
                {
                    var (top, right, bottom, left) = (0, 0, 0, 0);

                    if (Padding.Count() > 0)
                    {
                        top = Padding[0];
                        right = top;
                        bottom = top;
                        left = top;

                        if (Padding.Count() > 1)
                        {
                            right = Padding[1];
                            left = right;

                            if (Padding.Count() > 2)
                            {
                                bottom = Padding[2];

                                if (Padding.Count() > 3)
                                {
                                    left = Padding[3];
                                }
                            }
                        }
                    }

                    return new RectOffset(left, right, top, bottom);
                }
            }
            public List<int> Padding = new List<int>();
        }
    }
}

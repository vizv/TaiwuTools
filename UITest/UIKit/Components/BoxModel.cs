using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.Components
{
    public abstract class BoxModel : ManagedComponent<BoxModel.Arguments>
    {
        public abstract HorizontalOrVerticalLayoutGroup LayoutGroup { get; }

        public override void Apply(Arguments arguments)
        {
            LayoutGroup.padding = arguments.RectOffset;
        }

        public new class Arguments : ManagedComponent<Arguments>.Arguments
        {
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

using System.Collections.Generic;
using UIKit.Core;
using UnityEngine.UI;

namespace UIKit.Components
{
    public class BoxElement : ManagedComponent
    {
        public LayoutElement LayoutElement => Get<LayoutElement>();

        public override void Apply(ManagedComponent.ComponentAttributes componentAttributes)
        {
            var attributes = componentAttributes as ComponentAttributes;
            if (!attributes) return;

            LayoutElement.minWidth = attributes.MinimalWidth;
            LayoutElement.minHeight = attributes.MinimalHeight;
            LayoutElement.preferredWidth = attributes.PreferredWidth;
            LayoutElement.preferredHeight = attributes.PreferredHeight;
            LayoutElement.flexibleWidth = attributes.FlexibleWidth;
            LayoutElement.flexibleHeight = attributes.FlexibleHeight;
        }

        public new class ComponentAttributes : ManagedComponent.ComponentAttributes
        {
            public float MinimalWidth => MinimalSize.Count > 0 ? MinimalSize[0] : 0;
            public float MinimalHeight => MinimalSize.Count > 1 ? MinimalSize[1] : MinimalWidth;
            public List<float> MinimalSize = new List<float>();

            public float PreferredWidth => PreferredSize.Count > 0 ? PreferredSize[0] : 0;
            public float PreferredHeight => PreferredSize.Count > 1 ? PreferredSize[1] : PreferredWidth;
            public List<float> PreferredSize = new List<float>();

            public float FlexibleWidth => FlexibleSize.Count > 0 ? FlexibleSize[0] : (PreferredWidth > 0 ? 0 : 1);
            public float FlexibleHeight => FlexibleSize.Count > 1 ? FlexibleSize[1] : (PreferredHeight > 0 ? 0 : 1);
            public List<float> FlexibleSize = new List<float>();
        }
    }
}

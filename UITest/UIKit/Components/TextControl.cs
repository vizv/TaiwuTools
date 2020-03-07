using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.Components
{
    public class TextControl : ManagedComponent
    {
        public Text Text => Get<Text>();

        public override void Apply(ManagedComponent.ComponentAttributes componentAttributes)
        {
            var attributes = componentAttributes as ComponentAttributes;
            if (!attributes) return;

            Text.font = attributes.Font;
            Text.fontSize = attributes.FontSize;
            Text.color = attributes.Color;
            Text.alignment = attributes.Alignment;
            Text.text = attributes.Content;
            Text.horizontalOverflow = HorizontalWrapMode.Overflow;
        }

        public new class ComponentAttributes : ManagedComponent.ComponentAttributes
        {
            // FIXME: Add SerializableField Tag
            public Font Font = null;

            // FIXME: Add SerializableField Tag
            public int FontSize = 16;

            // FIXME: Add SerializableField Tag
            public Color Color = Color.white;

            // FIXME: Add SerializableField Tag
            public TextAnchor Alignment = TextAnchor.MiddleCenter;

            // FIXME: Add SerializableField Tag
            public string Content = null;
        }
    }
}

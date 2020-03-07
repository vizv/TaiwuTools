using System.Collections.Generic;
using UIKit.Core;
using UIKit.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace TaiwuUIKit.GameObjects
{
    public class BaseText : Label
    {
        // FIXME: Add SerializableField Tag
        public new string Text = null;

        // FIXME: Add SerializableField Tag
        public HorizontalAnchor Alignment = HorizontalAnchor.Center;

        // FIXME: Add SerializableField Tag
        public bool UseBoldFont = false;

        // FIXME: Add SerializableField Tag
        public bool UseOutline = true;

        public override void Create(bool active = true)
        {
            base.Text.Font = UseBoldFont ? DateFile.instance.boldFont : DateFile.instance.font;
            base.Text.Alignment = (new Dictionary<HorizontalAnchor, TextAnchor>() {
                { HorizontalAnchor.Left, TextAnchor.MiddleLeft },
                { HorizontalAnchor.Center, TextAnchor.MiddleCenter},
                { HorizontalAnchor.Right, TextAnchor.MiddleRight },
            })[Alignment];
            base.Text.Content = Text;
            base.Create(active);

            if (UseOutline) Get<Outline>();
            Get<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}

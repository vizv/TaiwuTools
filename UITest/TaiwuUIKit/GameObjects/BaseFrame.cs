using System.Collections.Generic;
using UIKit.Core;
using UIKit.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace TaiwuUIKit.GameObjects
{
    public class BaseFrame : Container
    {
        // FIXME: Add SerializableField Tag
        public Direction Direction = Direction.Horizontal;

        public override void Create(bool active = true)
        {
            // FIXME: use a resource loader
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var backgroundImage = dialog.GetComponent<CImage>() as Image;
            BackgroundImage = backgroundImage;

            // Default padding
            Group.Direction = Direction;
            Group.Padding = new List<int>() { 20 };
            Group.Spacing = 20;

            base.Create(active);
        }
    }
}

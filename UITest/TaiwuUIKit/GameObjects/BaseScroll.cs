using System.Collections.Generic;
using UIKit.GameObjects;
using UnityEngine;

namespace TaiwuUIKit.GameObjects
{
    public class BaseScroll : Container.ScrollContainer
    {
        public override void Create(bool active = true)
        {
            // FIXME: use a resource loader
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var backgroundImage = dialog.GetComponent<CImage>();
            BackgroundImage = backgroundImage;

            Group.Padding = new List<int>() { 20 };
            Group.Spacing = 10;

            base.Create(active);
        }
    }
}

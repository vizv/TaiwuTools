using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Frame : BoxModelGameObject
    {
        public Image BackgroundImage => Get<Image>();

        public override void Create()
        {
            base.Create();

            // FIXME: use utility
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var cimage = dialog.GetComponent<CImage>();
            BackgroundImage.type = cimage.type;
            BackgroundImage.sprite = cimage.sprite;
            BackgroundImage.color = cimage.color;
        }
    }
}

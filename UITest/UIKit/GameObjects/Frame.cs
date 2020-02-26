using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    class Frame : ManagedGameObject
    {
        public readonly Image BackgroundImage;

        public Frame(string name, int width = 0, int height = 0, int margin = 0) : base(name)
        {
            BackgroundImage = AddComponent<Image>();

            if (width == 0 && height == 0)
            {
                width = Screen.width - margin * 2;
                height = Screen.height - margin * 2;
            }
            RectTransform.sizeDelta = new Vector2(width, height);

            // FIXME: use utility
            var dialog = Resources.Load<GameObject>("prefabs/ui/views/ui_dialog").transform.Find("Dialog");
            var cimage = dialog.GetComponent<CImage>();
            BackgroundImage.type = cimage.type;
            BackgroundImage.sprite = cimage.sprite;
            BackgroundImage.color = cimage.color;
        }
    }
}

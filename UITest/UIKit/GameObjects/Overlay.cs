using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Overlay : Container
    {
        public Canvas Canvas => Get<Canvas>();

        public override void Create()
        {
            base.Create();

            Get<GraphicRaycaster>();

            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }
}

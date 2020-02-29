using UIKit.Components;
using UIKit.Core;
using UIKit.Core.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class OverlayCanvas : BoxModelGameObject
    {
        public Canvas Canvas => Get<Canvas>();

        public override void Create()
        {
            base.Create();

            Get<GraphicRaycaster>();

            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            LayoutGroup.childForceExpandHeight = true;
            LayoutGroup.childForceExpandWidth = true;
        }
    }
}

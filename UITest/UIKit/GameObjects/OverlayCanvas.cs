using UIKit.Components;
using UIKit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class OverlayCanvas : ManagedGameObject
    {
        public OverlayCanvas() : this(new Arguments()) { }
        public OverlayCanvas(Arguments arguments) : base(arguments) { }

        public override void Create()
        {
            base.Create();

            Get<GraphicRaycaster>();

            var canvas = Get<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var boxModel = Get<BoxModel>();
            boxModel.Apply(new BoxModel.Arguments());
            boxModel.LayoutGroup.childForceExpandHeight = true;
            boxModel.LayoutGroup.childForceExpandWidth = true;
        }
    }
}

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
            var canvas = ManagedObject.AddComponent<Canvas>();
            ManagedObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }
}

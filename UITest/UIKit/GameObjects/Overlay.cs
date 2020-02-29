﻿using UnityEngine;
using UnityEngine.UI;

namespace UIKit.GameObjects
{
    public class Overlay : Container
    {
        public Canvas Canvas => Get<Canvas>();

        public override void Create(bool active = true)
        {
            base.Create(active);

            Get<GraphicRaycaster>();

            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }
}

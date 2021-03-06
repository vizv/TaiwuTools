﻿// This file is part of the TaiwuTools <https://github.com/vizv/TaiwuTools/>.
// Copyright (C) 2020  Taiwu Modding Community Members
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Collections.Generic;
using UnityUIKit.Core;
using UnityUIKit.GameObjects;
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

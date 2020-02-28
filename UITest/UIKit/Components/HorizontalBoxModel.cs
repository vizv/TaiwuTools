using UnityEngine.UI;

namespace UIKit.Components
{
    public class HorizontalBoxModel : BoxModel {
        public override HorizontalOrVerticalLayoutGroup LayoutGroup => Get<HorizontalLayoutGroup>();
    }
}

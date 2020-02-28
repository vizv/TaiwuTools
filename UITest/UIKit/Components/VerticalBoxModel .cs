using UnityEngine.UI;

namespace UIKit.Components
{
    public class VerticalBoxModel : BoxModel
    {
        public override HorizontalOrVerticalLayoutGroup LayoutGroup => Get<VerticalLayoutGroup>();
    }

}

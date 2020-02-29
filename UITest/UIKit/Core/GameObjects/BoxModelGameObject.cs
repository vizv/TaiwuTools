using UIKit.Components;
using UnityEngine.UI;

namespace UIKit.Core.GameObjects
{
    public class BoxModelGameObject : ManagedGameObject
    {
        public BoxModel BoxModel => Get<BoxModel>();
        public HorizontalOrVerticalLayoutGroup LayoutGroup => BoxModel.LayoutGroup;

        public new Arguments Default;
        public BoxModelGameObject() : this(new Arguments()) { }
        public BoxModelGameObject(Arguments arguments) : base(arguments) => Default = arguments;

        public override void Create()
        {
            base.Create();

            BoxModel.Apply(Default.BoxModel);
        }

        public new class Arguments : ManagedGameObject.Arguments
        {
            public BoxModel.Arguments BoxModel = new BoxModel.Arguments();
        }
    }
}

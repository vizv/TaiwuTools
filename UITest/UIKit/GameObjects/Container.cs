using UIKit.Components;
using UIKit.Core;

namespace UIKit.GameObjects
{
    public class Container : ManagedGameObject
    {
        public new Arguments Default;
        public Container() : this(new Arguments()) { }
        public Container(Arguments arguments) : base(arguments) => Default = arguments;

        public override void Create()
        {
            base.Create();

            var boxModel = Get<BoxModel>();
            boxModel.Apply(Default.BoxModel);
        }

        public new class Arguments : ManagedGameObject.Arguments
        {
            public BoxModel.Arguments BoxModel;
        }
    }
}

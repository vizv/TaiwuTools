namespace UIKit.Core
{
    interface IManagedComponent<A> where A : Attributes
    {
        void Apply(A arguments);
    }
}

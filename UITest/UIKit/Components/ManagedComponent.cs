using System;
using UIKit.Core;
using UnityEngine;

namespace UIKit.Components
{
    public abstract class ManagedComponent : MonoBehaviour
    {
        protected GameObject ManagedObject => gameObject;

        // TODO: change to mixin???
        public T Get<T>() where T : Component => ManagedObject.GetComponent<T>() ?? ManagedObject.AddComponent<T>();
        public Component Get(Type type) => ManagedObject.GetComponent(type) ?? ManagedObject.AddComponent(type);

        public virtual void Apply(Arguments arguments)
        {
            throw new NotImplementedException();
        }

        public abstract class Arguments : Attributes { }
    }
}

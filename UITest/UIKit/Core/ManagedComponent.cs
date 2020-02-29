using System;
using UnityEngine;

namespace UIKit.Core
{
    public abstract class ManagedComponent : MonoBehaviour, IManagedObject
    {
        // IManagedObject
        public GameObject GameObject => gameObject;
        public T Get<T>() where T : Component => GameObject.GetComponent<T>() ?? GameObject.AddComponent<T>();
        public Component Get(Type type) => GameObject.GetComponent(type) ?? GameObject.AddComponent(type);

        public virtual void Apply(ComponentAttributes componentAttributes) { }

        public abstract class ComponentAttributes : Core.ComponentAttributes { }
    }
}

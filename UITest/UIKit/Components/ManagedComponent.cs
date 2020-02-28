﻿using System;
using UIKit.Core;
using UnityEngine;

namespace UIKit.Components
{
    public abstract class ManagedComponent<A> : MonoBehaviour, IManagedComponent<A> where A : Attributes
    {
        protected GameObject ManagedObject => gameObject;

        // TODO: change to mixin???
        protected T Get<T>() where T : Component => ManagedObject.GetComponent<T>() ?? ManagedObject.AddComponent<T>();
        protected Component Get(Type type) => ManagedObject.GetComponent(type) ?? ManagedObject.AddComponent(type);

        public virtual void Apply(A arguments)
        {
            throw new NotImplementedException();
        }

        public abstract class Arguments : Attributes { }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIKit.Core
{
    public abstract class ManagedGameObject
    {
        // internal GameObject name
        private string name = null;
        // FIXME: Add SerializableField Tag
        public string Name
        {
            set
            {
                name = value;
                if (managedObject) managedObject.name = name;
            }
            get
            {
                if (name == null) name = $"Unnammed UIKit GameObject <{GetType().FullName}>";
                return name;
            }
        }

        // FIXME: Add SerializableField Tag
        public Dictionary<Type, ManagedComponent.Arguments> Components = new Dictionary<Type, ManagedComponent.Arguments>();

        // internal GameObject FIXME: rename
        private GameObject managedObject;
        public GameObject ManagedObject
        {
            get
            {
                if (!managedObject) Create();
                return managedObject;
            }
        }

        // TODO: change to mixin???
        public T Get<T>() where T : Component => ManagedObject.GetComponent<T>() ?? ManagedObject.AddComponent<T>();
        public Component Get(Type type) => ManagedObject.GetComponent(type) ?? ManagedObject.AddComponent(type);

        public bool Created => !Destroyed;
        public bool Destroyed => !managedObject;
        public bool IsActive => ManagedObject.activeSelf;
        public RectTransform RectTransform => Get<RectTransform>();

        public void SetParent(ManagedGameObject managedGameObject, bool worldPositionStays = false) => SetParent(managedGameObject.ManagedObject, worldPositionStays);
        public void SetParent(GameObject gameObject, bool worldPositionStays = false) => SetParent(gameObject.transform, worldPositionStays);
        public void SetParent(Transform transform, bool worldPositionStays = false) => ManagedObject.transform.SetParent(transform, worldPositionStays);

        public void SetActive(bool value) => ManagedObject.SetActive(value);

        public T AddComponent<T>() where T : Component => ManagedObject.AddComponent<T>();

        public virtual void Create(bool active = true)
        {
            if (managedObject) return;
            managedObject = new GameObject(name);
            managedObject.SetActive(active);

            foreach (var componentPair in Components)
            {
                var component = ManagedObject.AddComponent(componentPair.Key) as ManagedComponent;
                if (!component) continue;

                component.Apply(componentPair.Value);
            }
        }

        public virtual void Destroy()
        {
            if (!managedObject) return;
            UnityEngine.Object.Destroy(managedObject);
            managedObject = null;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using UIKit.Core;
using UnityEngine;

namespace UIKit.GameObjects
{
    public abstract class ManagedGameObject : ManagedObject<GameObject>
    {
        public GameObject ManagedObject
        {
            get
            {
                if (!managedObject) Create();
                return managedObject;
            }
        }

        // TODO: change to mixin???
        protected T Get<T>() where T : Component => ManagedObject.GetComponent<T>() ?? ManagedObject.AddComponent<T>();
        protected Component Get(Type type) => ManagedObject.GetComponent(type) ?? ManagedObject.AddComponent(type);

        public bool Created => !Destroyed;
        public bool Destroyed => !managedObject;
        public bool IsActive => ManagedObject.activeSelf;
        public RectTransform RectTransform => Get<RectTransform>();
        public string Name => ManagedObject.name;

        public Arguments Default;
        protected ManagedGameObject(Arguments arguments) => Default = arguments;

        public void SetParent(ManagedGameObject managedGameObject, bool worldPositionStays = false) => SetParent(managedGameObject.ManagedObject, worldPositionStays);
        public void SetParent(GameObject gameObject, bool worldPositionStays = false) => SetParent(gameObject.transform, worldPositionStays);
        public void SetParent(Transform transform, bool worldPositionStays = false) => ManagedObject.transform.SetParent(transform, worldPositionStays);

        public void SetActive(bool value) => ManagedObject.SetActive(value);

        public T AddComponent<T>() where T : Component => ManagedObject.AddComponent<T>();

        public virtual void Create()
        {
            if (managedObject) return;
            managedObject = new GameObject(Default.Name);
            managedObject.SetActive(Default.Active);
        }

        public virtual void Destroy()
        {
            if (!managedObject) return;
            UnityEngine.Object.Destroy(managedObject);
            managedObject = null;
        }

        public class Arguments : Attributes
        {
            public string Name = "UntitledUIKitGameObject";
            public bool Active = true;

            //public Dictionary<C, C>
        }
    }
}

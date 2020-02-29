﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIKit.Core
{
    public abstract class ManagedGameObject : IManagedObject
    {
        // FIXME: Add SerializableField Tag
        private string name = null;
        public string Name
        {
            set
            {
                name = value;
                if (gameObject) gameObject.name = name;
            }
            get
            {
                if (name == null) name = $"Unnammed UIKit GameObject <{GetType().FullName}>";
                return name;
            }
        }

        // FIXME: Add SerializableField Tag
        public Dictionary<Type, ManagedComponent.ComponentAttributes> Components = new Dictionary<Type, ManagedComponent.ComponentAttributes>();

        // FIXME: Add SerializableField Tag
        public List<ManagedGameObject> Children = new List<ManagedGameObject>();

        // IManagedObject
        private GameObject gameObject;
        public GameObject GameObject
        {
            get
            {
                if (!gameObject) Create();
                return gameObject;
            }
        }
        public T Get<T>() where T : Component => GameObject.GetComponent<T>() ?? GameObject.AddComponent<T>();
        public Component Get(Type type) => GameObject.GetComponent(type) ?? GameObject.AddComponent(type);

        public bool Created => !Destroyed;
        public bool Destroyed => !gameObject;
        public bool IsActive => GameObject.activeSelf;
        public RectTransform RectTransform => Get<RectTransform>();

        public void SetParent(ManagedGameObject managedGameObject, bool worldPositionStays = false) => SetParent(managedGameObject.GameObject, worldPositionStays);
        public void SetParent(GameObject gameObject, bool worldPositionStays = false) => SetParent(gameObject.transform, worldPositionStays);
        public void SetParent(Transform transform, bool worldPositionStays = false) => GameObject.transform.SetParent(transform, worldPositionStays);

        public void SetActive(bool value) => GameObject.SetActive(value);

        public T AddComponent<T>() where T : Component => GameObject.AddComponent<T>();

        public virtual void Create(bool active = true)
        {
            if (gameObject) return;
            gameObject = new GameObject(name);
            gameObject.SetActive(active);

            foreach (var componentPair in Components)
            {
                var component = GameObject.AddComponent(componentPair.Key) as ManagedComponent;
                if (!component) continue;

                component.Apply(componentPair.Value);
            }

            foreach (var child in Children) child.SetParent(this);
        }

        public virtual void Destroy(bool destroyChild = true)
        {
            if (!gameObject) return;

            if (!destroyChild) gameObject.transform.DetachChildren();

            UnityEngine.Object.Destroy(gameObject);
            gameObject = null;
        }
    }
}

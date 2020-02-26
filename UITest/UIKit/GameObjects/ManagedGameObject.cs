using System;
using UIKit.Core;
using UnityEngine;

namespace UIKit.GameObjects
{
    public abstract class ManagedGameObject : ManagedObject<GameObject>
    {
        public GameObject ManagedObject => managedObject;
        public bool IsActive => managedObject.activeSelf;
        public RectTransform RectTransform => managedObject.GetComponent<RectTransform>();


        protected ManagedGameObject(string name)
        {
            managedObject = new GameObject(name);
        }

        public void SetParent(ManagedGameObject managedGameObject, bool worldPositionStays = false) => SetParent(managedGameObject.ManagedObject, worldPositionStays);
        public void SetParent(GameObject gameObject, bool worldPositionStays = false) => SetParent(gameObject.transform, worldPositionStays);
        public void SetParent(Transform transform, bool worldPositionStays = false) => managedObject.transform.SetParent(transform, worldPositionStays);

        public void SetActive(bool value) => managedObject.SetActive(value);

        //public abstract static T Create<T>(params object[] args) where T : GameObject;
        //{
        //    if (typeof(T) == typeof(GameObject))
        //    {
        //        // TODO: throw Invalid Argument Exception
        //        return null;
        //    }

        //    //Main.Logger.Log(string.Join(",", args.Select(arg => arg.ToString())));
        //    //Main.Logger.Log(args.ToString());
        //    return typeof(T).GetMethod("Create", BindingFlags.Public | BindingFlags.Static).Invoke(null, args) as T;
        //}

        public T AddComponent<T>() where T : Component
        {
            return managedObject.AddComponent<T>();
        }
    }
}

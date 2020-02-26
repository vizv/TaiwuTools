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

        public bool Created => !Destroyed;
        public bool Destroyed => !managedObject;
        public bool IsActive => ManagedObject.activeSelf;
        public RectTransform RectTransform => ManagedObject.GetComponent<RectTransform>();
        public string Name => ManagedObject.name;

        public Arguments DefaultArguments;
        protected ManagedGameObject(Arguments arguments) => DefaultArguments = arguments;

        public void SetParent(ManagedGameObject managedGameObject, bool worldPositionStays = false) => SetParent(managedGameObject.ManagedObject, worldPositionStays);
        public void SetParent(GameObject gameObject, bool worldPositionStays = false) => SetParent(gameObject.transform, worldPositionStays);
        public void SetParent(Transform transform, bool worldPositionStays = false) => ManagedObject.transform.SetParent(transform, worldPositionStays);

        public void SetActive(bool value) => ManagedObject.SetActive(value);

        public T AddComponent<T>() where T : Component => ManagedObject.AddComponent<T>();

        public virtual void Create()
        {
            if (managedObject) return;
            managedObject = new GameObject(DefaultArguments.Name);
            managedObject.SetActive(DefaultArguments.Active);
        }

        public virtual void Destroy()
        {
            if (!managedObject) return;
            Object.Destroy(managedObject);
            managedObject = null;
        }

        public class Arguments
        {
            public string Name = "UntitledUIKitGameObject";
            public bool Active = true;
        }
    }
}

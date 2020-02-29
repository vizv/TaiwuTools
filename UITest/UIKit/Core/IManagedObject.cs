using System;
using UnityEngine;

namespace UIKit.Core
{
    interface IManagedObject
    {
        GameObject GameObject { get; }

        T Get<T>() where T : Component;
        Component Get(Type type);
    }
}

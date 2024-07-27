using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lib
{
    public static class MyTransformExtensions
    {
        public static void ForEachSelfChildren<T>(this Transform transform, Action<T> callback)
        {
            foreach (Transform child in transform)
            foreach (var script in child.GetComponents<T>())
                callback.Invoke(script);
        }
    }
}
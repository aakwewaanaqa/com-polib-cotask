using System;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    public class CoTaskRunner : MonoBehaviour
    {
        private static readonly Lazy<CoTaskRunner> lazy = new(() =>
        {
            var go = new GameObject("CoTaskRunner");
            DontDestroyOnLoad(go);
            return go.AddComponent<CoTaskRunner>();
        });

        public static CoTaskRunner Sigleton => lazy.Value;
    }
}
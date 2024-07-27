using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Tasks
{
    public class TaskUnityEventForSceneAction : MonoBehaviour, IMyTask
    {
        [SerializeField] private UnityEvent _event;

        public bool InProgress => false;

        public void Begin(Action onComplete = null)
        {
            _event.Invoke();
            onComplete?.Invoke();
        }
    }
}
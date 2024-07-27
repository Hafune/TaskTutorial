using System;
using UnityEngine;

namespace Core.Lib
{
    public class TaskLog : MonoBehaviour, IMyTask
    {
        [SerializeField] private string _message;

        public bool InProgress => false;

        public void Begin(Action onComplete = null)
        {
            Debug.Log(_message);
            onComplete?.Invoke();
        }
    }
}
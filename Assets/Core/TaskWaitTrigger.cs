using System;
using UnityEngine;

namespace Core.Tasks
{
    [ExecuteInEditMode]
    public class TaskWaitTrigger : MonoBehaviour, IMyTask
    {
        [SerializeField] private CheckPoint _target;

        private Action _onComplete;

        public bool InProgress { get; private set; }

        public void Begin(Action onComplete = null)
        {
            if (InProgress)
                throw new Exception("Новый вызов таски до её завершения");
            
            _onComplete = onComplete;
            InProgress = true;
            _target.OnEnter += OnEnter;
        }

        private void OnEnter()
        {
            _target.OnEnter -= OnEnter;
            InProgress = false;
            _onComplete?.Invoke();
        }
    }
}
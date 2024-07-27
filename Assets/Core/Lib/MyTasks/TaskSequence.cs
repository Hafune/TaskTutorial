using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskSequence : MonoBehaviour, IMyTask
    {
        [SerializeField] [Min(1)] internal int _repeatCount = 1;
        [SerializeField] private bool _runOnStart;

        private List<IMyTask> _tasks = new();
        private int _completeCount;
        private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake()
        {
            for (int i = 0; i < _repeatCount; i++)
                transform.ForEachSelfChildren<IMyTask>(_tasks.Add);
        }

        private void Start()
        {
            if (_runOnStart)
                Begin();
        }

        public void Begin(Action onComplete = null)
        {
            if (InProgress)
                throw new Exception("Новый вызов таски до её завершения");

            InProgress = true;
            _onComplete = onComplete;
            _completeCount = -1;

            Next();
        }

        private void Next()
        {
            if (_tasks.Count > ++_completeCount)
                _tasks[_completeCount].Begin(Next);
            else
            {
                InProgress = false;
                _onComplete?.Invoke();
            }
        }
    }
}
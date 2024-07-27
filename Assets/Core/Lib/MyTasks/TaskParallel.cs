using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskParallel : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _runOnStart;

        private IMyTask[] _tasks;
        private int _completedCount;

        [CanBeNull] private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake()
        {
            List<IMyTask> iTasks = new(transform.childCount);
            transform.ForEachSelfChildren<IMyTask>(iTasks.Add);
            _tasks = iTasks.ToArray();
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
            _completedCount = 0;

            for (int i = 0, iMax = _tasks.Length; i < iMax; i++)
                _tasks[i].Begin(Complete);
        }

        private void Complete()
        {
            if (++_completedCount < _tasks.Length)
                return;

            InProgress = false;
            _onComplete?.Invoke();
        }
    }
}
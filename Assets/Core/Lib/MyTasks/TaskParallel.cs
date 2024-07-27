﻿using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskParallel : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _runOnStart;

        private List<IMyTask> _tasks;
        private int _completedCount;
        private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake()
        {
            _tasks = new();
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
            _completedCount = 0;

            for (int i = 0, iMax = _tasks.Count; i < iMax; i++)
                _tasks[i].Begin(Complete);
        }

        private void Complete()
        {
            if (++_completedCount < _tasks.Count)
                return;

            InProgress = false;
            _onComplete?.Invoke();
        }
    }
}
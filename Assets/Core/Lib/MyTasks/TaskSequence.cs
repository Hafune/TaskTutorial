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

        private IMyTask[] _tasks;
        private int _index;
        private Action _onComplete;

        public bool InProgress { get; private set; }

        private void Awake()
        {
            List<IMyTask> iTasks = new(transform.childCount * _repeatCount);
            
            for (int i = 0; i < _repeatCount; i++)
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
            _index = -1;

            Next();
        }

        private void Next()
        {
            if (_tasks.Length > ++_index)
                _tasks[_index].Begin(Next);
            else
            {
                InProgress = false;
                _onComplete?.Invoke();
            }
        }
    }
}
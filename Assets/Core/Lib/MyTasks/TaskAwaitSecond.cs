﻿using System;
using System.Collections;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskAwaitSecond : MonoBehaviour, IMyTask
    {
        [SerializeField] [Min(0)] private float _time;
        private WaitForSeconds _wait;

        public bool InProgress { get; private set; }

        private void Awake() => _wait = new WaitForSeconds(_time);

        public void Begin(Action onComplete = null)
            => StartCoroutine(StartSpawn(onComplete));

        private IEnumerator StartSpawn(Action onComplete)
        {
            InProgress = true;
            yield return _wait;
            InProgress = false;

            onComplete?.Invoke();
        }
    }
}
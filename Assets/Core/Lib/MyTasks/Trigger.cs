using JetBrains.Annotations;
using UnityEngine;

namespace Core.Lib
{
    public class Trigger : MonoBehaviour, ITrigger
    {
        [CanBeNull, SerializeField] private MonoBehaviour _task;
        private bool _isCompleted;

        private void OnValidate()
        {
            if (_task is not IMyTask)
                _task = null;

            _task = _task ? _task : (MonoBehaviour)GetComponentInChildren<IMyTask>();

            if (_task == this)
                _task = null;
        }

        private void OnEnable() => _isCompleted = false;

        private void OnTriggerEnter(Collider _)
        {
            if (_isCompleted)
                return;

            _isCompleted = true;

            if (_task)
                (_task as IMyTask)?.Begin();
        }
    }
}
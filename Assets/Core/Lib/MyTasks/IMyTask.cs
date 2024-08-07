﻿using System;
using Reflex;

namespace Core
{
    public interface IMyTask
    {
        public bool InProgress { get; }

        public void Begin(Action onComplete = null);
    }
}
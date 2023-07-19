﻿using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class Destroyable : MonoBehaviour
    {
        public event Action Destroyed;

        private void OnDestroy() => 
            Destroyed?.Invoke();
    }
}
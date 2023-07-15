using System;
using UnityEngine;

namespace Infrastructure.Progress.Handling.IslandLevel
{
    [Serializable]
    public class IslandWorldProgress
    {
        public bool IsEmpty = true;
        
        public Vector3 HeroPosition;
        public Quaternion HeroRotation;
    }
}
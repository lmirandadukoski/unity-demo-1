using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemo1
{
    [CreateAssetMenu(menuName = "UnityDemo1/Levels/Level Data", fileName = "[LEVELID]_LevelData")]
    public class LevelData : ScriptableObject
    {
        public long Index { get => _Index; }
        public Grid<int> Layout { get => _Layout; }

        [SerializeField, Min(0)] private long _Index;
        [SerializeField] private Grid<int> _Layout;
    }
}


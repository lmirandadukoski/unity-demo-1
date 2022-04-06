using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemo1
{
    [CreateAssetMenu(menuName = "UnityDemo1/Levels/Level Data", fileName = "[LEVELID]_LevelData")]
    public class LevelData : ScriptableObject
    {
        public long Index { get => _Index; }
        public Texture2D LevelLayout { get => _LevelLayout; }

        [SerializeField, Min(0)] private long _Index;
        [SerializeField] private Texture2D _LevelLayout;
    }
}


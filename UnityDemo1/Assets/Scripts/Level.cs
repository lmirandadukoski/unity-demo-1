using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemo1
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelData _Data;
        [SerializeField] private Collider _GroundCollider;

        [SerializeField] private Grid<int> _CurrentGrid;

        private void Start()
        {
            CreateLevel();    
        }

        private void OnDrawGizmos()
        {
            if(_CurrentGrid != null)
            {
                Gizmos.color = Color.green;

                if(_CurrentGrid.Cells != null)
                {
                    for (int i = 0; i < _CurrentGrid.Cells.Length; i++)
                    {
                        Bounds cellArea = _CurrentGrid.Cells[i].Area;
                        Gizmos.DrawWireCube(cellArea.center, cellArea.size);
                    }
                }
            }
        }

        public void Initialise(LevelData data)
        {
            if(data != null)
            {
                _Data = data;
            }
        }
        
        private void CreateLevel()
        {
            if(_Data != null)
            {
                LoadLevel(_Data.Layout);
            }
        }

        private void LoadLevel(Grid<int> grid)
        {
            _CurrentGrid = new Grid<int>(grid.Dimensions, _GroundCollider.bounds);
        }

        private void SaveLevel(Grid<int> grid)
        {

        }
    }
}

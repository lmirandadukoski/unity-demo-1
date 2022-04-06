using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UnityDemo1
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelData _Data;
        [SerializeField] private Collider _GroundCollider;

        [SerializeField] private Grid<int> _CurrentGrid;

        private static Color _InvalidCellColour = new Color(0, 0, 0);

        private void Start()
        {
            CreateLevel();    
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Vector3.zero), 1f);

            if(_CurrentGrid != null)
            {
                if(_CurrentGrid.Cells != null)
                {
                    for (int x = 0; x < _CurrentGrid.Dimensions.x; x++)
                    {
                        for (int y = 0; y < _CurrentGrid.Dimensions.y; y++)
                        {
                            var cell = _CurrentGrid.Cells[x, y];
                            Gizmos.color = cell.IsValid ? Color.green : Color.red;

                            if (cell.IsValid)
                            {
                                Gizmos.DrawWireCube(cell.Area.center, cell.Area.size);
                            }
                            else
                            {
                                Gizmos.DrawCube(cell.Area.center, cell.Area.size);
                            }

#if UNITY_EDITOR
                            Handles.Label(cell.Area.center + Vector3.up * 0.5f, string.Format("x{0}, y{1}", cell.Coordinates.x, cell.Coordinates.y));
#endif
                        }
                    }
                }
            }
        }
        
        private void CreateLevel()
        {
            if(_Data != null)
            {
                Vector2Int gridDimensions = new Vector2Int(_Data.LevelLayout.width, _Data.LevelLayout.height);
                bool[,] cellValidityValues = new bool[gridDimensions.x, gridDimensions.y];

                for (int x = 0; x < gridDimensions.x; x++)
                {
                    for (int y = 0; y < gridDimensions.y; y++)
                    {
                        Color cellColour = _Data.LevelLayout.GetPixel(x, y);
                        cellValidityValues[x, y] = IsValidCell(cellColour);

                        Debug.Log(string.Format("x{0}, y{1} = {2} {3}", x, y, cellColour, cellValidityValues[x, y]));
                    }
                }

                _CurrentGrid = new Grid<int>(gridDimensions, _GroundCollider.bounds, cellValidityValues);
            }
        }

        private void LoadLevel(long levelIndex)
        {
            
        }

        private void SaveLevel(long levelIndex)
        {

        }

        public bool IsValidCell(Color cellColour)
        {
            return cellColour != _InvalidCellColour;
        }
    }
}

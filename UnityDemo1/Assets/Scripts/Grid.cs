using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDemo1
{
    [System.Serializable]
    public class Grid<T>
    {
        public Vector2Int Dimensions { get => _Dimensions; }
        public Cell[] Cells { get => _Cells; }

        [SerializeField] private Vector2Int _Dimensions;
        [SerializeField] private Cell[] _Cells;

        [System.Serializable]
        public class Cell
        {
            public Vector2Int Coordinates { get => _Coordinates; }
            public Bounds Area { get => _Area; }

            [SerializeField] private Vector2Int _Coordinates;

            private Bounds _Area;
            private T _Object;

            public Cell(Vector2Int coordinates, Bounds area)
            {
                if (CanInitialise(coordinates, area))
                {
                    _Coordinates = coordinates;
                    _Area = area;
                }
            }

            private bool CanInitialise(Vector2Int coordinates, Bounds area)
            {
                bool dimensionsAreValid = coordinates.x >= 0 && coordinates.y >= 0;
                bool areaIsValid = area.size.x > 0.0f && area.size.z > 0.0f;

                return dimensionsAreValid && areaIsValid;
            }
        }

        public Grid(Vector2Int dimensions, Bounds area)
        {
            if(CanInitialise(dimensions, area))
            {
                _Dimensions = dimensions;
                Vector3 cellDimensions = CalculateCellDimensions(area);

                _Cells = new Cell[_Dimensions.x * _Dimensions.y];

                for (int y = 0; y < _Dimensions.y; y++)
                {
                    for (int x = 0; x < _Dimensions.x; x++)
                    {
                        Vector2Int cellCoordinates = new Vector2Int(x, y);
                        int cellArrayIndex = CalculateCellArrayIndex(cellCoordinates);
                        Vector3 cellPosition = CalculateCellPosition(area, cellCoordinates, cellDimensions);
                        _Cells[cellArrayIndex] = new Cell(cellCoordinates, new Bounds(cellPosition, cellDimensions));
                    }
                }
            }
        }

        private bool CanInitialise(Vector2Int dimensions, Bounds area)
        {
            bool dimensionsAreValid = dimensions.x >= 1 && dimensions.y >= 1;
            bool areaIsValid = area.size.x > 0.0f && area.size.z > 0.0f;

            return dimensionsAreValid && areaIsValid;
        }

        private Vector3 CalculateCellDimensions(Bounds area)
        {
            float cellWidth = area.size.x / _Dimensions.x;
            float cellHeight = area.size.z / _Dimensions.y;

            return new Vector3(cellWidth, area.size.y, cellHeight);
        }

        private int CalculateCellArrayIndex(Vector2Int cellCoordinates)
        {
            return (cellCoordinates.y * _Dimensions.x) + cellCoordinates.x;
        }

        private Vector3 CalculateCellPosition(Bounds area, Vector2Int cellCoordinates, Vector3 cellDimensions)
        {
            return area.min + new Vector3(cellCoordinates.x * cellDimensions.x, 0.0f, cellCoordinates.y * cellDimensions.z) + new Vector3(cellDimensions.x, 0.0f, cellDimensions.z) * 0.5f;
        }

        //private Vector3 CalculateCellPosition(int x, int y, float cellWidth, float cellHeight)
        //{
        //    return m_Bounds.min + new Vector3(y * cellWidth, 0.0f, x * cellHeight) + new Vector3(cellWidth, 0.0f, cellHeight) * 0.5f;
        //}
    }
}


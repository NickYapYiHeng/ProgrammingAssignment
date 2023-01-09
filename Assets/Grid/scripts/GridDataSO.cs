using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridData")]
[System.Serializable]
public class GridDataSO : ScriptableObject
{
    public int width;
    public int height;
    public float gridSize;
    public bool[] IsBlocked;

    public void SaveObsticleArray(bool[,] array)
    {
        IsBlocked = Set2DArrayTo1D(array);
    }

    public bool[,] LoadObsticleArray()
    {
        return Set1DArrayTo2D(IsBlocked) ;
    }

    public bool[] Set2DArrayTo1D(bool[,] array)
    {
        bool[] result = new bool[width * height];

        for(int x = 0; x < array.GetLength(0); x++)
        {
            for(int y = 0; y < array.GetLength(1); y++)
            {
                result[array.GetLength(0) * x + y] = array[x, y];
            }
        }

        return result;
    }

    public bool[,] Set1DArrayTo2D(bool[] array)
    {
        bool[,] result = new bool[width,height];

        for (int x = 0; x < result.GetLength(0); x++)
        {
            for (int y = 0; y < result.GetLength(1); y++)
            {
                result[x,y] = array[result.GetLength(0) * x + y];
            }
        }

        return result;
    }

}

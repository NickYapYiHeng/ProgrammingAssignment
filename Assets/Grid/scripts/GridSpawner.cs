using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.VFX;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] public GridDataSO _gridDataSO;
    private int[,] gridArray;
    public GameObject[,] TileSpawned;
    public string GridCoord;

    public GameObject GridPrefab;
    public GameObject Wall;

    private void Start()
    {
        gridArray = new int[_gridDataSO.width, _gridDataSO.height];
        TileSpawned = new GameObject[_gridDataSO.width, _gridDataSO.height];


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
                GameObject tileObj = (GameObject)Instantiate(GridPrefab, transform);


                float posX = x * _gridDataSO.gridSize;
                float posY = y * -_gridDataSO.gridSize;

                tileObj.transform.position = new Vector3(posX, 0, posY);
                tileObj.GetComponent<Tile>().width = x; 
                tileObj.GetComponent<Tile>().height = y;
                tileObj.name = $"X : {x} Y: {y}";
                TileSpawned[x,y] = tileObj;
                GridCoord = x.ToString() + y.ToString();
                
                IsBlockedCheck(x,y);

            }
        }

    }   

    public void IsBlockedCheck( int ObjWidth, int ObjHeight)
    {
        int num = Int32.Parse(GridCoord);
        var temp = TileSpawned[ObjWidth,ObjHeight];

        if (_gridDataSO.IsBlocked[num] == true)
        {
            
            Instantiate(Wall, temp.transform);

            Wall.transform.position = new Vector3(0, 1, 0);
        }
    }



}

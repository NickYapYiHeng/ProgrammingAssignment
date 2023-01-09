using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] public bool findDistance;
    public GridSpawner _gridSpawner;
    public GridDataSO _gridDataSO;
    public int startX;
    public int startY;
    public int endX;
    public int endY;
    public List<GameObject> path = new List<GameObject>();

    public void Update()
    {
        if(findDistance)
        {
            SetDistance();
            SetPath();
            findDistance= false;
        }
    }

    public void InitialSetup()
    {


        foreach (GameObject obj in _gridSpawner.TileSpawned)
        {
            obj.GetComponent<Tile>().visited = -1;
        }
        _gridSpawner.TileSpawned[startX, startY].GetComponent<Tile>().visited = 0;
    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if(y+1<_gridDataSO.width && _gridSpawner.TileSpawned[x,y+1] && _gridSpawner.TileSpawned[x, y+1].GetComponent<Tile>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (y - 1 > -1 && _gridSpawner.TileSpawned[x, y - 1] && _gridSpawner.TileSpawned[x, y - 1].GetComponent<Tile>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (x + 1 < _gridDataSO.height && _gridSpawner.TileSpawned[x + 1, y] && _gridSpawner.TileSpawned[x + 1, y].GetComponent<Tile>().visited == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && _gridSpawner.TileSpawned[x - 1, y] && _gridSpawner.TileSpawned[x - 1, y].GetComponent<Tile>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;

    }

    public void SetVisited(int x, int y, int step)
    {
        if (_gridSpawner.TileSpawned[x,y])
            _gridSpawner.TileSpawned[x,y].GetComponent<Tile>().visited = step;
    }

    public void SetDistance()
    {
        InitialSetup();
        int x = startX;
        int y = startY;
        int[] testArray = new int[_gridDataSO.width* _gridDataSO.height];
        for(int step =1; step< _gridDataSO.width * _gridDataSO.height; step++ )
        {
            foreach(GameObject obj in _gridSpawner.TileSpawned)
            {
                if(obj && obj.GetComponent<Tile>().visited == step-1)
                    TestFourDirections(obj.GetComponent<Tile>().width, obj.GetComponent<Tile>().height, step);
            }
        }
    }

    public void TestFourDirections(int x, int y, int step)
    {
        if(TestDirection(x, y, -1, 1))
            SetVisited(x, y + 1, step);
        if (TestDirection(x, y, -1, 2))
            SetVisited(x, y - 1, step);
        if (TestDirection(x, y, -1, 3))
            SetVisited(x + 1, y, step);
        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);
    }

    public void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (_gridSpawner.TileSpawned[endX,endY] && _gridSpawner.TileSpawned[endX, endY].GetComponent<Tile>().visited > 0)
        {
            path.Add(_gridSpawner.TileSpawned[x, y]);
            step = _gridSpawner.TileSpawned[x, y].GetComponent<Tile>().visited - 1;
        }
        else
        {
            print("Cant Find End Location");
            return;
        }
        for(int i = step; step > -1; step--)
        {
            if (TestDirection(x, y, step, 1))
                tempList.Add(_gridSpawner.TileSpawned[x, y + 1]);
            if (TestDirection(x, y, step, 2))
                tempList.Add(_gridSpawner.TileSpawned[x, y - 1]);
            if (TestDirection(x, y, step, 3))
                tempList.Add(_gridSpawner.TileSpawned[x + 1, y]);
            if (TestDirection(x, y, step, 4))
                tempList.Add(_gridSpawner.TileSpawned[x - 1, y]);
            GameObject tempObj = FindClosest(_gridSpawner.TileSpawned[endX, endY].transform, tempList);
            path.Add(tempObj);
            x = tempObj.GetComponent<Tile>().width;
            y = tempObj.GetComponent<Tile>().height;
            tempList.Clear();
        }
    }

    public GameObject FindClosest(Transform targetLocation, List<GameObject> List)
    {
        float currentDistance = _gridDataSO.gridSize*_gridDataSO.width*_gridDataSO.height;
        int indexNumber = 0;
        for(int i = 0; i<List.Count; i++)
        {
            if(Vector3.Distance(targetLocation.position, List[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, List[i].transform.position);
                indexNumber = i;
            }
        }
        return List[indexNumber];
    }
}



using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GridDataSO))]
public class GridDataCustomEditor :Editor
{
    GridDataSO _gridDataSO;
    bool[,] IsBlocked;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _gridDataSO = (GridDataSO)target;

        if(IsBlocked == null)
        {
           IsBlocked = _gridDataSO.LoadObsticleArray() != null ? _gridDataSO.LoadObsticleArray() : new bool[_gridDataSO.width - 1, _gridDataSO.height - 1];
        }

        GUILayout.BeginHorizontal("Box");

        if(_gridDataSO.width >  0 && _gridDataSO.height > 0)
        {
            
            for(int x = 0; x<_gridDataSO.width; x++)
            {
                GUILayout.BeginVertical("Box");
                for (int y = 0; y<_gridDataSO.height; y++)
                {
                    IsBlocked[x, y] = GUILayout.Toggle(IsBlocked[x,y], $"{x} : {y}", "Button") ;
                }
                GUILayout.EndVertical();
            }
            
        }
        GUILayout.EndHorizontal();

        if(GUILayout.Button("SAVE"))
        {
            EditorUtility.SetDirty(_gridDataSO);
            _gridDataSO.SaveObsticleArray(IsBlocked);
        }
    }
}

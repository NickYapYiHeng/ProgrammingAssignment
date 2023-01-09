using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIGridCoord : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI Text;
    private static GUIGridCoord instance;
    public static GUIGridCoord Instance { get { return instance; } }


    public void Awake()
    {
        instance = this; 
    }
    public void UpdateHUDText(string GridName)
    {
        Text.text = GridName;
    }
}

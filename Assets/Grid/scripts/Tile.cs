using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public Renderer renderer;
    public int width;
    public int height;
    public int visited;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        renderer.material.color = Color.red;
        
        GUIGridCoord.Instance.UpdateHUDText(name);


    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

}

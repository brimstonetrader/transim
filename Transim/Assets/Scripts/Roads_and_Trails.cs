using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This is a refrence to Drag and Drop in Unity - 2021 Tutorial 
// https://www.youtube.com/watch?v=Tv82HIvKcZQ

public class Roads_and_Trails : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;
    public Tilemap tilemap;
    public Tile road;
    public Tile trail;

    void Awake() 
    {
        _cam = Camera.main;
    }

    void onMouseDown() 
    {
        _dragOffset = transform.position - GetMousePos();
        if(Input.GetKey(KeyCode.Mouse0))
         {
             tilemap.SetTile(Intizer(GetMousePos()), road);   
         }
         if(Input.GetKey(KeyCode.Mouse1))
         {
             tilemap.SetTile(Intizer(GetMousePos()), trail);   
         }
    }
    
    void OnMouseDrag() 
    {
        transform.position = GetMousePos() + _dragOffset;
    }

    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    Vector3Int Intizer(Vector3 floatnasty) {
        float someFloat = 42.7f;
        int xi = (int)Math.Round(floatnasty.x); 
        int yi = (int)Math.Round(floatnasty.y); 
        int zi = (int)Math.Round(floatnasty.z); 
        return (new Vector3Int (xi,yi,zi));
    }
}

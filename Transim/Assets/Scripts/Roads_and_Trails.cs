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
    public TileBase road;
    public TileBase trail;

    void Awake() 
    {
        _cam = Camera.main;
    }

    void Update()
    {
    if(Input.GetMouseButtonDown(0))
        {
        print(Intizer(GetMousePos()));
        tilemap.SetTile(Intizer(GetMousePos()), road);   
        }
    if(Input.GetMouseButtonDown(1))
        {
        print(Intizer(GetMousePos()));
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
        int xi = (int)Math.Round(floatnasty.x * 136/65); 
        int yi = (int)Math.Round(floatnasty.y * 92/45); 
        int zi = (int)Math.Round(floatnasty.z); 
        return (new Vector3Int (xi,yi,zi));
    }
}

using System.Numerics;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This is a refrence to Drag and Drop in Unity - 2021 Tutorial 
// https://www.youtube.com/watch?v=Tv82HIvKcZQ

public class Roads_and_Trails : MonoBehaviour
{
    private UnityEngine.Vector3 _dragOffset;
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
        Vector3Int stwp = GridAligner(GetMousePos());
        tilemap.SetTile(stwp, road);   
        tilemap.SetTile(new Vector3Int (stwp.x - 1, stwp.y, 0), road);   
        tilemap.SetTile(new Vector3Int (stwp.x, stwp.y - 1, 0), road);   
        tilemap.SetTile(new Vector3Int (stwp.x - 1, stwp.y - 1, 0), road);   
    }
    if(Input.GetMouseButtonDown(1))
        {
        Vector3Int stwp = GridAligner(GetMousePos());
        tilemap.SetTile(stwp, trail);   
        tilemap.SetTile(new Vector3Int (stwp.x - 1, stwp.y, 0), trail);   
        tilemap.SetTile(new Vector3Int (stwp.x, stwp.y - 1, 0), trail);   
        tilemap.SetTile(new Vector3Int (stwp.x - 1, stwp.y - 1, 0), trail);   
        }
    }

    void ToggleLineSuite(Vector3Int stwp, TileBase tile) {
        while(!Input.GetMouseButtonDown(1) || !Input.GetMouseButtonDown(0)) {
        Vector3Int stwp2 = GridAligner(GetMousePos());
        tilemap.SetTile(new Vector3Int (stwp2.x - 1, stwp2.y, 0), tile);   
        tilemap.SetTile(new Vector3Int (stwp2.x, stwp2.y - 1, 0), tile);   
        tilemap.SetTile(new Vector3Int (stwp2.x - 1, stwp2.y - 1, 0), tile);           
        }
    }
    
    void OnMouseDrag() 
    {
        transform.position = GetMousePos() + _dragOffset;
    }

    UnityEngine.Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    Vector3Int GridAligner(UnityEngine.Vector3 pos) {
        int xmin  = -50;
        int xmax  = 15;
        int ymin  = -30;
        int ymax  = 15;
        int gxmin  = -67;
        int gxmax  = 68;
        int gymin  = -54;
        int gymax  = 37;
        int xi = (int)Math.Round(((pos.x - xmin / (xmax-xmin)) * (gxmax-gxmin)) + gxmin); 
        int yi = (int)Math.Round(((pos.y - ymin / (ymax-ymin)) * (gymax-gymin)) + gymin); 
        int zi = (int)Math.Round(pos.z); 
        return (new Vector3Int (xi,yi,zi));
    }
}



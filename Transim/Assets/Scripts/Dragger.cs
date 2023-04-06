using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a refrence to Drag and Drop in Unity - 2021 Tutorial 
// https://www.youtube.com/watch?v=Tv82HIvKcZQ

public class Dragger: MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;

    void Awake() 
    {
        _cam = Camera.main;
    }

    void onMouseDown() 
    {
        _dragOffset = transform.position - GetMousePos();
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
}

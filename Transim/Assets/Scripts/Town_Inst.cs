using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Inst : MonoBehaviour
{
    int gxmin  = -67;
    int gxmax  = 68;
    int gymin  = -54;
    int gymax  = 37;
    public Tile Home;
    public Tile Work;
    public Tile Vibe;        

    void Start() {
        SetGrid();
    }

    public void SetGrid() {
        int total = (gxmax-gxmin) * (gymax-gymin);
        List<int> randcoords = new List<int>() {UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax),UnityEngine.Random.Range(gxmin, gxmax),UnityEngine.Random.Range(gymin, gymax)};
        for(int i = 0; i < total; i++) {
            int posX = (int)Mathf.Floor(i/(gxmax-gxmin));
            int posY = i % (gymax-gymin);


        }
    }
}

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    private Renderer m_Renderer;


    private void Awake() {
        this.m_Renderer = this.GetComponent<Renderer>();
    }

    public void SetPosition(int x, int y) {
        this.X = x;
        this.Y = y;

        this.transform.position = new Vector3(x, y, 0);
    }
    public void SetColor(Color c) {
        m_Renderer.material.color = c;
    }
}
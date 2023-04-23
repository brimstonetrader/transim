using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Inst : MonoBehaviour
{
    public int gridX = 67;
    public int gridY = 46;
    public Tile tilePrefab;

    void Start() {
        SetGrid();
    }

    public void SetGrid() {
        int total = gridX * gridY;

        for(int i = 0; i < total; i++) {
            int posX = (int)Mathf.Floor(i/gridX);
            int posY = i % gridY;

            Tile t = Instantiate(tilePrefab).GetComponent<Tile>();
            t.SetPosition(posX, posY);

            if((posX + posY) % 2 == 0)
                t.SetColor(Color.blue);
            else
                t.SetColor(Color.red);
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
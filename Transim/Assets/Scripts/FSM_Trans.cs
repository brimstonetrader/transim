using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Trans : FSM_Human
{
    [HideInInspector]
    public Flux fluxState;
    [HideInInspector]
    public Home homeState;
    [HideInInspector]
    public Vibe vibeState;
    [HideInInspector]
    public Work workState;

    public float speed = 4f;

    private void Awake()
    {
        fluxState = new Flux(this);
        homeState = new Home(this);
        vibeState = new Vibe(this);
        workState = new Work(this);        
    }

    protected override BaseState GetInitialState()
    {
        return homeState;
    }
}
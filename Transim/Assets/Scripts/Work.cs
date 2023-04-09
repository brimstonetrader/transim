using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : BaseState
{
    private FSM_Trans _sm;

    public Work(FSM_Trans stateMachine) : base("Work", stateMachine) {
      _sm = stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.black;
    }
}
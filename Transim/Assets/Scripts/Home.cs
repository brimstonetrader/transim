using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : BaseState
{
    private FSM_Trans _sm;
    public float time;

    public Home(FSM_Trans stateMachine) : base("Home", stateMachine) {
      _sm = stateMachine;
    }

    
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        time = 0;
        if ((time % 360) == 120) {
            stateMachine.ChangeState(_sm.fluxState);
        }
    }
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.black;
    }
}

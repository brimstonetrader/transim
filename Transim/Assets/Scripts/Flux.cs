using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flux : BaseState
{
  private FSM_Trans _sm;

  public Flux(FSM_Trans stateMachine) : base("Flux", stateMachine) {
    _sm = stateMachine;
  }

  public override void UpdateLogic()
  {
    base.UpdateLogic();
    if (0 == 2) {
      stateMachine.ChangeState(_sm.vibeState);
    }
  }

  public override void UpdatePhysics()
  {
    base.UpdatePhysics();
    Vector2 vel = _sm.rigidbody.velocity;
    vel.x = 2 * _sm.speed;
    _sm.rigidbody.velocity = vel;
  }

  public override void Enter()
  {
    base.Enter();
    _sm.spriteRenderer.color = Color.blue;
  }

}

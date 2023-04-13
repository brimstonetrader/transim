using System.Net.WebSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Given our analog clock, we will scale a day into six minutes. 
//Big ups to https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134

public class FSM_Human : MonoBehaviour
{
    public Rigidbody rigidbody;
    public SpriteRenderer spriteRenderer;
    BaseState currentState;

    public float duration = 5;
    public int c = 0;
    private int r;
    public string state = "vibe";


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
        StartCoroutine(Switch());
    }


    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    IEnumerator Switch() {
        print("yes");
        r = UnityEngine.Random.Range(1, 4);
        if (r <= 1) {
            ChangeState(new BaseState("Home", this));
            yield return new WaitForSeconds(10f);
        }
        else if (r <= 2) {
            ChangeState(new BaseState("Vibe", this));
            yield return new WaitForSeconds(10f);
        }
        else {
            ChangeState(new BaseState("Work", this));
            yield return new WaitForSeconds(10f);
        }
        StartCoroutine(Switch());
    }

    public void ChangeState(BaseState newState)
    {
        if (newState.name == "Home") {
            StartCoroutine(LerpPosition(new Vector2(0,1), new BaseState("Home", this)));
        }
        if (newState.name == "Work") {
            StartCoroutine(LerpPosition(new Vector2(1,0), new BaseState("Work", this)));
        }
        if (newState.name == "Vibe") {
            StartCoroutine(LerpPosition(new Vector2(1,-1), new BaseState("Vibe", this)));
        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition, BaseState newState)
    {
        float time = 0;
        Vector2 startPosition = transform.position;
        currentState.Exit();
        currentState = new BaseState("Flux", this);
        state = "Flux";
        spriteRenderer.color = Color.black;
        currentState.Enter();
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        currentState.Exit();
        currentState = new BaseState(newState.name, this);
        state = newState.name;
        if (state=="Work") {spriteRenderer.color = Color.red;}
        if (state=="Home") {spriteRenderer.color = Color.blue;}
        if (state=="Vibe") {spriteRenderer.color = Color.yellow;}        
        currentState.Enter();

        
    }


    protected virtual BaseState GetInitialState()
    {
        return new BaseState("Vibe", this);
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}



public class BaseState
{
    public string name;
    protected FSM_Human stateMachine;

    public BaseState(string name, FSM_Human stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}

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
    _sm.spriteRenderer.color = Color.black;
  }

}

public class Vibe : BaseState
{
    private FSM_Trans _sm;

    public Vibe(FSM_Trans stateMachine) : base("Vibe", stateMachine) {
      _sm = stateMachine;
    }    
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.green;
    }
}

public class Work : BaseState
{
    private FSM_Trans _sm;

    public Work(FSM_Trans stateMachine) : base("Work", stateMachine) {
      _sm = stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.red;
    }
}

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
        _sm.spriteRenderer.color = Color.blue;
    }
}

using System.Linq.Expressions;
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
    public TimeClock clock;
    BaseState currentState;

    public float duration = 5;
    private int time = 0;
    private int day = 360;
    public int c = 0;
    private int r;
    public string state = "Home";
    public Vector2 homeLoc;
    public Vector2 vibeLoc;
    public Vector2 workLoc;
    bool worked = false;
    bool homed = false;
    bool vibed = false;
    int vtoh;
    int htow;
    int wtovorh;


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
        workLoc = new Vector2 (UnityEngine.Random.Range(-25f,20f), UnityEngine.Random.Range(-25f,20f));
        vibeLoc = new Vector2 (UnityEngine.Random.Range(-25f,20f), UnityEngine.Random.Range(-25f,20f));
        homeLoc = new Vector2 (UnityEngine.Random.Range(-25f,20f), UnityEngine.Random.Range(-25f,20f));
        DailyUpdate();
        }


    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
        time = clock.GetTimer() % 360;
        if (time == 0) {
            DailyUpdate();
        }
        
        if ((time > htow) && (state != "Work") && (!worked)) {
            worked = true;
            ChangeState(new BaseState("Work", this));
        }
        else if ((time > wtovorh+htow) && (state != "Vibe") && (!vibed)) {
            vibed = true;
            ChangeState(new BaseState("Vibe", this));
        }
        else if ((time > vtoh+wtovorh+htow) && (state != "Home") && (!homed)) {
            homed = true;
            ChangeState(new BaseState("Home", this));
        }

        
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public IEnumerator DailyUpdate() {           
        htow     = UnityEngine.Random.Range(110, 130);
        wtovorh  = UnityEngine.Random.Range(230, 250);
        vtoh     = UnityEngine.Random.Range(260, 280);
        worked   = false;
        homed    = false;
        vibed    = false;
        yield return new WaitForSeconds(0.1f);
        }

    public void ChangeState(BaseState newState)
    {
        if (newState.name == "Home") {
            StartCoroutine(LerpPosition(homeLoc, new BaseState("Home", this)));
        }
        if (newState.name == "Work") {
            StartCoroutine(LerpPosition(workLoc, new BaseState("Work", this)));
        }
        if (newState.name == "Vibe") {
            StartCoroutine(LerpPosition(vibeLoc, new BaseState("Vibe", this)));
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
        return new BaseState("Flux", this);
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
    stateMachine.ChangeState(_sm.vibeState);
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
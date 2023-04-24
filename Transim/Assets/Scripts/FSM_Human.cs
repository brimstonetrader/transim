using System.Linq.Expressions;
using System.Net.WebSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Given our analog clock, we will scale a day into six minutes. 
//Big ups to https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134

public class FSM_Human : MonoBehaviour
{
    public Rigidbody rigidbody;
    public SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    public TimeClock clock;
    BaseState currentState;

    public float lerpduration = 5;
    public int timer = 0;
    public int days = 0;
    private int daylength = 360;
    public int c = 0;
    private int r;
    public string state = "Home";
    public Vector3 homeLoc;
    public Vector3 vibeLoc;
    public Vector3 workLoc;
    bool worked = false;
    bool homed = false;
    bool vibed = false;
    int vtoh=100;
    int htow=200;
    int wtovorh=300;


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
        DailyUpdate();
        Timekeeper();
        }

    IEnumerator Timekeeper() {
        if (timer > daylength-1) {
            DailyUpdate();
        }
        timer = clock.GetTimer() % daylength;
        yield return new WaitForSeconds(0.5f);
        Timekeeper();
        
    }


    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
        if ((timer > htow) && (!worked)) {
            worked = true;
            state = "Work";
            ChangeState(new BaseState("Work", this));
        }
        else if ((timer > wtovorh+htow) &&(!vibed) && worked){
            vibed = true;
            state = "Vibe";
            ChangeState(new BaseState("Vibe", this));
        }
        else if ((timer > vtoh+wtovorh+htow) && (!homed) && worked && vibed) {
            homed = true;
            state = "Home";
            ChangeState(new BaseState("Home", this));
        }

        
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public IEnumerator DailyUpdate() {           
        days++;
        htow     = UnityEngine.Random.Range(110, 130);
        wtovorh  = UnityEngine.Random.Range(230, 250);
        vtoh     = UnityEngine.Random.Range(260, 280);
        worked   = false;
        homed    = false;
        vibed    = true;
        if (UnityEngine.Random.Range(0, 100) < 70) {vibed = false;}
                                                        
        yield return new WaitForSeconds(0.1f);
        }

    public void ChangeState(BaseState newState)
    {
        if      (newState.name == "Home") {agent.destination = homeLoc;}
        else if (newState.name == "Work") {agent.destination = workLoc;}
        else if (newState.name == "Vibe") {agent.destination = vibeLoc;}
        
        currentState.Exit();
        currentState = new BaseState("Flux", this);
        state = "Flux";
        spriteRenderer.color = Color.black;
        currentState.Enter();
        
        currentState.Exit();
        currentState = new BaseState(newState.name, this);
        state = newState.name;
        if      (state=="Work") {spriteRenderer.color = Color.red;}
        else if (state=="Home") {spriteRenderer.color = Color.blue;}
        else if (state=="Vibe") {spriteRenderer.color = Color.green;}        
        currentState.Enter();
   }


    protected virtual BaseState GetInitialState()
    {
        return new BaseState("Flux", this);
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name + "time: " + timer.ToString() + " days: " + days.ToString(): "";
        //string content = currentState != null ? currentState.name + "time: " + timer.ToString() + " days: " + days.ToString();
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

    public Home(FSM_Trans stateMachine) : base("Home", stateMachine) {
      _sm = stateMachine;
    }

    
    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.blue;
    }
}
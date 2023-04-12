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
    private float r = 0;


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
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

    public IEnumerator Switch() {
        r = UnityEngine.Random.Range(1, 3);
        if (r == 1) {
            ChangeState(new BaseState("Home", this));
            yield return new WaitForSeconds(0.1f);
        }
        else if (r == 2) {
            ChangeState(new BaseState("Work", this));
            yield return new WaitForSeconds(0.1f);
        }
        else {
            ChangeState(new BaseState("Vibe", this));
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (newState.name == "Home") {
            LerpPosition(new Vector2(1,0), new BaseState("Home", this));
        }
        if (newState.name == "Work") {
            LerpPosition(new Vector2(0,1), new BaseState("Work", this));
        }
        if (newState.name == "Vibe") {
            LerpPosition(new Vector2(1,1), new BaseState("Vibe", this));
        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition, BaseState newState)
    {
        float time = 0;
        Vector2 startPosition = transform.position;
        currentState.Exit();
        currentState = new BaseState("Flux", this);
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
        currentState.Enter();
        
    }


    protected virtual BaseState GetInitialState()
    {
        return null;
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
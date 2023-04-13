using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This was a reference to Making an analog clock or stopwatch
// - including speeding up time with Unity https://www.youtube.com/watch?v=hNTpi-pkh7w

public class TimeClock : MonoBehaviour
{

    public Image imageSecondHand;
    public Image imageMinuteHand;
    public Image imageHourHand;

    public Button speedUp;
    public Button slowDown;
    public Button normal;

    private bool isTimer = false;
    public float timer = 0.0f;
    private float timerSpeed = 100.0f;
    [SerializeField]
    private int days = 0;
    private int r = 0;

    public FSM_Human fsm;



    // Start is called before the first frame update
    void Start()
    {
        isTimer = true;
        DisplayTime();
        print("Creating timer");
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimer)
        {
            timer += Time.deltaTime * timerSpeed;
            DisplayTime();
            print("Timer Speed" + timerSpeed);
        }

    }

    void DisplayTime() 
    {
        if(timer >= 60.0f * 60.0f * 24.0f)
        {
            timer -= 60.0f * 60.0f *24.0f;
            days++;
        }
        int hours = Mathf.FloorToInt(timer / (60.0f * 60.0f));
        int minutes = Mathf.FloorToInt(timer / 60.0f - hours * 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60 - hours * 60.0f * 60.0f);

        if (hours > 12)
            hours -= 12;

        imageHourHand.transform.localEulerAngles = new Vector3(0,0,hours / 12.0f * -360.0f);
        imageMinuteHand.transform.localEulerAngles = new Vector3(0,0,minutes / 60.0f * -360.0f);
        imageSecondHand.transform.localEulerAngles = new Vector3(0,0,seconds / 60.0f * -360.0f);
    }


    public void SpeedingUp()
    {
        timerSpeed *= 2.0f;
        print("Speeding UP");
    }

    public void SlowingDown()
    {
        timerSpeed *= 0.5f;
        print("Slowing DOWN");
    }

    public void Normal()
    {
        print("Normal");
    }

}

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

    private bool isTimer = false;
    private float timer = 0.0f;
    private float timerSpeed = 1.0f;
    [SerializeField]
    private int days = 0;



    // Start is called before the first frame update
    void Start()
    {
        isTimer = true;
        DisplayTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimer)
        {
            timer += Time.deltaTime * timerSpeed;
            DisplayTime();
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
        timerSpeed = 3.0f;
    }

    public void SlowingDown()
    {
        timerSpeed = 0.25f;
    }

}

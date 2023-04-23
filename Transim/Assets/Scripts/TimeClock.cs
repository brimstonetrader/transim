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
    public float timer = 0f;
    public float timerSpeed = 1f;
    [SerializeField]
    private int days = 0;
    private int r = 0;


    // Start is called before the first frame update
    void Start()
    {
        isTimer = true;
        DisplayTime();
        print("Creating timer");
    }

    // Update is called once per frame
    void Update()
    {    if(isTimer)
        {
            timer += Time.deltaTime * timerSpeed;
            DisplayTime();
            print("Timer Speed" + timerSpeed);
        }}


    void DisplayTime() 
    {
        if(timer >= 60 * 60 * 24)
        {
            timer -= 60 * 60 *24;
            days++;
        }
        int hours = Mathf.FloorToInt((float)timer / (60.0f * 60.0f));
        int minutes = Mathf.FloorToInt((float)timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer);
        if (hours > 12)
            hours -= 12;

        imageHourHand.transform.localEulerAngles = new Vector3(0,0,hours / 12 * -360);
        imageMinuteHand.transform.localEulerAngles = new Vector3(0,0,minutes / 60 * -360);
        imageSecondHand.transform.localEulerAngles = new Vector3(0,0,seconds / 60 * -360);
    }


    public void SpeedingUp()
    {
        timerSpeed *= 2f;
        print("Speeding UP");
    }

    public int GetTimer() {
        return Mathf.FloorToInt(timer);
    }

    public void SlowingDown()
    {
        timerSpeed *= 0.5f;
        print("Slowing DOWN");
    }
}

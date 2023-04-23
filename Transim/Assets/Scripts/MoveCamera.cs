using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.UpArrow)
        || Input.GetKeyDown(KeyCode.DownArrow)
        || Input.GetKeyDown(KeyCode.LeftArrow)
        || Input.GetKeyDown(KeyCode.RightArrow))
            Camera.main.transform.Translate (Input.GetAxisRaw("Horizontal")*10,Input.GetAxisRaw("Vertical")*10, 0); 
    }
    
    
    IEnumerator LerpPosition(Vector2 targetPosition, BaseState newState)
    {
        float time = 0;
        float duration = 1;
        Vector2 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        
    }

}

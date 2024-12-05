using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NpcMovement : MonoBehaviour
{
    public float speed = 10.0f;
    int phase = -1;
    public float waitTime = 1.0f;
    private bool newcustomer = false;
    private bool satisfied = false;
    bool access = true;
    public float rotateTime = 2.0f;
    public Vector3 targetPosition;
    public Vector3 returnPosition;
    public Vector3 currentPosition;
    void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y , transform.position.z + 35);
        returnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    void Update()
    {
        currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if(Input.GetKey(KeyCode.Y)) //debug
        {
            satisfied = true;
        }

        if (Input.GetKey(KeyCode.U)) //debug
        {
            newcustomer = true;
        }

        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else if (access == true)
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
            newcustomer = false;
            satisfied = false;
            phase = 0;
            access = false;
        }

        if (phase == 0)
        { //eka pätkä alusta oven eteen
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (currentPosition == targetPosition && satisfied == true)
            {
                phase = 1;
                rotateTime = 2.0f;
                satisfied = false;
            }
        }
        else if (phase == 1)
        { //käännös
            Debug.Log("ijdijdsa");
            transform.Rotate(0, Time.deltaTime * -45, 0);
            rotateTime -= Time.deltaTime;
            if (rotateTime <= 0.0f)
            {
                targetPosition = new Vector3(transform.position.x - 24, transform.position.y, transform.position.z);
                rotateTime = 2.0f;
                phase = 2;
            }
        }
        else if (phase == 2)
        { // liikke tuolille
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                if(currentPosition == targetPosition && newcustomer == true)
                {
                transform.Rotate(0, Time.deltaTime * +45, 0);
                rotateTime -= Time.deltaTime;
                if (rotateTime <= 0.0f)
                 {
                    waitTime = 1.0f;
                    transform.position = returnPosition;
                    phase = -1;
                    access = true;
                    newcustomer = false;
                 }
                }  
        }
    }
}
/*
 *         else if (phase == 1)
        { //käännös
            Debug.Log("ijdijdsa");
            transform.Rotate(0, Time.deltaTime * -45, 0);
            rotateTime -= Time.deltaTime;
            if (rotateTime <= 0.0f)
            {
                targetPosition = new Vector3(transform.position.x - 12, transform.position.y, transform.position.z);
                rotateTime etc etc
                phase = 2;
            }
        }
        else if (phase == 2)
        { // liikke tuolille
            if (rotateTime > 0.0f)
            {
                transform.Rotate(0, Time.deltaTime * +45, 0);
                rotateTime -= Time.deltaTime;
            }
            else if (rotateTime <= 0.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                if(currentPosition == targetPosition)
                {
                    phase = 3;
                }*/

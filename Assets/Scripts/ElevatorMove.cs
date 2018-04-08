using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SloanKelly.GameLib;

public class ElevatorMove : MonoBehaviour {

    public float duration = 3;
    public int currentFloor = 0;
    int destinationFloor = 1;
    bool[] upFloor = new bool[] { false, false, false, false, false, false };
    bool[] downFloor = new bool[] { false, false, false, false, false, false } ;
    public bool movingUp = true;
    bool liftIdle;
    bool doorOpen = false;
    string[] tags = new string[] { "Ground", "1", "2", "3", "4", "5" };
    static bool running = false;
    public GameObject[] buttonUp;
    public GameObject[] buttonDown;
    public GameObject[] displayLight;
    public GameObject player;
    int[] pos = new int[] { 0, 50, 100, 150, 200, 250 };
    Vector3 startPosition;
    Vector3 endPosition;
    Animator anim;

    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        anim = GetComponentInChildren<Animator>();
    }
    //
    void Update()
    {
        liftIdle = isIdle();

        if(noUpRequest() && !noDownRequest())
        {
            movingUp = false;
        }

        else if(noDownRequest() && !noUpRequest())
        {
            movingUp = true;
        }

        else if(!noUpRequest())
        {
            movingUp = false;
        }
        else if(!noDownRequest())
        {
            movingUp = true;
        }

        if(movingUp)
        {
            MoveUp();
        }

        else
        {
            MoveDown();
        }

        for(int i = 0; i < upFloor.Length; i++)
        {
            if(i == currentFloor)
            {
                displayLight[currentFloor].GetComponent<Light>().color = Color.blue;
            }

            else if(upFloor[i] || downFloor[i])
            {
                displayLight[i].GetComponent<Light>().color = Color.white;
            }

            else
            {
                displayLight[i].GetComponent<Light>().color = Color.clear;
            }

        }

        for(int i = 0; i < upFloor.Length; i++)
        {
            if(buttonUp[i].GetComponent<Renderer>().material.color == Color.blue)
            {
                upFloor[i] = true;
            }
            else if(buttonDown[i].GetComponent<Renderer>().material.color == Color.blue)
            {
                downFloor[i] = true;
            }
        }
    }

    bool isIdle()
    {
        if (noUpRequest() && noDownRequest())
            return true;
        else return false;
    }

    bool noUpRequest()
    {
        if ((!upFloor[0] && !upFloor[1] && !upFloor[2] &&
            !upFloor[3] && !upFloor[4] && !upFloor[5]))
            return true;
        else return false;
    }

    bool noDownRequest()
    {
        if (!downFloor[0] && !downFloor[1] && !downFloor[2] &&
            !downFloor[3] && !downFloor[4] && !downFloor[5])
            return true;
        else return false;
    }

    bool moveUpRequest()
    {
        for(int i = 0; i < upFloor.Length; i++)
        {
            if (upFloor[i]) return true;
        }
        return false;
    }

    bool moveDownRequest()
    {
        for (int i = 0; i < upFloor.Length; i++)
        {
            if (downFloor[i]) return true;
        }
        return false;
    }

    void DoSomething()
    {
        print("Delay");
    }

    void OnTriggerStay(Collider col)
    {
        if (Input.GetKey(KeyCode.Alpha0) && currentFloor == 0)
        {
            upFloor[0] = true;
        }

        if (Input.GetKey(KeyCode.Alpha1) && currentFloor < 1)
        {
            upFloor[1] = true;
        }

        if(Input.GetKey(KeyCode.Alpha2) && currentFloor < 2)
        {
            upFloor[2] = true;
        }

        if (Input.GetKey(KeyCode.Alpha3) && currentFloor < 3)
        {
            upFloor[3] = true;
        }

        if (Input.GetKey(KeyCode.Alpha4) && currentFloor < 4)
        {
            upFloor[4] = true;
        }

        if (Input.GetKey(KeyCode.Alpha5) && currentFloor < 5)
        {
            upFloor[5] = true;
        }

        if (Input.GetKey(KeyCode.Alpha0) && currentFloor > 0)
        {
            downFloor[0] = true;
        }

        if (Input.GetKey(KeyCode.Alpha1) && currentFloor > 1)
        {
            downFloor[1] = true;
        }

        if (Input.GetKey(KeyCode.Alpha2) && currentFloor > 2)
        {
            downFloor[2] = true;
        }

        if (Input.GetKey(KeyCode.Alpha3) && currentFloor > 3)
        {
            downFloor[3] = true;
        }

        if (Input.GetKey(KeyCode.Alpha4) && currentFloor > 4)
        {
            downFloor[4] = true;
        }

        if (Input.GetKey(KeyCode.Alpha5) && currentFloor > 5)
        {
            downFloor[5] = true;
        }

        if(Input.GetKey(KeyCode.C))
        {
            DoorControl("Close");
            doorOpen = false;
        }
        if(Input.GetKey(KeyCode.O))
        {
            DoorControl("Open");
            doorOpen = true;
        }
    }

    void MoveUp()
    {
        for(int i = currentFloor; i < upFloor.Length; i++)
        {
            if (doorOpen)
                return;
            if(upFloor[i])
            {
                upFloor[i] = false;
                startPosition = transform.position;
                endPosition = new Vector3(transform.position.x, pos[i], transform.position.z);
                buttonUp[i].GetComponent<Renderer>().material.color = Color.clear;
                StartCoroutine(MoveElevator(i, currentFloor));
                DoorControl("Open");
                doorOpen = true;
                currentFloor = i;
            }
        }
        if(isIdle())
        {
            liftIdle = true;
        }
        if (currentFloor == 5)
        {
            movingUp = false;
            liftIdle = true;
        }
    }



    void MoveDown()
    {
        for(int i = currentFloor; i >= 0; i--)
        {
            if(downFloor[i])
            {
                downFloor[i] = false;
                startPosition = transform.position;
                endPosition = new Vector3(transform.position.x, pos[i], transform.position.z);
                buttonDown[i].GetComponent<Renderer>().material.color = Color.clear;
                StartCoroutine(MoveElevator(i, currentFloor));
                DoorControl("Open");
                doorOpen = true;
                currentFloor = i;
            }
        }
        if(currentFloor == 0)
        {
            movingUp = true;
            liftIdle = true;
        }
    }

    private IEnumerator MoveElevator(int i, int currentFloor)
    {
        return CoroutineFactory.Create(Mathf.Abs(i - currentFloor) * duration, time =>
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
        });
    }

    void OnIdle()
    {
        for(int i = 0; i < upFloor.Length; i++)
        {
            if (doorOpen)
                return;
            if (downFloor[i])
            {
                if(i < currentFloor)
                {
                    liftIdle = false;
                    movingUp = false;
                    MoveDown();
                    break;
                }

                else
                {
                    liftIdle = false;
                    movingUp = true;
                    MoveUp();
                    break;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(doorOpen)
        {
            DoorControl("Close");
            doorOpen = false;
        }
    }

    void DoorControl(string direction)
    {
        anim.SetTrigger(direction);
    }
}

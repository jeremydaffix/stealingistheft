﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class MovingCustomerController : MonoBehaviour
{
    WaypointsTraveler wpt;

    bool isMoving = false;
    bool isColliding = false;


    // Start is called before the first frame update
    void Start()
    {
        wpt = GetComponent<WaypointsTraveler>();


        StartCoroutine("RandomStop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomStop()
    {
        do
        {
            while (isColliding)
                yield return new WaitForEndOfFrame();

            int walkPauseTime = Random.Range(2, 12);
            wpt.Move(isMoving);
            yield return new WaitForSeconds(walkPauseTime);

            isMoving = !isMoving;

        } while (true);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;

        wpt.Move(false);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;

        wpt.Move(isMoving);
    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }
}
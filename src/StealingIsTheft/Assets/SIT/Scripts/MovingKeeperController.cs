using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class MovingKeeperController : MonoBehaviour
{
    WaypointsTraveler wpt;
    Animator anim;

    bool isMoving = false;
    bool isColliding = false;


    void Start()
    {
        wpt = GetComponent<WaypointsTraveler>();
        anim = GetComponent<Animator>();


        StartCoroutine("RandomStop");
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        bool detected = CheckForPlayer();

        if(detected)
        {

        }
    }

    bool CheckForPlayer()
    {
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.up, 15f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0.5f), 15f, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0.5f), 15f, layerMask);

        //Debug.Log(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position));


        if ((hit1.collider != null && hit1.collider.name == "Player") ||
            (hit2.collider != null && hit2.collider.name == "Player") ||
            (hit3.collider != null && hit3.collider.name == "Player"))
        {
            return true;
        }

        else if(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position) < 2.0f)
        {
            return true;
        }

        return false;
    }


    IEnumerator RandomStop()
    {
        do
        {
            while (isColliding)
                yield return new WaitForEndOfFrame();

            int walkPauseTime = Random.Range(1, 6);
            wpt.Move(isMoving);
            anim.SetBool("isWalking", isMoving);
            yield return new WaitForSeconds(walkPauseTime);

            isMoving = !isMoving;

        } while (true);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;

        wpt.Move(false);
        anim.SetBool("isWalking", false);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;

        wpt.Move(isMoving);
        anim.SetBool("isWalking", isMoving);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}

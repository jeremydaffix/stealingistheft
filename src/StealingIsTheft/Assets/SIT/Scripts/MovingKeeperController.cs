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

    bool playerChope = false;


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

        if(detected && !playerChope)
        {
            if(PlayerController.PlayerInstance.IsNaked || PlayerController.PlayerInstance.IsDrunk || PlayerController.PlayerInstance.IsStealing)
            {
                playerChope = true;

                if (PlayerController.PlayerInstance.IsStealing) Say("Thief, respect my authority!");
                else if (PlayerController.PlayerInstance.IsNaked) Say("Hey you, why are you naked?");
                else if (PlayerController.PlayerInstance.IsDrunk) Say("Sir, are you drunk?");

                wpt.enabled = false;

                Feedback.Instance.GameOver();
            }
        }
    }

    bool CheckForPlayer()
    {
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.up, 12f, layerMask);
        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0.5f), 5f, layerMask);
        //RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0.5f), 5f, layerMask);
        Debug.Log(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position));
        if ((hit1.collider != null && hit1.collider.name == "Player") /*||
            (hit2.collider != null && hit2.collider.name == "Player") ||
            (hit3.collider != null && hit3.collider.name == "Player")*/)
        {
            return true;
        }

        else if(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position) < 4.0f)
        {
            return true;
        }

        return false;
    }


    public void Say(string msg)
    {
        Feedback.Instance.ShowSimpleMessage(new Vector2(transform.position.x, transform.position.y + 2f), msg, new Color(0.5f, 0.5f, 1f), 22, 0.75f);
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

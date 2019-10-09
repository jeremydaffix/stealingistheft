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
            ChopePlayer();
        }
    }

    bool CheckForPlayer()
    {
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.up, 11f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, (Quaternion.AngleAxis(-30f, Vector3.forward) * transform.up.normalized).normalized, 6f, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, (Quaternion.AngleAxis(30f, Vector3.forward) * transform.up.normalized).normalized, 6f, layerMask);

        //Debug.Log(transform.up + " =   " + (Quaternion.AngleAxis(-35f, Vector3.forward) * transform.up.normalized).normalized + "    " + (Quaternion.AngleAxis(35f, Vector3.forward) * transform.up.normalized).normalized);

        //Debug.Log(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position));

        if ((hit1.collider != null && hit1.collider.name == "Player") ||
            (hit2.collider != null && hit2.collider.name == "Player") ||
            (hit3.collider != null && hit3.collider.name == "Player"))
        {
            return true;
        }

        /*else if(Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position) < 2.5f)
        {
            return true;
        }*/

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

            int walkPauseTime = Random.Range(1, 4);
            wpt.Move(isMoving);
            anim.SetBool("isWalking", isMoving);
            yield return new WaitForSeconds(isMoving ? walkPauseTime * 2f : walkPauseTime);

            isMoving = !isMoving;

        } while (true);
    }


    void ChopePlayer()
    {
        if (PlayerController.PlayerInstance.IsNaked || PlayerController.PlayerInstance.IsDrunk || PlayerController.PlayerInstance.IsStealing)
        {
            playerChope = true;

            if (PlayerController.PlayerInstance.IsStealing) Say("Thief, respect my authority!");
            else if (PlayerController.PlayerInstance.IsNaked) Say("Hey you, why are you naked?");
            else if (PlayerController.PlayerInstance.IsDrunk) Say("Sir, are you drunk?");

            wpt.enabled = false;
            isMoving = false;
            anim.SetBool("isWalking", isMoving);

            Vector3 dir = PlayerController.PlayerInstance.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = (Quaternion.AngleAxis(angle - 90f, Vector3.forward));

            SoundSystem.inst.PlayKeeper();

            Feedback.Instance.GameOver(transform);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;

        wpt.Move(false);
        anim.SetBool("isWalking", false);

        if(collision.gameObject.name == "Player")
        {
            if (!playerChope)
            {
                ChopePlayer();
            }
        }
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

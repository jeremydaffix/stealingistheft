using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class MovingCustomerController : MonoBehaviour
{
    WaypointsTraveler wpt;
    Animator anim;

    bool isMoving = false;
    bool isColliding = false;
    float lastSay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        wpt = GetComponent<WaypointsTraveler>();
        anim = GetComponent<Animator>();


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

            int walkPauseTime = Random.Range(1, 4);
            wpt.Move(isMoving);
            anim.SetBool("isWalking", isMoving);
            yield return new WaitForSeconds(isMoving ? walkPauseTime * 2f : walkPauseTime);

            isMoving = !isMoving;

        } while (true);
    }


    public void Say(string msg)
    {
        Feedback.Instance.ShowSimpleMessage(new Vector2(transform.position.x, transform.position.y + 2f), msg, new Color(0.5f, 0.5f, 0.5f), 22, 0.75f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isColliding = true;

            wpt.Move(false);
            anim.SetBool("isWalking", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isColliding = false;

            wpt.Move(isMoving);
            anim.SetBool("isWalking", isMoving);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if ((Time.fixedTime - lastSay) > 5.0f)
            {
                int randomSpeech = Random.Range(0, 3);

                if (randomSpeech == 0) Say("Are you in a hurry, young man?");
                else if (randomSpeech == 1) Say("I miss the good old times!");
                else if (randomSpeech == 2) Say("Don't push me!");

                lastSay = Time.fixedTime;
            }
        }
    }
}

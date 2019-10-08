using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedKeeperController : MonoBehaviour
{
    bool isColliding = false;
    bool playerChope = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool detected = CheckForPlayer();

        if (detected && !playerChope)
        {
            if (PlayerController.PlayerInstance.IsNaked || PlayerController.PlayerInstance.IsDrunk || PlayerController.PlayerInstance.IsStealing ||
                !PlayerController.PlayerInstance.HasSunglasses || !PlayerController.PlayerInstance.HasPincers || PlayerController.PlayerInstance.IsWearingAntiTheft)
            {
                playerChope = true;

                if (PlayerController.PlayerInstance.IsStealing)
                    Say("Thief, respect my authority!");

                else if (PlayerController.PlayerInstance.IsNaked)
                    Say("Hey you, why are you naked?");

                else if (PlayerController.PlayerInstance.IsDrunk)
                    Say("Sir, are you drunk?");


                else if (!PlayerController.PlayerInstance.HasPincers || PlayerController.PlayerInstance.IsWearingAntiTheft)
                    Feedback.Instance.ShowSimpleMessage(new Vector2(transform.position.x - 4f, transform.position.y + 4f), "***BEEEEEP BEEEEEEEEEP BEEEEEEP BEEEEEEEEEEP ***", new Color(1.0f, 0.5f, 0.5f), 32, 1.50f);

                else if (!PlayerController.PlayerInstance.HasSunglasses)
                    Say("These junkie eyes? Calling the cops!");

                Feedback.Instance.GameOver(transform);
            }
        }
    }

    bool CheckForPlayer()
    {
        int layerMask = ~(1 << gameObject.layer);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.up, 10f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0.5f), 10f, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0.5f), 10f, layerMask);

        if ((hit1.collider != null && hit1.collider.name == "Player") ||
            (hit2.collider != null && hit2.collider.name == "Player") ||
            (hit3.collider != null && hit3.collider.name == "Player"))
        {
            return true;
        }

        else if (Vector3.Distance(transform.position, PlayerController.PlayerInstance.transform.position) < 5.0f)
        {
            return true;
        }

        return false;
    }


    public void Say(string msg)
    {
        Feedback.Instance.ShowSimpleMessage(new Vector2(transform.position.x, transform.position.y + 2f), msg, new Color(0.5f, 0.5f, 1f), 22, 0.75f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }
}

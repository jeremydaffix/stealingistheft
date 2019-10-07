using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnWalk : MonoBehaviour
{
    [SerializeField]
    private string message;

    bool actionEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(actionEnabled)
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(PlayerController.PlayerInstance.Take(message))
                {
                    Destroy(gameObject);
                }
            }
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            actionEnabled = true;

            Feedback.Instance.ShowPlayerAction(message);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        actionEnabled = false;

        Feedback.Instance.HidePlayerAction();
    }
}

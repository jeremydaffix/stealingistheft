using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnWalk : MonoBehaviour
{
    [SerializeField]
    private string message;
    /*
    [SerializeField]
    private Vector2 position = new Vector2();

    [SerializeField]
    private Color color = Color.white;

    [SerializeField]
    private int size = 48;

    [SerializeField]
    private float duration = 0.25f;

    [SerializeField]
    bool autoDestroy = true;

    [SerializeField]
    SimpleMessageOnWalk nextMessage = null;*/


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
